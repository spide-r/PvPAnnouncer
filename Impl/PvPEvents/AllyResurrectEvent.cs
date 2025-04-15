using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent: PvPActorEvent
{
    public override List<string> SoundPaths()
    {
        return [BackUpGrit, BackOnFeet, RisesAgain];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IPacket packet) //todo: determine how a resurrection is logged/sent to the client
    {
        throw new NotImplementedException();
    }

}
