using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class AllyLimitBreakEvent : PvPActionEvent
{
    public AllyLimitBreakEvent()
    {
        Name = "Limit Breaks";
        Id = "AllyLimitBreakEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return pp.IsLimitBreak();
            }
        }

        return false;
    }
}