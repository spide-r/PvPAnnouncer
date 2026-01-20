using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchConditionsEvent : PvPEvent
{
    public MatchConditionsEvent(List<BattleTalk> soundPathsList, string name = "Match Conditions", string internalName = "MatchConditions")
    {
        SoundPathsList = soundPathsList;
        Name = name;
        InternalName = internalName;
    }
    // Weather, Match Obstacles, Conditions Changing
    
    
    private List<BattleTalk> SoundPathsList { get; }
    public override List<BattleTalk> SoundPaths()
    {
        return SoundPathsList;
    }

    public override bool InvokeRule(IMessage message)
    {
        return false;
    }
}
