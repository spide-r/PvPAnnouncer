using System;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class EnteredMechEvent: PvPEvent
{
    public EnteredMechEvent()
    {
        Name = "Entering a RW Mech";
        Id = "EnteredMechEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is UserEnteredMechMessage;
    }
}
