using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class PvPAnnouncer: IPvPAnnouncer
{
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