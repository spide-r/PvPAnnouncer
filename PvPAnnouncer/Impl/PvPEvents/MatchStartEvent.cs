using System;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchStartEvent: PvPMatchEvent
{
    public MatchStartEvent()
    {
        Name = "Matches Starting";
        InternalName = "MatchStartEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchStartedMessage;
    }

}
