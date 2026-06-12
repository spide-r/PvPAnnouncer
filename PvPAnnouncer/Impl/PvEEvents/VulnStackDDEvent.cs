using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvEEvents;

public class VulnStackDDEvent : PvPActorEvent
{
    public VulnStackDDEvent()
    {
        Name = "Gaining a Vuln Stack/Damage Down";
        Id = "VulnStackDDEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is AppliedStatusMessage enemyAppliedStatusEvent)
        {
            var status = PluginServices.DataManager.Excel.GetSheet<Status>()
                .GetRow((uint) enemyAppliedStatusEvent.status);
            if (status.StatusCategory == 2) // detrimental
            {
                if (status.Unknown0 == 39) // probably damage down
                    return true;

                if (status.Unknown0 == 40) // probably Vuln Up
                    return true;
            }
        }

        return false;
    }
}