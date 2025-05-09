using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyActionEvent : PvPActionEvent
{
    //generic event for specific action and sound pairings

    private List<string> SoundPathsList { get; }
    private List<string> SoundPathsM { get; }
    private List<string> SoundPathsF { get; }
    private uint[] ActionIds { get; }
    
    public AllyActionEvent(uint[] actionIds,
        List<string> soundPaths,
        List<string> soundPathsMasc,
        List<string> soundPathsFem, string name = "Action")
    {
        SoundPathsList = soundPaths;
        SoundPathsM = soundPathsMasc;
        SoundPathsF = soundPathsFem;
        ActionIds = actionIds;
        Name = name;
    }

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

    public override bool InvokeRule(IMessage p)
    {
        if (p is ActionEffectMessage)
        {
            ActionEffectMessage message = (ActionEffectMessage)p;
            if (PluginServices.PvPMatchManager.IsMonitoredUser(message.SourceId))
            {
                return ActionIds.Contains(message.ActionId);

            }
        }
        return false;
    }
}
