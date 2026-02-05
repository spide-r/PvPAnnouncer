using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnteredMechEvent: PvPEvent
{
    public EnteredMechEvent()
    {
        Name = "Entering a RW Mech";
        InternalName = "EnteredMechEvent";
    }
    public override List<Shoutcast> SoundPaths()
    {
        return [ColossalThing];
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is UserEnteredMechMessage;
    }
}
