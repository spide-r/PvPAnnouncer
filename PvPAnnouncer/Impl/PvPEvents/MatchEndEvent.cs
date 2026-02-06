using System;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchEndEvent: PvPMatchEvent
{
    public MatchEndEvent()
    {
        Name = "Matches Ending";
        InternalName = "MatchEndEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchEndMessage;
    }
}
