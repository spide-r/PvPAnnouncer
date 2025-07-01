using System;
using System.Collections.Generic;
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

    public override Dictionary<uint, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<uint, List<string>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return false;
    }
}
