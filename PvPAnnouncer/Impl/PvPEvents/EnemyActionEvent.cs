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

    public EnemyActionEvent(uint[] actionIds, string name = "Enemy Actions", string internalName = "EnemyActions")
    {
        _actionIds = actionIds;
        Name = name;
        InternalName = internalName;
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
