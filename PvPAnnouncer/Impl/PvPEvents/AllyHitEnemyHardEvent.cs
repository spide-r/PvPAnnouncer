using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class AllyHitEnemyHardEvent : PvPActionEvent
{
    public AllyHitEnemyHardEvent()
    {
        Name = "Hit an Enemy Hard/Used Burst on an enemy";
        Id = "AllyHitEnemyHardEvent";
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
                //todo critsOrDirectHits may be too touchy when in a pve duty 
                return pp.CritsOrDirectHits() || pp.IsLimitBreak() ||
                       ActionIds.IsBurst(pp.ActionId);
            }
        }

        return false;
    }
}