using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyActionEvent : PvPActionEvent
{
    //generic event for specific action and sound pairings

    private List<BattleTalk> SoundPathsList { get; }
    private uint[] ActionIds { get; }
    
    public AllyActionEvent(uint[] actionIds,
        List<BattleTalk> soundPaths, string name = "Action", string internalName = "internal")
    {
        SoundPathsList = soundPaths;
        ActionIds = actionIds;
        Name = name;
        InternalName = internalName;
    }

    public override List<BattleTalk> SoundPaths()
    {
        return SoundPathsList;
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
