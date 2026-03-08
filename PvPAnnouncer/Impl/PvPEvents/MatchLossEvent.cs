using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class MatchLossEvent : PvPMatchEvent
{
    public MatchLossEvent()
    {
        Name = "Match Loss";
        Id = "MatchLossEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchLossMessage;
    }
}