using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyLimitBreakEvent: PvPActionEvent
{
    public AllyLimitBreakEvent()
    {
        Name = "Limit Breaks";
        InternalName = "AllyLimitBreakEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return ActionIds.IsLimitBreak(pp.ActionId);
            }
        }
        return false;    
    }

}
