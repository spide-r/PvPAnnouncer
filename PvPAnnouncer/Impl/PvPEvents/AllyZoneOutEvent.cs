using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class AllyZoneOutEvent: PvPActorEvent
{
    public AllyZoneOutEvent()
    {
        Name = "Fatal Fall Damage";
        Id = "AllyZoneOutEvent";
    }

    public override bool InvokeRule(IMessage m) 
    {
        if (m is UserZoneOutMessage)
        {
            return true;
        }
        return false;
    }

}
