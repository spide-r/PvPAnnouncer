using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvEEvents;

public class VulnStackEvent : PvPActorEvent
{
    //todo make the announcers respond to this
    public VulnStackEvent()
    {
        Name = "Gaining a Vuln Stack";
        Id = "VulnStackEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is EnemyAppliedStatusMessage enemyAppliedStatusEvent)
            if (enemyAppliedStatusEvent.status == StatusIds.VulnUp)
                return true;

        return false;
    }
}