using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchEndEvent: PvPMatchEvent
{
    public MatchEndEvent()
    {
        Name = "Matches Ending";
    }

    public override List<string> SoundPaths()
    {
        return [AllOverUntilNextTime, BattleElectrifying];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
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
