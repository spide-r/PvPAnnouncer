using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
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
        if (p is ActionEffectMessage)
        {
            ActionEffectMessage message = (ActionEffectMessage)p;
            if (PluginServices.PvPMatchManager.IsMonitoredUser(message.SourceId))
            {
                return message.ActionId == ActionId;

            }
        }
        return false;
    }
}
