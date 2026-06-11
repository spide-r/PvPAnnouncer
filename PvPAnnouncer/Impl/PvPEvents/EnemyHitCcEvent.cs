using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class EnemyHitCcEvent : PvPActionEvent
{
    public EnemyHitCcEvent()
    {
        Name = "Enemy applied CC";
        Id = "EnemyHitCcEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is EnemyAppliedStatusMessage enemyAppliedStatusEvent)
        {
            var status = PluginServices.DataManager.Excel.GetSheet<Status>()
                .GetRow((uint) enemyAppliedStatusEvent.status);
            if (status.StatusCategory == 2) // detrimental
            {
                if (status.LockMovement) // probably stun/sleep
                {
                    PluginServices.PluginLog.Debug("Hit with stun/sleep!!!!!");
                    return true;
                }

                if (status.HitEffect.RowId == 4) // probably slow
                {
                    PluginServices.PluginLog.Debug("Hit with slow!!!!!!!!");
                    return true;
                }
            }
        }

        return false;
    }
}