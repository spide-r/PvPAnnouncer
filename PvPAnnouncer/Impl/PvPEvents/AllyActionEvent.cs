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
    private Dictionary<uint, List<string>> PersonalizedSoundPathsList;
    private uint[] ActionIds { get; }
    
    public AllyActionEvent(uint[] actionIds,
        List<string> soundPaths,
        Dictionary<uint, List<string>> personalizedSoundPaths, string name = "Action")
    {
        SoundPathsList = soundPaths;
        PersonalizedSoundPathsList = personalizedSoundPaths;
        ActionIds = actionIds;
        Name = name;
    }

    public override List<string> SoundPaths()
    {
        return SoundPathsList;
    }

    public override Dictionary<uint, List<string>> PersonalizedSoundPaths()
    {
        return PersonalizedSoundPathsList;
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
