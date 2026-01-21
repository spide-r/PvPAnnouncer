using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Client.System.Threading;
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
    
    private readonly Queue<BattleTalk> _lastVoiceLines = new();
    private readonly Queue<string> _lastEvents = new();
    private long _timestamp = 0;
    private int _lastVoiceLineLength = 0;

    public void ClearQueue()
    {
        _lastVoiceLines.Clear();
        _lastEvents.Clear();
    }
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
        if (diff < (PluginServices.Config.CooldownSeconds + _lastVoiceLineLength))
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

    
    private void AddVoiceLineToRecentList(BattleTalk e)
    {
        PluginServices.PluginLog.Verbose($"Adding Voice line {e.Voiceover} to history");

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
        List<BattleTalk> sounds = new List<BattleTalk>();

        foreach (var sound in pvpEvent.SoundPaths())
        {
            // == Objective 5 == 
            if (PluginServices.Config.WantsAllPersonalization(sound.Personalization))
            {
                sounds.AddRange(sound);

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
        var s = sounds[rand];
        WrapUp(pvpEvent, s);
        Task announceTask = Task.Factory.StartNew(async () =>
        {
            PluginServices.PluginLog.Verbose($"Playing announcement (Delaying): {s} by {PluginServices.Config.AnimationDelayFactor}");
            await Task.Delay(PluginServices.Config.AnimationDelayFactor); //delay to prevent shenanigans w/ attacks being announced before their animations finish
            PlaySound(s.GetPath(PluginServices.Config.Language));
            SendBattleTalk(s);
            PluginServices.PluginLog.Verbose($"Finished Playing announcement after delay: {s}");

        });
        announceTask.WaitAsync(TimeSpan.FromSeconds(5));
    }

    private void WrapUp(PvPEvent pvpEvent, BattleTalk chosenLine)
    {
        AddEventToRecentList(pvpEvent);
        AddVoiceLineToRecentList(chosenLine);
        _lastVoiceLineLength = chosenLine.Duration;
        _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public void SendBattleTalk(BattleTalk battleTalk) 
    {
        if (PluginServices.Config.HideBattleText || battleTalk is CutsceneTalk) 
        {
            return;
        }
        unsafe
        {
            try
            {
                var name = battleTalk.Name;
                var text = battleTalk.Text.ExtractText(); 
                var duration = battleTalk.Duration;
                var icon = battleTalk.Icon;
                var style = battleTalk.Style;
                if (text.StartsWith('_'))
                {
                    text = "Uh oh! You shouldn't see this! Contact the PvPannouncer dev!";
                    duration = 5;
                }
                if (icon != 0 && PluginServices.Config.WantsIcon)
                {
                    UIModule.Instance()->ShowBattleTalkImage(name, text, duration, icon, style);
                }
                else
                {
                    UIModule.Instance()->ShowBattleTalk(name, text, duration, style);
                }
            }
            catch (InvalidOperationException _) 
            {
                UIModule.Instance()->ShowBattleTalk("Metem", "Uh oh! You shouldn't see this! Contact the PvPAnnouncer dev!", 6, 6);
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "Issue sending Battle Talk!");
            }
            
        }
    }
 
    //icon notes: 070000/073071,073265,073266, 073261, 073275, 
    // alphinaud: 73000/73008 - ew costume
    /*
     * ui/icon/073000/073071_hr1.tex
ui/icon/073000/073265_hr1.tex
ui/icon/073000/073266_hr1.tex
ui/icon/073000/073261_hr1.tex
ui/icon/073000/073275_hr1.tex
ui/icon/073000/073287_hr1.tex
ui/icon/073000/073034_hr1.tex
ui/icon/073000/073036_hr1.tex
ui/icon/073000/073024_hr1.tex
ui/icon/073000/073026_hr1.tex
ui/icon/073000/073025_hr1.tex
ui/icon/073000/073007_hr1.tex
ui/icon/073000/073012_hr1.tex
     */
    /*
     * ui/icon/073000/073071_hr1.tex
ui/icon/073000/073265_hr1.tex
ui/icon/073000/073266_hr1.tex
ui/icon/073000/073261_hr1.tex
ui/icon/073000/073275_hr1.tex
ui/icon/073000/073287_hr1.tex
ui/icon/073000/073034_hr1.tex
ui/icon/073000/073036_hr1.tex
ui/icon/073000/073024_hr1.tex
ui/icon/073000/073026_hr1.tex
ui/icon/073000/073025_hr1.tex
ui/icon/073000/073007_hr1.tex
ui/icon/073000/073012_hr1.tex
ui/icon/073000/073112_hr1.tex

     */
    // guaranteed legit ones:
    /*
     * ui/icon/073000/073178_hr1.tex
ui/icon/073000/073085_hr1.tex
ui/icon/073000/073071_hr1.tex
ui/icon/073000/073265_hr1.tex
ui/icon/073000/073266_hr1.tex
ui/icon/073000/073261_hr1.tex
ui/icon/073000/073275_hr1.texe
ui/icon/073000/073287_hr1.tex
ui/icon/073000/073210_hr1.tex
ui/icon/073000/073034_hr1.tex
ui/icon/073000/073036_hr1.tex
ui/icon/073000/073112_hr1.tex
ui/icon/073000/073178_hr1.tex
ui/icon/073000/073085_hr1.tex

     */
    // sound/voice/vo_line/8204254_en.scd

}