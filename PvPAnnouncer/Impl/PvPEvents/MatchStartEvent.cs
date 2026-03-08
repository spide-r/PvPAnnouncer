using System;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class MatchStartEvent : PvPMatchEvent
{
    public MatchStartEvent()
    {
        Name = "Matches Starting";
        Id = "MatchStartEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchStartedMessage;
    }
}