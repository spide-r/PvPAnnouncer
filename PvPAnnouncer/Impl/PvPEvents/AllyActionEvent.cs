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

    private List<string> SoundPathsList { get; }
    private Dictionary<Personalization, List<string>> PersonalizedSoundPathsList;
    private uint[] ActionIds { get; }
    
    public AllyActionEvent(uint[] actionIds,
        List<string> soundPaths,
        Dictionary<Personalization, List<string>> personalizedSoundPaths, string name = "Action", string internalName = "internal")
    {
        SoundPathsList = soundPaths;
        PersonalizedSoundPathsList = personalizedSoundPaths;
        ActionIds = actionIds;
        Name = name;
        InternalName = internalName;
    }

    public override List<string> SoundPaths()
    {
        return SoundPathsList;
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
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
