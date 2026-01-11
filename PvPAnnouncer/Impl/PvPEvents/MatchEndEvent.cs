using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchEndEvent: PvPMatchEvent
{
    public MatchEndEvent()
    {
        Name = "Matches Ending";
        InternalName = "MatchEndEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [AllOverUntilNextTime];
    }

    public override Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<BattleTalk>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchEndMessage;
    }
}
