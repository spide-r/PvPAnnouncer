using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchConditionsEvent(List<string> soundPathsList): PvPEvent
{
    // Weather, Match Obstacles, Conditions Changing
    private List<string> SoundPathsList { get; init; } = soundPathsList;
    public override List<string> SoundPaths()
    {
        return SoundPathsList;
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
