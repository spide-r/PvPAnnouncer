using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MechSpawnEvent: PvPEvent
{
    public MechSpawnEvent()
    {
        Name = "Mech Spawn (Not Implemented Yet)";
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
        return false;
    }
}
