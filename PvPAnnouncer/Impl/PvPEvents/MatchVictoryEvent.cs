using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchVictoryEvent: PvPMatchEvent
{
    public MatchVictoryEvent()
    {
        Name = "Match Victory";
        InternalName = "MatchVictoryEvent";
    }

    public override List<Shoutcast> SoundPaths()
    {
        return [GenericVictory,  NewGcBornVictory, RobotKo, VictoryChamp, MjBeautifullyPlayed, 
            MjMadeYourMark, MjKissLadyLuck, MjClobberedWithTable, Hahahahahaha, LetThisMomentLastForever, 
            PlayedHandToPerfection, TriumphWorthyOfCelebration, VictoryGivesGreaterHope, CommendableResult, 
            PerformanceForTheAges, HoldYourHeadHigh, WorthyOfGrandfather, OnlyPossibleOutcome, WellDone,
            ToTheBoldGoTheSpoils, AllAccordingToPlan, VictoryIsOursThancred, PraiseOrPity, DecisiveVictoryThancred, RaiseAGlassVictory, CouldHaveBeenWorse,
            FavoredByTheSpinner, MasterfulStrategy, SureStepForward, VictoriesPaveWayToRenown, UnequivocalVictory, ImpeccablePerformance, 
            ExcellentRoundImpressed, NotRestOnLaurels, BestEffortWellDone, RefreshingDiversion, NoLessExpectedEstinien, AimForTheKill,
            HonorToFightBySide, SplendidResultKeepUp, BefittingMyInspiration, AdventurerOfTalent, QuiteTheStatement, HeartRacingKrile, DelightVictoryKrile,
            FeatOldManWuk, SmilingBecauseSmiling, AllRightYouDidGreat, TryMyOwnHandWuk, PapaChallengeMatch, 
            StrategyKeyToVictory, GloryUponTuliyollal, DoneWell, FairPerformanceBetter, NotBadAtAll, HandWell, 
            BefittingStature, GoodEnoughGuess, ManyTalents, ThatsTheWay, NotSurprised, 
            LetAllCreationBeConsumed, InfernoSwelling, NotWalkAway, FunThanExpected];
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchVictoryMessage;
    }
}