using System;
using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.impl.PvPEvents;
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
        
        PluginServices.PluginLog.Verbose($"PvP Event {pvpEvent.Name} received");
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
        
        _lastEvents.Enqueue(e.Name);

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
        bool b = _lastEvents.Contains(pvpEvent.Name);
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
        if (pvpEvent.SoundPathsFem().Count > 0 || pvpEvent.SoundPathsMasc().Count > 0)
        {
            bool userWantsFem = PluginServices.Config.WantsFem;
            bool userWantsMasc = PluginServices.Config.WantsMasc;

            if (userWantsFem || userWantsMasc)
            {
                
                if (userWantsFem)
                {
                    sounds.AddRange(pvpEvent.SoundPathsFem());
                }

                if (userWantsMasc)
                {
                    sounds.AddRange(pvpEvent.SoundPathsMasc());
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
    }

    private void WrapUp(PvPEvent pvpEvent, string chosenLine)
    {
        AddEventToRecentList(pvpEvent);
        AddVoiceLineToRecentList(chosenLine);
        _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}