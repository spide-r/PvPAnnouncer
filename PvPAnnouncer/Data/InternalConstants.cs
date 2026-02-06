using System.Collections.Generic;
using System.Linq;

namespace PvPAnnouncer.Data;
using static AnnouncerLines;
using static ScionLines;
public abstract class InternalConstants
{
    public const string MessageTag = "PvPAnnouncer";
    public const uint PvPAnnouncerDevIcon = 73282;
    public const string PvPAnnouncerDevName = "PvPAnnouncer Dev";
    public const string ErrorContactDev = "Uh oh! You shouldn't see this! Contact the PvPAnnouncer dev!";

    public static readonly List<Shoutcast> LimitBreakList =
    [
        WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, NeitherSideHoldingBack, BattleElectrifying, 
        MjGameChanger, MjBoldMove, MjHeatingUp, AbsoluteBrutality, SuchFerocity, SomethingsComing, ThisEndsHere,
        RunBeastRun, DanceForMe, ThePowerToTranscend, StillFightInYou, ChangedRoutine,
        VauntedFortitude, BoundingFromWallToWallMasc, AssaultedRefMasc, FeralOnslaught, FelineFerocity, LitheAndLethal,
        BBEmbiggening, KaboomBBSpecial, UnusedBombarianPress, BBDesprate, DischargeAether, DGSteps, DGFeverPich, SRBringsWorkToLife,
        ChimericalFoe, FeralPowersWeapon, PunishingAttackFusion, VFWickedWeapon, VFUnleashedAether, EvenMoreAether, LightheadedFem,
        DBTaggingIn, Fusion, HoldingNothingBack, TyrannyTimeMasc, UnleashedFullMightMasc, ArmLifeOwnMasc, ArmSlitheringOutDisgustingMasc, CruelCoil,
        VictoryWithinReach, ReleaseYourPotential, YouHaveThem, ForTheHouseOfLeveilleur, FocusReleaseAlphinaud, SoItComesToThis, TheoryInPractice,
        GiveItEverythingAlisaie, ShowThemWhatYoureMadeOf, ExpectedWhenSerious, KeepUpTheOffensive, JustGettingStarted, Fall, ThereIsNoEscapeAlisaie, KeepThemOnTheirHeels,
        DanceOfTheStars, ThereIsNoEscapeYshtola, AndNowToReturnTheFavor, HoldNothingBack, HaveThemNowEstinien, EndTheirStruggles,
        GiveItEverything, DeclareIntentions, MakeAStandOurselves, FinishThemTooMuch, PointsAndAll, FocusRelease, TimeHasCome, ShowMeWhatYouCanDo,
        FinishingTouchKrile, GotThemNowWuk, CantPassChanceWuk, WillStopThemWuk, NoHoldingBackWukMahjong, NoHoldingBackWuk, Impressive, IntentionsKnown, 
        EvenTheOdds, HaveThemStrike, ShowThemTheirPlace, ThereYouHaveThem
    ];
    
    public static readonly List<string> LimitBreakListStr =
    [
        "WhatPower", "PotentMagicks", "WhatAClash", "ThrillingBattle", "NeitherSideHoldingBack", "BattleElectrifying",
        "MjGameChanger", "MjBoldMove", "MjHeatingUp", "AbsoluteBrutality", "SuchFerocity", "SomethingsComing",
        "ThisEndsHere",
        "RunBeastRun", "DanceForMe", "ThePowerToTranscend", "StillFightInYou", "ChangedRoutine",
        "VauntedFortitude", "BoundingFromWallToWallMasc", "AssaultedRefMasc", "FeralOnslaught", "FelineFerocity",
        "LitheAndLethal",
        "BBEmbiggening", "KaboomBBSpecial", "UnusedBombarianPress", "BBDesprate", "DischargeAether", "DGSteps",
        "DGFeverPich", "SRBringsWorkToLife",
        "ChimericalFoe", "FeralPowersWeapon", "PunishingAttackFusion", "VFWickedWeapon", "VFUnleashedAether",
        "EvenMoreAether", "LightheadedFem",
        "DBTaggingIn", "Fusion", "HoldingNothingBack", "TyrannyTimeMasc", "UnleashedFullMightMasc", "ArmLifeOwnMasc",
        "ArmSlitheringOutDisgustingMasc", "CruelCoil",
        "VictoryWithinReach", "ReleaseYourPotential", "YouHaveThem", "ForTheHouseOfLeveilleur", "FocusReleaseAlphinaud",
        "SoItComesToThis", "TheoryInPractice",
        "GiveItEverythingAlisaie", "ShowThemWhatYoureMadeOf", "ExpectedWhenSerious", "KeepUpTheOffensive",
        "JustGettingStarted", "Fall", "ThereIsNoEscapeAlisaie", "KeepThemOnTheirHeels",
        "DanceOfTheStars", "ThereIsNoEscapeYshtola", "AndNowToReturnTheFavor", "HoldNothingBack", "HaveThemNowEstinien",
        "EndTheirStruggles",
        "GiveItEverything", "DeclareIntentions", "MakeAStandOurselves", "FinishThemTooMuch", "PointsAndAll",
        "FocusRelease", "TimeHasCome", "ShowMeWhatYouCanDo",
        "FinishingTouchKrile", "GotThemNowWuk", "CantPassChanceWuk", "WillStopThemWuk", "NoHoldingBackWukMahjong",
        "NoHoldingBackWuk", "Impressive", "IntentionsKnown",
        "EvenTheOdds", "HaveThemStrike", "ShowThemTheirPlace", "ThereYouHaveThem"
    ];

    public static Shoutcast[] GetBattleTalkList()
    {
        var s = typeof(ScionLines).GetFields().Where(info => info.FieldType == typeof(Shoutcast))
            .Select(info => (Shoutcast) info.GetValue(null)!).ToList();
        var a = typeof(AnnouncerLines).GetFields().Where(info => info.FieldType == typeof(Shoutcast))
            .Select(info => (Shoutcast) info.GetValue(null)!).ToList();
        return  s.Concat(a).ToArray();
    }
}