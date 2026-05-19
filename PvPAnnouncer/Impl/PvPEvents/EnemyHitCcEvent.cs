using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class EnemyHitCcEvent : PvPActionEvent
{
    //todo
    public EnemyHitCcEvent()
    {
        Name = "Enemy applied CC";
        Id = "EnemyHitCcEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return false;
    }
}