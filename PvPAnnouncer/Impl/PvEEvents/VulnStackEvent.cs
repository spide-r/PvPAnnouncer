using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvEEvents;

public class VulnStackEvent : PvPActorEvent
{
    public VulnStackEvent()
    {
        Name = "Gaining a Vuln Stack";
        Id = "VulnStackEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is EnemyAppliedStatusMessage enemyAppliedStatusEvent)
            if (enemyAppliedStatusEvent.status == 1789)
                return true;

        return false;
    }
}