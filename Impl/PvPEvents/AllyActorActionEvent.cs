using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyActionEvent : PvPActionEvent
{
    public AllyActionEvent(uint actionId,
        List<string> soundPaths,
        List<string> soundPathsMasc,
        List<string> soundPathsFem, string name = "Action")
    {
        SoundPathsList = soundPaths;
        SoundPathsM = soundPathsMasc;
        SoundPathsF = soundPathsFem;
        ActionId = actionId;
        Name = name;
    }


    //generic event for specific action and sound pairings

    private List<string> SoundPathsList { get; }
    private List<string> SoundPathsM { get; }
    private List<string> SoundPathsF { get; }
    private uint ActionId { get; }

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
                return message.ActionId == ActionId;

            }
        }
        return false;
    }
}
