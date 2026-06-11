using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class AllyMitUsedEvent : PvPActionEvent
{
    public AllyMitUsedEvent()
    {
        Name = "[PvP Only] Mitigation";
        Id = "AllyMitUsedEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (PluginServices.DutyManager.IsMonitoredUser(pp.SourceId))
            {
                return ActionIds.IsMitigation(pp.ActionId);
            }
        }

        return false;
    }
}