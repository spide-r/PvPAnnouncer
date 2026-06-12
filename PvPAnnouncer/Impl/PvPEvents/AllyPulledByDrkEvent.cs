using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class AllyPulledByDrkEvent : PvPActionEvent
{
    public AllyPulledByDrkEvent()
    {
        Name = "[PvP Only] Pulled By Dark Knight";
        Id = "AllyPulledByDrkEvent";
    }

    //todo - extend this maybe to all draw-ins if you can find a way to filter out knockbacks 

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (pp.ActionId != ActionIds.SaltedEarth)
            {
                return false;
            }

            foreach (var target in pp.GetTargetIds())
            {
                foreach (var actionEffectType in pp.GetEffectTypes(target))
                {
                    PluginServices.PluginLog.Verbose($"ActionEffect Drk Pull on {target}: {actionEffectType}");
                }

                if (PluginServices.DutyManager.IsMonitoredUser(target))
                {
                    if (pp.GetEffectTypes(target).Contains(ActionEffectType.KbAndDrawIn))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}