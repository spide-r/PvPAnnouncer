using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchVictoryEvent: PvPMatchEvent
{
    public MatchVictoryEvent()
    {
        Name = "Match Victory";
        Id = "MatchVictoryEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchVictoryMessage;
    }
}