using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyZoneOutEvent: PvPActorEvent
{
    public override List<string> SoundPaths()
    {
        return [Fallen, TheyreDownIsItOver, ChallengerDownIsThisEnd, TooMuch];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IPacket packet) //todo: determine how a fall damage death is logged
    {
        throw new NotImplementedException();
    }

}
