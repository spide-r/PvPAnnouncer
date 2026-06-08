using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class EnemyHitCcEvent : PvPActionEvent
{
    //todo - fill in CC id's and add announcements
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