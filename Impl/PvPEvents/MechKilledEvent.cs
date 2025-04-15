using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MechKilledEvent: PvPEvent
{
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

    public override bool InvokeRule(IPacket packet)
    {
        return false;
    }
}
