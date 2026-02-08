using Dalamud.Game.ClientState.Conditions;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyMissedCc: PvPActionEvent
{
    public EnemyMissedCc()
    {
        Name = "Cleansing & Dodging CC";
        Id = "EnemyMissedCcEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    if (PluginServices.Condition.Any(ConditionFlag.Transformed))
                    {
                        return false;
                    }
                    if (pp.GetEffectTypes(target).Contains(ActionEffectType.StatusNoEffect)) // whats the difference between StatusNoEffect and NoEffectText 
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
