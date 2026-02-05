using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Client.System.Threading;
using FFXIVClientStructs.FFXIV.Client.UI;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class Announcer(IEventShoutcastMapping eventShoutcastMapping, IShoutcastRepository shoutcastRepository): IAnnouncer
{
    /*
     * Objectives:
     * 1. Comment a Reasonable amount
     * 2. Dont repeat voice lines
     * 3. Don't comment on the same thing twice
     * 4. Don't say more than one thing at a time or comment too quickly
     * 5. Use the appropriate gender for the challenger
     */
    
    private readonly Queue<Shoutcast?> _lastVoiceLines = new();
    private readonly Queue<string> _lastEvents = new();
    private long _timestamp = 0;
    private int _lastVoiceLineLength = 0;

    public void ClearQueue()
    {
        _lastVoiceLines.Clear();
        _lastEvents.Clear();
    }

    public void ReceivePvPEvent(bool bypass, PvPEvent pvpEvent)
    {
        if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
        {
            PluginServices.PluginLog.Error("Dawntrail is not installed! Plugin will do nothing until this is fixed!");
            return;
        }
        
        PluginServices.PluginLog.Verbose($"PvP Event {pvpEvent.InternalName} received");
        long newTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        long diff = newTimestamp - _timestamp;
        
        // == Objective 4 ==
        if (diff < (PluginServices.Config.CooldownSeconds + _lastVoiceLineLength))
        {
            PluginServices.PluginLog.Verbose($"Cooldown not finished");
            return;
        }

        if (bypass)
        {
            PluginServices.PluginLog.Verbose("Bypassing randomness & repeat commentary.");
            PlaySoundAndSendBattleTalk(true, pvpEvent);
            return;
        }

        
        int rand = Random.Shared.Next(100);
        
        
        // == Objective 1 ==
        if (rand < (100 - PluginServices.Config.Percent)) 
        {
            PluginServices.PluginLog.Verbose($"Percent not hit. is {rand} when it should be greater than {100 - PluginServices.Config.Percent}");

            return;
        }
        // == Objective 3 == 
        if (FailsRepeatCommentaryCheck(pvpEvent))
        {
            //PluginServices.PluginLog.Verbose($"Repeat commentary check failed");
            return;
        }
        
        PlaySoundAndSendBattleTalk(pvpEvent);
    }

    public void ReceivePvPEvent(PvPEvent pvpEvent)
    {
        ReceivePvPEvent(false, pvpEvent);
    }
    

    private void AddEventToRecentList(PvPEvent e)
    {
        PluginServices.PluginLog.Verbose("Adding Event to history");
        if (_lastEvents.Count > PluginServices.Config.RepeatEventCommentaryQueue - 1) 
        {
            PluginServices.PluginLog.Verbose($"Dequeuing Event from history");

            _lastEvents.Dequeue();
        }
        
        _lastEvents.Enqueue(e.InternalName);
    }

    
    private void AddVoiceLineToRecentList(Shoutcast talk)
    {
        PluginServices.PluginLog.Verbose($"Adding Voice line {talk.SoundPath} to history");

        if (_lastVoiceLines.Count > PluginServices.Config.RepeatVoiceLineQueue - 1) 
        {
            PluginServices.PluginLog.Verbose($"Dequeuing Voice Line from history");

            _lastVoiceLines.Dequeue();
        }
        
        _lastVoiceLines.Enqueue(talk);

    }

    private bool FailsRepeatCommentaryCheck(PvPEvent pvpEvent)
    {
        bool b = _lastEvents.Contains(pvpEvent.InternalName);
        foreach (var lastEvent in _lastEvents)
        {
            PluginServices.PluginLog.Verbose($"Last Event: {lastEvent}");
        }
        PluginServices.PluginLog.Verbose($"Repeat commentary check triggered - value is {b}");
        return b;
    }

    public void PlaySound(string sound)
    {
        PluginServices.PluginLog.Verbose($"Playing sound: {sound}");

        if (!PluginServices.DataManager.FileExists(sound))
        {
            PluginServices.PluginLog.Error("Sound file not found!");
            return;
        }
        
        
        PluginServices.SoundManager.PlaySound(sound);
    }
    

    private void PlaySoundAndSendBattleTalk(bool bypass, PvPEvent pvpEvent)
    {
        List<Shoutcast> sounds = [];

        foreach (var shoutcastId in eventShoutcastMapping.GetShoutcastList(pvpEvent.Name))
        {
            var sound = shoutcastRepository.GetShoutcast(shoutcastId);
            // == Objective 5 == 
            if (PluginServices.Config.WantsAllAttributes(sound.Attributes))
            {
                sounds.AddRange(sound);
            }
        }
        
        // == Objective 2 == 
        if (!bypass)
        {
            foreach (var line in _lastVoiceLines)
            {
                if (line != null && sounds.Contains(line))
                {
                    sounds.Remove(line);
                }
            }
        }

        if (sounds.Count < 1)
        {
            return;
        }
        
        int rand = Random.Shared.Next(sounds.Count);
        var s = sounds[rand];
        WrapUp(pvpEvent, s);
        Task announceTask = Task.Factory.StartNew(async () =>
        {
            PluginServices.PluginLog.Verbose($"Playing announcement (Delaying): {s.SoundPath} by {PluginServices.Config.AnimationDelayFactor}");
            await Task.Delay(PluginServices.Config.AnimationDelayFactor); //delay to prevent shenanigans w/ attacks being announced before their animations finish
            var p = s.GetShoutcastSoundPathWithLang(PluginServices.Config.Language);
            PlaySound(p);
            SendBattleTalk(s);
            PluginServices.PluginLog.Verbose($"Finished Playing announcement after delay: {s}");

        });
        announceTask.WaitAsync(TimeSpan.FromSeconds(5));
    }
    public void PlaySoundAndSendBattleTalk(PvPEvent pvpEvent)
    {
        PlaySoundAndSendBattleTalk(false, pvpEvent);
    }

    private void WrapUp(PvPEvent pvpEvent, Shoutcast? chosenLine)
    {
        AddEventToRecentList(pvpEvent);
        if (chosenLine != null)
        {
            AddVoiceLineToRecentList(chosenLine);
            _lastVoiceLineLength = chosenLine.Duration;
        }

        _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public void SendBattleTalk(Shoutcast shoutcast) 
    {
        if (PluginServices.Config.HideBattleText) 
        {
            return;
        }

        var transcription = "";

        if (shoutcast.GetTranscriptionWithLang(PluginServices.Config.Language).Equals(""))
        {
            if (shoutcast.GetTranscriptionWithLang("en").Equals(""))
            {
                PluginServices.PluginLog.Verbose($"Text empty for {shoutcast.ShouterName}, {shoutcast.SoundPath}");
                return; 
            }

            transcription = shoutcast.GetTranscriptionWithLang("en");
            PluginServices.PluginLog.Warning($"Text empty for {shoutcast.ShouterName}, {shoutcast.SoundPath} on lang {PluginServices.Config.Language} - falling back to EN");
        }
        else
        {
            transcription = shoutcast.GetTranscriptionWithLang(PluginServices.Config.Language);
        }

        unsafe
        {
            try
            {
                var name = shoutcast.ShouterName;
                var duration = shoutcast.Duration;
                var icon = shoutcast.Icon;
                var style = shoutcast.Style;
                if (icon != 0 && PluginServices.Config.WantsIcon)
                {
                    UIModule.Instance()->ShowBattleTalkImage(name, transcription, duration, icon, style);
                }
                else
                {
                    UIModule.Instance()->ShowBattleTalk(name, transcription, duration, style);
                }
                //todo play around with UIModule.Instance()->ShowBattleTalkSound();
            }
            catch (InvalidOperationException) 
            {
                UIModule.Instance()->ShowBattleTalk(InternalConstants.PvPAnnouncerDevName, InternalConstants.ErrorContactDev, 6, 6);
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "Issue sending Battle Talk!");
            }
            
        }
    }
}