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
    
    private readonly Queue<string> _lastThreeVoiceLines = new();
    private PvPEvent _lastEvent = new AllyDeathEvent();
    private long _timestamp;
    public void ReceivePvPEvent(PvPEvent pvpEvent)
    {
        long newTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        long diff = newTimestamp - _timestamp;
        
        // == Objective 4 ==
        if (diff < PluginServices.Config.CooldownSeconds)
        {
            return;
        }

        _timestamp = newTimestamp;
        
        int rand = Random.Shared.Next(100);
        
        
        // == Objective 1 ==
        if (rand < PluginServices.Config.Percent) 
        {
            return;
        }
        // == Objective 3 == 
        if (!PassesRepeatCommentaryCheck(pvpEvent))
        {
            return;
        }
        
        PlaySound(pvpEvent);
    }
    

    private void AddToRecentCommentary(String e)
    {
        if (_lastThreeVoiceLines.Count > 2) // maximum 3 
        {
            _lastThreeVoiceLines.Dequeue();
        }
        
        _lastThreeVoiceLines.Enqueue(e);

    }

    private bool PassesRepeatCommentaryCheck(PvPEvent pvpEvent)
    {
        return pvpEvent == _lastEvent;
    }

    public void PlaySound(string sound)
    {
        PluginServices.SoundManager.PlaySound(sound);

    }

    public void PlaySound(PvPEvent pvpEvent)
    {
        _lastEvent = pvpEvent;
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
        foreach (var line in _lastThreeVoiceLines)
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
        AddToRecentCommentary(s);
        PlaySound(AnnouncerLines.GetPath(s));
    }
}