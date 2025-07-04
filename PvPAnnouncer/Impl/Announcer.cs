﻿using System;
using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Client.UI;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class Announcer: IAnnouncer
{
    /*
     * Objectives:
     * 1. Comment a Reasonable amount
     * 2. Dont repeat voice lines
     * 3. Don't comment on the same thing twice
     * 4. Don't say more than one thing at a time or comment too quickly
     * 5. Use the appropriate gender for the challenger
     */
    
    private readonly Queue<string> _lastVoiceLines = new();
    private readonly Queue<string> _lastEvents = new();
    private long _timestamp = 0;
    public void ReceivePvPEvent(PvPEvent pvpEvent)
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
        if (diff < PluginServices.Config.CooldownSeconds)
        {
            PluginServices.PluginLog.Verbose($"Cooldown not finished");
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
        
        PlaySound(pvpEvent);
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

    
    private void AddVoiceLineToRecentList(String e)
    {
        PluginServices.PluginLog.Verbose("Adding Voice line to history");

        if (_lastVoiceLines.Count > PluginServices.Config.RepeatVoiceLineQueue - 1) 
        {
            PluginServices.PluginLog.Verbose($"Dequeuing Voice Line from history");

            _lastVoiceLines.Dequeue();
        }
        
        _lastVoiceLines.Enqueue(e);

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
        PluginServices.SoundManager.PlaySound(sound);
    }

    public void PlaySound(PvPEvent pvpEvent)
    {
        List<string> sounds = new List<string>(pvpEvent.SoundPaths());
        
        // == Objective 5 == 

        if (PluginServices.Config.WantsPersonalizedVoiceLines)
        {
            foreach (var personalizedSoundPath in pvpEvent.PersonalizedSoundPaths())
            {
                var key = personalizedSoundPath.Key;
                var personalSounds = personalizedSoundPath.Value;
                if (PluginServices.Config.WantsPersonalization(key))
                {
                    sounds.AddRange(personalSounds);
                }
            }
        }
        
        
        
        // == Objective 2 == 
        foreach (var line in _lastVoiceLines)
        {
            if (sounds.Contains(line))
            {
                sounds.Remove(line);
            }
        }

        if (sounds.Count < 1)
        {
            return;
        }
        
        int rand = Random.Shared.Next(sounds.Count);
        string s = sounds[rand];
        WrapUp(pvpEvent, s);
        PlaySound(AnnouncerLines.GetPath(s));
        SendBattleTalk(s);
    }

    private void WrapUp(PvPEvent pvpEvent, string chosenLine)
    {
        AddEventToRecentList(pvpEvent);
        AddVoiceLineToRecentList(chosenLine);
        _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public void SendBattleTalk(string voiceLine) //todo: when we eventually add the custom event creator it might be worth moving this to its own class
    {
        if (PluginServices.Config.HideBattleText || voiceLine.StartsWith("cut")) //todo: sloppy
        {
            return;
        }
        unsafe
        {
            try
            {
                BattleTalk battleTalk = new BattleTalk(voiceLine);
                var name = "Metem";
                var text = battleTalk.Text.ExtractText();
                var duration = battleTalk.Duration;
                var icon = battleTalk.Icon;
                var style = battleTalk.Style;
                if (icon != 0)
                {
                    UIModule.Instance()->ShowBattleTalkImage(name, text, icon, duration, style);
                }
                else
                {
                    UIModule.Instance()->ShowBattleTalk(name, text, duration, style);
                }
            }
            catch (InvalidOperationException e)
            {
                UIModule.Instance()->ShowBattleTalk("Metem", AnnouncerLines.GetAnnouncementStringFromUnusedVo(voiceLine), AnnouncerLines.GetAnnouncementLengthFromUnusedVo(voiceLine), 6);
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "Issue sending Battle Talk!");
            }
            
        }
    }
}