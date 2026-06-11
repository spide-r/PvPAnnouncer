using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class MatchVictoryEvent : PvPMatchEvent
{
    public MatchVictoryEvent()
    {
        Name = "Duty Victory";
        Id = "MatchVictoryEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchVictoryMessage;
    }
}