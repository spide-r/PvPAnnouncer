using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitByLimitBreakEvent : PvPActionEvent
{
    public AllyHitByLimitBreakEvent()
    {
        Name = "Hit by an Enemy Limit Break";
        Id = "AllyHitByLimitBreakEvent"; 
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                return false;
            }
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    return ActionIds.IsLimitBreakAttack(pp.ActionId);
                }
            }
        }
        return false;
    }
}
