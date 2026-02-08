using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchConditionsEvent : PvPEvent
{
    public MatchConditionsEvent(List<Shoutcast> soundPathsList, string name = "Match Conditions", string id = "MatchConditions")
    {
        SoundPathsList = soundPathsList;
        Name = name;
        Id = id;
    }
    // Weather, Match Obstacles, Conditions Changing
    
    
    private List<Shoutcast> SoundPathsList { get; }

    public override bool InvokeRule(IMessage message)
    {
        return false;
    }
}
