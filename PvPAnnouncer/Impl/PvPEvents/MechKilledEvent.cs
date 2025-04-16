using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MechKilledEvent: PvPEvent
{
    public MechKilledEvent()
    {
        Name = "Mech Killed (Not Implemented Yet)";
    }
    public override List<string> SoundPaths()
    {
        return [];
    }

    public override List<string> SoundPathsMasc()
    {
        return [ColossalThingSwordMasc];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IMessage message)
    {
        return false;
    }
}
