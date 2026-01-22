using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchLossEvent: PvPMatchEvent
{
    public MatchLossEvent()
    {
        Name = "Match Loss";
        InternalName = "MatchLossEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [MjPainfulToWatch, MjCommendableEffort, MjCompetitionTooMuch, MjUtterlyHumiliated,
            OutcomeWithHubris, PainfulThrashing, LearnAndForgeOn, ProudYetIgnorant, ReplaceIgnoranceWithKnowledge, 
            OutplayedClearMinds, TakeFirstPlaceNextTime, LaughingInTheEnd, FellJustShortThancred, CouldHaveBeenWorse, DrinkBestOfUs, KeepChinUpPrettyLeast, 
            DestroyerMightyBlow, NarrowestOfDefeats, OpponentsFormidableIndeed, TriflingUntoLosing, ShoulderToLeanUpon, BestEffortWellDone,
            BedWithoutSupper, WeCanDoBetterYshtola, HoneOurWits, DisappointingDefeatGrowStronger, OffWithYouSleep, DidntBearBrunt, ShouldHaveBeenUs,
            ClaimVictoryNextTime, CapableOfMoreEstinien, TogetherRiseAgain, DrinkAndDriedSquid, BeatUsToItNextTime, WillDoEvenBetter,
            AllaganGameGraha, SomethingToTeachDefeat, ThatsTooBadKrile, DidntHandItToThem, ManagedSoPoorly, WontGiveUpOntoNext, 
            ThrillingGameNextTime, ExploringIntricacies, PlayAgainWinOrLose, ToughLossLearnKrile, DidntHoldBackWuk, GonnaBeSickWuk, 
            WinSomeLoseSomeWuk, LearningExperienceWuk, FairPerformanceBetter, FaceLamatyi, HowCanThisBe, NoNo, ScoreIsntRight,
            ChangeOfStrategyInOrder, OccasionalLossInevitable, ProudOfYou, ProtectFromFutureDefeat, UnworthyVowOfReason, ThreeHeadsStupid, 
            SmashTable, CouldHaveBeenWorseErenville, LifeGoYourWay, FairGo, DontPlayGames, PresenceDistraction, MerelyBadHand, Hahahahahaha, LifesFireAlreadySpent, HydaelynsChosenMuster];
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchLossMessage;
    }
}