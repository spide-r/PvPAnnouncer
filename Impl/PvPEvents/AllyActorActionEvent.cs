using System;
using System.Collections.Generic;
using PvPAnnouncer.Impl;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyActionEvent : PvPActorActionEvent
{
    //generic event for specific action and sound pairings
    public AllyActionEvent(uint actionId, List<string> soundPaths, List<string> soundPathsMasc, List<string> soundPathsFem)
    {
        SoundPathsList = soundPaths;
        SoundPathsF = soundPathsFem;
        SoundPathsM = soundPathsMasc;
        ActionId = actionId;
    }

    public List<string> SoundPathsList { get; init; }
    public List<string> SoundPathsM { get; init; }
    public List<string> SoundPathsF { get; init; }
    public uint ActionId { get; init; }

    public override List<string> SoundPaths()
    {
        return SoundPathsList;
    }

    public override List<string> SoundPathsMasc()
    {
        return SoundPathsM;
    }

    public override List<string> SoundPathsFem()
    {
        return SoundPathsF;
    }

    public override bool InvokeRule(IPacket p)
    {
        if (p is ActionEffectPacket)
        {
            ActionEffectPacket packet = (ActionEffectPacket)p;
            if (PluginServices.PvPMatchManager.IsMonitoredUser(packet.SourceId))
            {
                return packet.ActionId == ActionId;

            }
        }
        return false;
    }
}
