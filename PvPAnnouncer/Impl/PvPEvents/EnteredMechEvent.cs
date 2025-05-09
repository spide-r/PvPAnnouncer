using System;
using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnteredMechEvent: PvPEvent
{
    //todo: Test in a RW match
    public EnteredMechEvent()
    {
        Name = "Entered Mech (Not Implemented Yet)";
    }
    public override List<string> SoundPaths()
    {
        return [ColossalThing];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is UserEnteredMechMessage;
    }
}
