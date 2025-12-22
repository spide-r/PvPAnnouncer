using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitHardByMultipleEvent: PvPEvent
{
    //todo counter that only trips when hit by like 6+ big hits in a timespan
    public override List<string> SoundPaths()
    {
        throw new System.NotImplementedException();
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        throw new System.NotImplementedException();
    }

    public override bool InvokeRule(IMessage message)
    {
        throw new System.NotImplementedException();
    }
}