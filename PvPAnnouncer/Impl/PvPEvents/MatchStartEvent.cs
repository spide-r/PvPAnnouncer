using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchStartEvent: PvPMatchEvent
{
    public MatchStartEvent()
    {
        Name = "Matches Starting";
        InternalName = "MatchStartEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [SendingCameras, UpstartBegins, OurTurnThancred, FirstOpponent, NineLives, IntroBc,
            HoneyBShowBegun, SavorSting, IntroHbl, BBMuscled, IntroBb, WTReturned, IntroWt,
            DGSteps, IntroDg, SRGallery, IntroSr,BAMeansBusiness, IntroBa, WolfLair, IntroHb,
            VFFeastEyes, IntroVf, TagTeam, IntroDbRh, GrandChampion, IntroTt, IntroLw, GiveItEverythingAlisaie, 
            ShowThemWhatYoureMadeOf, VictoryWithinReach, ExplosivesInPlace, ToTheBoldGoTheSpoils, OvercomeTheOdds,
            LooksPromising, AchievedWithoutRisk, VictoryAtHandIsItNot, WereCountingOnYou, MayTheTwelveGuideYou, FocusOnVictoryGraha, TaskAtHandVictoryOurs,
            CanvasOfVictory, SkillsToTheProof, IntentionsKnown, StrategyKeyToVictory, VowOfReasonGuide, WorthyOfReputation, VictoryOurs, CouldBeChance];
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchStartedMessage;
    }

}
