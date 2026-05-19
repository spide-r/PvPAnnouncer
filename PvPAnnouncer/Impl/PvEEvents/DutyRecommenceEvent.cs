using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvEEvents;

public class DutyRecommenceEvent : PvPActorEvent
{
    public DutyRecommenceEvent()
    {
        Name = "Restarting a Duty";
        Id = "DutyRecommenceEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is DutyRecommenceMessage;
    }
}