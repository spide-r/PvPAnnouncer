using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyActionEvent : PvPActionEvent
{
    private static uint[] _actionIds = [];

    public EnemyActionEvent(uint[] actionIds, List<string> soundPaths, Dictionary<Personalization, List<string>> personalizedSoundPaths, string name = "Enemy Actions")
    {
        _actionIds = actionIds;
        SoundPathsList = soundPaths;
        PersonalizedSoundPathsList = personalizedSoundPaths;
        Name = name;
    }

    public List<string> SoundPathsList { get; init; }
    public Dictionary<Personalization, List<string>> PersonalizedSoundPathsList { get; init; }
    public override List<string> SoundPaths()
    {
        return SoundPathsList;
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return PersonalizedSoundPathsList;
    }
    
    public override bool InvokeRule(IMessage arg)
    {
        if (arg is ActionEffectMessage message)
        {
            if (!_actionIds.Contains(message.ActionId))
            {
                return false;
            }
            foreach (var target in message.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
