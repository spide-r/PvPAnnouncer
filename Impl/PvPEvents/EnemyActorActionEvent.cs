using System;
using System.Collections.Generic;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyActorActionEvent : PvPActorActionEvent
{
    private static uint _actionId;

    public EnemyActorActionEvent(uint actionId, List<string> soundPaths, List<string> soundPathsMasc, List<string> soundPathsFem)
    {
        _actionId = actionId;
        SoundPathsList = soundPaths;
        SoundPathsM = soundPathsMasc;
        SoundPathsF = soundPathsFem;
    }

    public List<string> SoundPathsList { get; init; }
    public List<string> SoundPathsM { get; init; }
    public List<string> SoundPathsF { get; init; }

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
    public override bool InvokeRule(IPacket arg)
    {
        if (arg is ActionEffectPacket)
        {
            ActionEffectPacket packet = (ActionEffectPacket)arg;
            if (!PluginServices.PvPMatchManager.IsMonitoredUser(packet.SourceId))
            {
                return packet.ActionId == _actionId;
            }
        }
        return false;
    }
}
