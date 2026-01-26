using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PvPAnnouncer.Data;
using static AnnouncerLines;
using static ScionLines;
public abstract class InternalConstants
{
    public const string MessageTag = "PvPAnnouncer";

    public static readonly List<BattleTalk> LimitBreakList =
    [
        WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, NeitherSideHoldingBack, BattleElectrifying, 
        MjGameChanger, MjBoldMove, MjHeatingUp, AbsoluteBrutality, SuchFerocity, SomethingsComing, ThisEndsHere,
        RunBeastRun, DanceForMe, ThePowerToTranscend, StillFightInYou, ChangedRoutine,
        VauntedFortitude, BoundingFromWallToWallMasc, AssaultedRefMasc, FeralOnslaught, FelineFerocity, LitheAndLethal,
        BBEmbiggening, KaboomBBSpecial, UnusedBombarianPress, BBDesprate, DischargeAether, DGSteps, DGFeverPich, SRBringsWorkToLife,
        ChimericalFoe, FeralPowersWeapon, PunishingAttackFusion, VFWickedWeapon, VFUnleashedAether, EvenMoreAether, LightheadedFem,
        DBTaggingIn, Fusion, HoldingNothingBack, TyrannyTimeMasc, UnleashedFullMightMasc, ArmLifeOwnMasc, ArmSlitheringOutDisgustingMasc, CruelCoil,
        VictoryWithinReach, ReleaseYourPotential, YouHaveThem, ForTheHouseOfLeveilleur, FocusReleaseAlphinaud, SoItComesToThis, TheoryAndPractice,
        GiveItEverythingAlisaie, ShowThemWhatYoureMadeOf, ExpectedWhenSerious, KeepUpTheOffensive, JustGettingStarted, Fall, ThereIsNoEscapeAlisaie, KeepThemOnTheirHeels,
        DanceOfTheStars, ThereIsNoEscapeYshtola, AndNowToReturnTheFavor, HoldNothingBack, HaveThemNowEstinien, EndTheirStruggles,
        GiveItEverything, DeclareIntentions, MakeAStandOurselves, FinishThemTooMuch, PointsAndAll, FocusRelease, TimeHasCome, ShowMeWhatYouCanDo,
        FinishingTouchKrile, GotThemNowWuk, CantPassChanceWuk, WillStopThemWuk, NoHoldingBackWukMahjong, NoHoldingBackWuk, Impressive, IntentionsKnown, 
        EvenTheOdds, HaveThemStrike, ShowThemTheirPlace, ThereYouHaveThem
    ];
    
    
    public static List<T?> GetStaticReadOnlyFields<T>(Type classType)
    {
        if (classType == null) throw new ArgumentNullException(nameof(classType));
        return classType
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => (T)f.GetValue(null))
            .ToList();
    }
}