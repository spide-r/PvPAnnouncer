using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchEndEvent: PvPMatchEvent
{
    public MatchEndEvent()
    {
        Name = "Matches Ending";
        InternalName = "MatchEndEvent";
    }

    public override List<Shoutcast> SoundPaths()
    {
        return [AllOverUntilNextTime, HoldYourHeadHigh, OnlyPossibleOutcome, WellDone, CouldHaveBeenWorse,
            RaiseAGlassVictory, QuiteTheStatement, HeartRacingKrile, AllRightYouDidGreat];
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchEndMessage;
    }
}
