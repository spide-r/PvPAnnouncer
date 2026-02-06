using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;


public class AllyHitEnemyHardEvent : PvPActionEvent
{
    public AllyHitEnemyHardEvent()
    {
        Name = "Hit an Enemy Hard/Used Burst on an enemy";
        InternalName = "AllyHitEnemyHardEvent";
    }

    public override bool InvokeRule(IMessage message) 
    {
        if (message is ActionEffectMessage pp)
        {
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                return false;
            }
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return pp.CritsOrDirectHits() || ActionIds.IsLimitBreakAttack(pp.ActionId) || ActionIds.IsBurst(pp.ActionId);
            }
        }
        return false;
    }
}
