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

    public EnemyActionEvent(uint[] actionIds, List<BattleTalk> soundPaths, Dictionary<Personalization, List<BattleTalk>> personalizedSoundPaths, string name = "Enemy Actions", string internalName = "EnemyActions")
    {
        _actionIds = actionIds;
        SoundPathsList = soundPaths;
        PersonalizedSoundPathsList = personalizedSoundPaths;
        Name = name;
        InternalName = internalName;
    }

    public List<BattleTalk> SoundPathsList { get; init; }
    public Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPathsList { get; init; }
    public override List<BattleTalk> SoundPaths()
    {
        return SoundPathsList;
    }

    public override Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths()
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
