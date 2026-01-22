using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class MaxBattleFeverEvent: PvPEvent
{
    public MaxBattleFeverEvent()
    {
        Name = "Gaining Battle High V / Flying High";
        InternalName = "MaxBattleFeverEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [WhatPower, RainOfDeath, DontStopNow, RoofCavedSuchDevastation, TyrannyTimeMasc, HerCharmsNotDeniedFem,
            GatheringAetherFem, UnusedOhMercyFem, DGFeverPich, YesYesJustSo, More, LetThisMomentLastForever, Hahahahahaha,
            Kill, Rend, KeepItUp, KeepUpTheOffensive, JustWhatWeNeeded, OnwardsAndUpwards, JustGettingStarted, AllAccordingToPlan, PraiseOrPity, KeepThemOnTheirHeels,
            StarsFavorUs, TheHandIsOurs, LivingLegends, GiveItEverything, FightHardNotHardEnough, CourageAndZeal, BefittingMyInspiration,
            GoodThingsWait, GettingInterestingWuk, YouReallyAreFamous, Impressive, NotHoldingAnythingBack, 
            KeepItUpWuk, NeverFailToImpress, EnjoyingYourself, EnjoyingThisArentYou, LetAllCreationBeConsumed, InfernoSwelling];
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is BattleHighMessage bhm)
        {
            return bhm.Level == 5;
        } 
        return message is FlyingHighMessage;
    }
}