using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchConditionsEvent : PvPEvent
{
    public MatchConditionsEvent(List<string> soundPathsList, string name = "Match Conditions")
    {
        SoundPathsList = soundPathsList;
        Name = name;
    }
    // Weather, Match Obstacles, Conditions Changing
    
    
    private List<string> SoundPathsList { get; }
    public override List<string> SoundPaths()
    {
        return SoundPathsList;
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return false;
    }
}
