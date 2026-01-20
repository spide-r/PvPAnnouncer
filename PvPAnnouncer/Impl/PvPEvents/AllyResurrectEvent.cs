using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent: PvPActorEvent
{
    public AllyResurrectEvent()
    {
        Name = "Resurrection";
        InternalName = "AllyResurrectEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [BackUpGrit, BackOnFeet, RisesAgain, WhatFightingSpirit, BackInAction, BattleNotOverDontDespair,
            ItsAliveLindwurm, ContinuesToMultiply, LWCompletelyHealed, SomethingRevolting, RiseOnceMore, VauntedFortitude,
            StillFightInYou, LifesFireAlreadySpent, DontStopNow, PraiseTheTwelve, PainfulThrashing, OutcomeWithHubris, 
            KeepYourChinUp, OnYourFeetAlphinaud, OnwardsAndUpwards, TwelveGiveMeStrength, KeepUpTheOffensive, LittleSetback, 
            BackInTheGame, OnYourFeetAlisaie, NotMostDisastrous, LadyLuckOtherIdeas, NotEasyRecovering, CreditWhereDue, StillDoThisDontGiveUp, 
            HowWeStrikeBack, RightBackToIt, WhereWereWe, SuchShouldBeFate, VicissitudesOfFortune,BodethIllTurnTide, ReviewOurStrategy, HoldFastFriend, 
            BeAtEase, HaveCare, SimplyNoKnowing, UnfortunateSuchIsLife, BedWithoutSupper, NotOverYetYshtola, RedoubleEfforts,
            MakeUpLostGround, AndNowToReturnTheFavor, BeStrong, SettleThisQuickly, HaveFaithMyFriend, AimForTheKill, DefeatedGreaterFoes, TumultuousDespair,
            PainStrengthPassion, HaventLostEdge, RepayThisDebt, BackToTheFray, WereNotDoneHere, FocusOnVictoryGraha, 
            TaskAtHandVictoryOurs, OhHowEmbarrassing, MattersHalfFinished, CowelToStir, TaleWillNotEnd, PullYourselfTogether, BackToTheFight,
            GoodProgressKrile, HeartRacingKrile, DontLoseHeartKrile, WontGiveUpOntoNext, ReclaimLostKrile,
            DoingFineKeepItUpWuk, DidntHoldBackWuk, TheyllPayForThatWuk, RightBackToItWuk, ChangeOfStrategyInOrder, 
            SharlayanApproachResults, ThoughtBetter, ComeOutOnTop, MinorSetback];
    }

    public override bool InvokeRule(IMessage m)
    {
        return m is UserResurrectedMessage;
    }

}
