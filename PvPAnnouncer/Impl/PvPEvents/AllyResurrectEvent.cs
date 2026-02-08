using System;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent: PvPActorEvent
{
    public AllyResurrectEvent()
    {
        Name = "Resurrection";
        Id = "AllyResurrectEvent";
    }

    public override bool InvokeRule(IMessage m)
    {
        return m is UserResurrectedMessage;
    }

}
