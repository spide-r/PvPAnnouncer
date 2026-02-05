using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyDeathEvent : PvPActorControlEvent
{
    public AllyDeathEvent()
    {
        Name = "Deaths";
        InternalName = "AllyDeathEvent";
    }
    public override List<Shoutcast> SoundPaths()
    {
        return [TheyreDownIsItOver, ChallengerDownIsThisEnd, TooMuch, WentDownHard, 
            CouldntAvoid, BeenFelled, TheyreDownCanTheyRecover, ChampCrushed, MjGoodnessGracious, MjPainfulToWatch, 
            MjChallengerDownHardGentle, MjDownNotOut, HaveYouTheStrength, HydaelynsChosenMuster, LifesFireAlreadySpent, 
            ConfoundIt, OutcomeWithHubris, WhatANuisance, WontLetThemGetAway, WinThisYet, WontSoonForget, 
            NothingCantRecoverFrom, LittleSetback, ThisIsNotOver, ChanceWillCome, GainedOnUsWinInEnd, SomeCompetitionNow, ThatIsntGood, NotEasyRecovering,
            SuchShouldBeFate, VicissitudesOfFortune, BodethIllTurnTide, ReviewOurStrategy, SimplyNoKnowing, UnfortunateSuchIsLife, BedWithoutSupper,
            DontBeSoReckless, BeMoreCareful, LeftOurselvesOpen, TumultuousDespair, AreYouAlrightGraha, OhHowEmbarrassing, AreYouAlright, CantBeRightKrile,
            HowDidThatHappenWuk, GonnaBeSickWuk, FeelYourPainWuk, DidntHoldBackWuk, CanDoBetterWuk, DamnItAll, CantBe, DammitAll, StepBehind, 
            ErrorInCalculations, FallingBehind, LuckWontLast, Competition, Unfortunate, DontPlayGames, OverestimatedPotential];
    }

    public override bool InvokeRule(IMessage m)
    {
        if (m is ActorControlMessage message)
        {
            if (PluginServices.PvPMatchManager.IsMonitoredUser(message.EntityId))
            {
                return message.GetCategory() == ActorControlCategory.Death;

            }
        }
        return m is UserDiedMessage;
    }


    public override ActorControlCategory ActorControlCategory { get; init; } = ActorControlCategory.Death;
}
