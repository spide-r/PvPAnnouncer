using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class Announcer: IAnnouncer
{
    /*
     * Objectives:
     * Comment a Reasonable amount
     * Dont repeat voice lines
     * Don't comment on the same thing twice
     * Don't say more than one thing at a time
     * Dont't comment too quickly
     * Use the appropriate gender for the challenger
     */
    public void ReceivePvPEvent(IPvPEvent pvpEvent)
    {
        //todo implement actual logic
        PlaySound(pvpEvent);
    }

    public void PlaySound(IPvPEvent pvpEvent)
    {
        string[]? sounds = pvpEvent.SoundPaths;
        if (sounds != null)
        {
            int rand = Random.Shared.Next(sounds.Length);
        
            PvPAnnouncerPlugin.SoundManager?.PlaySound(sounds[rand]);
        }
    }
}