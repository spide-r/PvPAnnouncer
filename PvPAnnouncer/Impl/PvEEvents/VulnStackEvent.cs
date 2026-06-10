using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvEEvents;

public class VulnStackEvent : PvPActorEvent
{
    public VulnStackEvent()
    {
        Name = "Gaining a Vuln Stack/Damage Down";
        Id = "VulnStackEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is EnemyAppliedStatusMessage enemyAppliedStatusEvent)
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