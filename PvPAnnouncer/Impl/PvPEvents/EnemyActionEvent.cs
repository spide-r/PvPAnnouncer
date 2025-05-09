using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyActionEvent : PvPActionEvent
{
    private static uint[] _actionIds = [];

    public EnemyActionEvent(uint[] actionIds, List<string> soundPaths, List<string> soundPathsMasc, List<string> soundPathsFem, string name = "Enemy Actions")
    {
        _actionIds = actionIds;
        SoundPathsList = soundPaths;
        SoundPathsM = soundPathsMasc;
        SoundPathsF = soundPathsFem;
        Name = name;
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
