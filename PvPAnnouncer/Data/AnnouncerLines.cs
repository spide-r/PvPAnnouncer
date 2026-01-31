using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Data;
#pragma warning disable 8618
public static class AnnouncerLines
{
    
    //Announcer sound path: sound/voice/vo_line/INSERT_NUMBER_HERE_en.scd
    // en, de, fr, ja work
    
    //potentially _kr, _chs (chinese simplified), 
    
    //Lines are found in ContentDirectorBattleTalk as well as InstanceContentTextData - Use AlphaAOT 
    
    //trust lines are found in NpcYell
    
    //todo: huge recovery from earths reply/microcosmos (regen voice lines) (8206125 for huge heal from others, 8206122 from pot?)

    private static string GetVoPath(string announcement, string lang)
    {
        return "sound/voice/vo_line/" + announcement + "_" + lang + ".scd";
    }

    private static readonly string Metem = "Metem";
    private static readonly uint MetemI = 073287;
    
    #region Intros
    public static BattleTalk IntroBc;
    public static BattleTalk IntroBb;
    public static BattleTalk IntroHbl;
    public static BattleTalk IntroWt;
    public static BattleTalk VictoryWt;
    public static BattleTalk IntroDg;
    public static BattleTalk IntroSr;
    public static BattleTalk IntroBa;
    public static BattleTalk IntroHb;
    public static BattleTalk IntroVf;
    public static BattleTalk IntroDbRh;
    public static BattleTalk IntroTt;
    public static BattleTalk IntroLw;
    #endregion
    #region Generic
    public static BattleTalk GenericVictory;
    public static BattleTalk NewGcBornVictory;
    public static BattleTalk PresDefeated;
    public static BattleTalk RobotKo; //todo this KO doesnt work because its not metem - look for it!!!!!!
    public static BattleTalk ViciousBlow;
    public static BattleTalk FeltThatOneStillStanding;
    public static BattleTalk BeautifullyDodged;
    public static BattleTalk SawThroughIt;
    public static BattleTalk WentDownHard;
    public static BattleTalk TheyreDownIsItOver;
    public static BattleTalk BackUpGrit;
    public static BattleTalk StillInIt;
    public static BattleTalk WhatPower;
    public static BattleTalk PotentMagicks;
    public static BattleTalk IroncladDefense;
    public static BattleTalk Fallen;
    public static BattleTalk AllOverUntilNextTime;
    public static BattleTalk StruckSquare;
    public static BattleTalk Oof;
    public static BattleTalk CouldntAvoid;
    public static BattleTalk MustHaveHurtNotOut;
    public static BattleTalk EffortlesslyDodged;
    public static BattleTalk ClearlyAnticipated;
    public static BattleTalk StylishEvasion;
    public static BattleTalk AvoidedWithEase;
    public static BattleTalk TooMuch;
    public static BattleTalk ChallengerDownIsThisEnd;
    public static BattleTalk RisesAgain;
    public static BattleTalk BackOnFeet;
    public static BattleTalk OofMustHaveHurt;
    public static BattleTalk NotFastEnough;
    public static BattleTalk CantBeCareless;
    public static BattleTalk DirectHitStillStanding;
    public static BattleTalk ImpressiveFootwork;
    public static BattleTalk DancingAwayUnharmed;
    public static BattleTalk AnotherAttackEvaded;
    public static BattleTalk SlippedBeyondReach;
    public static BattleTalk BeenFelled;
    public static BattleTalk TheyreDownCanTheyRecover;
    public static BattleTalk WhatFightingSpirit;
    public static BattleTalk BackInAction;
    #endregion
    #region BlackCat
    public static BattleTalk FirstOpponent;
    public static BattleTalk FelineFerocity;
    public static BattleTalk LitheAndLethal;
    public static BattleTalk NineLives;
    public static BattleTalk FeralOnslaught;
    public static BattleTalk MyRing;
    public static BattleTalk RepairRing;
    #endregion
    #region HoneyB
    public static BattleTalk HoneyBShowBegun;
    public static BattleTalk SavorSting;
    public static BattleTalk ChangedRoutine;
    public static BattleTalk VenomStrikeFem;
    public static BattleTalk FeelingLoveFem;
    public static BattleTalk ResistTheIrresistible;
    public static BattleTalk HerCharmsNotDeniedFem;
    public static BattleTalk WhatAClash;
    #endregion
    #region BruteB
    public static BattleTalk BBMuscled;
    public static BattleTalk BBEmbiggening;
    public static BattleTalk KaboomBBSpecial;
    public static BattleTalk BBDesprate;
    public static BattleTalk BannedCompoundRobot;
    public static BattleTalk AssaultedRefMasc;
    public static BattleTalk FuseField;
    public static BattleTalk ChainDeathmatch;
    #endregion
    #region WickedT
    public static BattleTalk WTReturned;
    public static BattleTalk SomethingGrowingFem;
    public static BattleTalk GatheringAetherFem;
    public static BattleTalk ConvertAetherFem;
    public static BattleTalk MassiveCannonFem;
    public static BattleTalk GrownWingsFem;
    public static BattleTalk UnleashedANewFeralSoul;
    public static BattleTalk DischargeAether;
    public static BattleTalk BattleElectrifying;
    #endregion
    #region DancingG
    public static BattleTalk DGSteps;
    public static BattleTalk UpstartBegins;
    public static BattleTalk InvitationToDance;
    public static BattleTalk DGFeverTakenHold;
    public static BattleTalk DGFeverPich;
    #endregion
    #region SugarR
    public static BattleTalk SRGallery;
    public static BattleTalk SRBringsWorkToLife;
    public static BattleTalk Quicksand;
    public static BattleTalk River;
    public static BattleTalk StormParasols;
    public static BattleTalk Lava;
    public static BattleTalk RingBecomeDesert;
    public static BattleTalk StormOfNeedles;
    public static BattleTalk SuckedIn;
    public static BattleTalk Thunderstorm;
    public static BattleTalk TransformativePiece;
    #endregion
    #region BruteA
    public static BattleTalk BAMeansBusiness;
    public static BattleTalk ChimericalFoe;
    public static BattleTalk FeralPowersWeapon;
    public static BattleTalk FiendishFlora;
    public static BattleTalk CamerasPetrified;
    public static BattleTalk BAHanging;
    public static BattleTalk BBLariat;
    public static BattleTalk PunishingAttackFusion;
    public static BattleTalk SentRivalFlyingMasc;
    public static BattleTalk BoundingFromWallToWallMasc;
    public static BattleTalk RoofCavedSuchDevastation;
    public static BattleTalk SendingCameras;
    public static BattleTalk StartedFire;
    public static BattleTalk DodgedEverything;
    public static BattleTalk BrutalBlow;
    public static BattleTalk ThrillingBattle;
    #endregion
    #region HowlingB
    public static BattleTalk WolfLair;
    public static BattleTalk SuchSpeedMasc;
    public static BattleTalk ColossalThing;
    public static BattleTalk ColossusSwordMasc;
    public static BattleTalk SomethingsAmissMasc;
    public static BattleTalk RingDestroyedFallen;
    public static BattleTalk MovedToFloatingDeathtraps;
    public static BattleTalk DestroyedIsle;
    public static BattleTalk NeitherSideHoldingBack;
    #endregion
    #region VampF
    public static BattleTalk VFFeastEyes;
    public static BattleTalk FallPreyCruelMistress;
    public static BattleTalk VFWickedWeapon;
    public static BattleTalk NowhereLeft;
    public static BattleTalk DrainingAudienceFem;
    public static BattleTalk VFUnleashedAether;
    public static BattleTalk SpectacleResumes;
    public static BattleTalk RainOfDeath;
    public static BattleTalk DrainingCrowd;
    public static BattleTalk EvenMoreAether;
    public static BattleTalk LightheadedFem;
    public static BattleTalk SiphonedAether;
    #endregion
    #region DeepBRedH
    public static BattleTalk TagTeam;
    public static BattleTalk SuchScorn;
    public static BattleTalk FearsomeFlamesMasc;
    public static BattleTalk FlamesSpreading;
    public static BattleTalk DBTaggingIn;
    public static BattleTalk EnormousWave;
    public static BattleTalk BothBrosEntered;
    public static BattleTalk DoubleTrouble;
    public static BattleTalk Fusion;
    public static BattleTalk HoldingNothingBack;
    #endregion
    #region TTyrant
    public static BattleTalk GrandChampion;
    public static BattleTalk TTThrone;
    public static BattleTalk TyrannyTimeMasc;
    public static BattleTalk HoldingTheirOwn;
    public static BattleTalk AllTheseWeaponsMasc;
    public static BattleTalk AbsoluteBrutality;
    public static BattleTalk SuchFerocity;
    public static BattleTalk TTAlteredFormMasc;
    public static BattleTalk UnleashedFullMightMasc;
    public static BattleTalk SplitRingHalfMasc;
    public static BattleTalk ThrownRingMasc;
    public static BattleTalk RingRestored;
    public static BattleTalk EitherSide;
    #endregion
    #region TPresident
    public static BattleTalk PresidentMustPay;
    public static BattleTalk ArmLifeOwnMasc;
    public static BattleTalk ArmSlitheringOutDisgustingMasc;
    public static BattleTalk LWScatteredGore;
    public static BattleTalk CruelCoil;
    public static BattleTalk ChampCrushed;
    public static BattleTalk RoundRingMasc;
    public static BattleTalk RingPieces;
    public static BattleTalk GoreLatched;
    public static BattleTalk LWCompletelyHealed;
    public static BattleTalk LWOutOfControl;
    public static BattleTalk PowerRunAmokMasc;
    public static BattleTalk RegenCapacityMasc;
    public static BattleTalk StruckWithVenon;
    public static BattleTalk SomethingRevolting;
    public static BattleTalk VictoryChamp;
    #endregion
    #region Unused
    public static BattleTalk UnusedNoRespectMasc;
    public static BattleTalk UnusedUpOnThePost;
    public static BattleTalk UnusedBombarianPress;
    public static BattleTalk UnusedOhMercyFem;
    public static BattleTalk UnusedBackTail;
    #endregion
    
    #region Mahjong
    public static BattleTalk MjGameChanger;
    public static BattleTalk MjBoldMove;
    public static BattleTalk MjChallengerRecover;
    public static BattleTalk MjRivalVying;
    public static BattleTalk MjHeatingUp;
    public static BattleTalk MjHandDecided;
    public static BattleTalk MjKissLadyLuck;
    public static BattleTalk MjGoodnessGracious;
    public static BattleTalk MjOurTile;
    public static BattleTalk MjPainfulToWatch;
    public static BattleTalk MjClobberedWithTable;
    public static BattleTalk MjBeautifullyPlayed;
    public static BattleTalk MjMadeYourMark;
    public static BattleTalk MjDontStandAChance;
    public static BattleTalk MjChallengerDownHardGentle;
    public static BattleTalk MjStillInItGentle;
    public static BattleTalk MjStillStandingGentle;
    public static BattleTalk MjDownNotOut;
    public static BattleTalk MjTitanOfTable;
    public static BattleTalk MjCommendableEffort;
    public static BattleTalk MjReportingLive;
    public static BattleTalk MjCompetitionTooMuch;
    public static BattleTalk MjUtterlyHumiliated;
    #endregion
    
    #region M12sp2
    public static BattleTalk LindwurmsHeart;
    public static BattleTalk BadFeeling;
    public static BattleTalk Transforming;
    public static BattleTalk ItsAliveLindwurm;
    public static BattleTalk BattleNotOverDontDespair;
    public static BattleTalk CreatingMore;
    public static BattleTalk ContinuesToMultiply;
    public static BattleTalk CuriousProps;
    public static BattleTalk TakenOnChampionForm;
    public static BattleTalk DejaVu;
    public static BattleTalk RingRendInTwo;
    public static BattleTalk MutationCorrupting;
    public static BattleTalk SomethingsComing;
    public static BattleTalk WarpedFabric;
    public static BattleTalk AnotherDimension;
    public static BattleTalk HardPressed;
    public static BattleTalk FeverPitch;
    public static BattleTalk StayStrong;
    public static BattleTalk NotSureIUnderstand;
    public static BattleTalk LeftRealmOfLogic;
    public static BattleTalk DefiesComprehension;
    #endregion
    
    // I'd like to thank IntelliJ Smart Align Plugin for making all of this readable
    public static void Init(IBattleTalkFactory factory)
    {
        var onlyMetem       = new List<Personalization> { Personalization.MetemAnnouncer };
        var blackCat        = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.BlackCat };
        var honeyBee        = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.HoneyBLovely };
        var bruteBomber     = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.BruteBomber };
        var wickedThunder   = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.WickedThunder };
        var dancingGreen    = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.DancingGreen };
        var sugarRiot       = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.SugarRiot };
        var bruteAbominator = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.BruteAbominator };
        var howlingBlade    = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.HowlingBlade };
        var vampFatale      = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.VampFatale };
        var deepBlueRedHot  = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.DeepBlueRedHot };
        var theTyrant       = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.Tyrant };
        var thePresident    = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.President };
        var femPronouns     = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.FemPronouns };
        var mascPronouns    = new List<Personalization> { Personalization.MetemAnnouncer   , Personalization.MascPronouns };
        // === Intro Lines === 
        IntroBc   = CreateMetemCsLine("TEXT_VOICEMAN_07010_000010_METEM", [Personalization.MetemAnnouncer, Personalization.BlackCat], factory);
        IntroBb   = CreateMetemCsLine("TEXT_VOICEMAN_07010_000050_METEM", [Personalization.MetemAnnouncer, Personalization.BruteBomber], factory);
        IntroHbl  = CreateMetemCsLine("TEXT_VOICEMAN_07010_000040_METEM", [Personalization.MetemAnnouncer, Personalization.HoneyBLovely], factory);
        IntroWt   = CreateMetemCsLine("TEXT_VOICEMAN_07010_000060_METEM", [Personalization.MetemAnnouncer, Personalization.WickedThunder], factory);
        IntroDg   = CreateMetemCsLine("TEXT_VOICEMAN_07210_000010_METEM", [Personalization.MetemAnnouncer, Personalization.DancingGreen], factory);
        IntroSr   = CreateMetemCsLine("TEXT_VOICEMAN_07210_000020_METEM", [Personalization.MetemAnnouncer, Personalization.SugarRiot], factory);
        IntroBa   = CreateMetemCsLine("TEXT_VOICEMAN_07210_000030_METEM", [Personalization.MetemAnnouncer, Personalization.BruteAbominator], factory);
        IntroHb   = CreateMetemCsLine("TEXT_VOICEMAN_07210_000040_METEM", [Personalization.MetemAnnouncer, Personalization.HowlingBlade], factory);
        IntroVf   = CreateMetemCsLine("TEXT_VOICEMAN_07410_000010_METEM", [Personalization.MetemAnnouncer, Personalization.VampFatale], factory);
        IntroDbRh = CreateMetemCsLine("TEXT_VOICEMAN_07410_000020_METEM", [Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot], factory);
        IntroTt   = CreateMetemCsLine("TEXT_VOICEMAN_07410_000050_METEM", [Personalization.MetemAnnouncer, Personalization.Tyrant], factory);
        IntroLw   = CreateMetemCsLine("TEXT_VOICEMAN_07410_000080_METEM", [Personalization.MetemAnnouncer, Personalization.President], factory);

        // === Victory ===
        VictoryWt        = CreateMetemCsLine("TEXT_VOICEMAN_07010_000080_METEM", [Personalization.MetemAnnouncer], factory);
        GenericVictory   = CreateMetemCsLine("TEXT_VOICEMAN_07010_000030_METEM", [Personalization.MetemAnnouncer], factory);
        NewGcBornVictory = CreateMetemCsLine("TEXT_VOICEMAN_07410_000070_METEM", [Personalization.MetemAnnouncer], factory);
        PresDefeated     = CreateMetemCsLine("TEXT_VOICEMAN_07410_000090_METEM", [Personalization.MetemAnnouncer], factory);
        
        RobotKo = factory.CreateFromCutsceneLine("Referee", 5, 6, "TEXT_VOICEMAN_07010_000020_REFEREE",
            onlyMetem);
        BannedCompoundRobot  = factory.CreateFromContentDirectorBattleTalk("Referee", 8205440, onlyMetem);  // Banned compound detected. Combatant disqualified.

        
        // == Generic Lines == 
        ViciousBlow                    = CreateMetemBtLine(8205341, onlyMetem      , factory); // A vicious blow! That'll leave a mark!
        FeltThatOneStillStanding       = CreateMetemBtLine(8205342, onlyMetem      , factory); // Even I felt that one! But the challenger's still standing!
        BeautifullyDodged              = CreateMetemBtLine(8205343, onlyMetem      , factory); // Beautifully dodged!
        SawThroughIt                   = CreateMetemBtLine(8205344, onlyMetem      , factory); // Amazing! The challenger saw straight through it!
        WentDownHard                   = CreateMetemBtLine(8205345, onlyMetem      , factory); // Oh my! The challenger went down hard!
        TheyreDownIsItOver             = CreateMetemBtLine(8205346, onlyMetem      , factory); // And they're down! Is it over already!?
        BackUpGrit                     = CreateMetemBtLine(8205347, onlyMetem      , factory); // They're back on their feet! What grit!
        StillInIt                      = CreateMetemBtLine(8205348, onlyMetem      , factory); // They're still in it! But for how long!?
        WhatPower                      = CreateMetemBtLine(8205349, onlyMetem      , factory); // What power! Looks like someone isn't holding back!
        PotentMagicks                  = CreateMetemBtLine(8205350, onlyMetem      , factory); // Such potent magicks! But will they be enough to turn the tide?
        IroncladDefense                = CreateMetemBtLine(8205351, onlyMetem      , factory); // They're bracing for the storm with an ironclad defense!
        Fallen                         = CreateMetemBtLine(8205352, onlyMetem      , factory); // Oh dear! The challenger has fallen out of the ring!
        AllOverUntilNextTime           = CreateMetemBtLine(8205388, onlyMetem      , factory); // It's all over...until next time!
        StruckSquare                   = CreateMetemBtLine(8205772, onlyMetem      , factory); // That struck square!
        Oof                            = CreateMetemBtLine(8205773, onlyMetem      , factory); // Oof! Will the challenger be alright!?
        CouldntAvoid                   = CreateMetemBtLine(8205774, onlyMetem      , factory); // Oh no, they couldn't avoid that one!
        MustHaveHurtNotOut             = CreateMetemBtLine(8205775, onlyMetem      , factory); // That must have hurt! But they're not out of the fight yet!
        EffortlesslyDodged             = CreateMetemBtLine(8205776, onlyMetem      , factory); // Effortlessly dodged!
        ClearlyAnticipated             = CreateMetemBtLine(8205777, onlyMetem      , factory); // Amazing! They clearly anticipated that attack!
        StylishEvasion                 = CreateMetemBtLine(8205778, onlyMetem      , factory); // A stylish evasion, well done!
        AvoidedWithEase                = CreateMetemBtLine(8205779, onlyMetem      , factory); // Avoided with ease! They knew just what to do!
        TooMuch                        = CreateMetemBtLine(8205780, onlyMetem      , factory); // Oh no, that was too much for the challenger!
        ChallengerDownIsThisEnd        = CreateMetemBtLine(8205781, onlyMetem      , factory); // The challenger's down! Is this the end!?
        RisesAgain                     = CreateMetemBtLine(8205782, onlyMetem      , factory); // The challenger rises again! Can they turn this around!?
        BackOnFeet                     = CreateMetemBtLine(8205783, onlyMetem      , factory); // They're back on their feet! Let's see if they can stay standing!
        OofMustHaveHurt                = CreateMetemBtLine(8206066, onlyMetem      , factory); // Oof, that must've hurt! Hang in there!
        NotFastEnough                  = CreateMetemBtLine(8206067, onlyMetem      , factory); // Not fast enough, I'm afraid!
        CantBeCareless                 = CreateMetemBtLine(8206068, onlyMetem      , factory); // Even I felt that! Can't afford to be careless, now!
        DirectHitStillStanding         = CreateMetemBtLine(8206069, onlyMetem      , factory); // A direct hit! But they're still standing!
        ImpressiveFootwork             = CreateMetemBtLine(8206070, onlyMetem      , factory); // Impressive footwork on display today!
        DancingAwayUnharmed            = CreateMetemBtLine(8206071, onlyMetem      , factory); // And they dance out of the way unharmed! Unbelievable!
        AnotherAttackEvaded            = CreateMetemBtLine(8206072, onlyMetem      , factory); // Another attack evaded with aplomb!
        SlippedBeyondReach             = CreateMetemBtLine(8206073, onlyMetem      , factory); // Once again they slip beyond their opponent's reach!
        BeenFelled                     = CreateMetemBtLine(8206074, onlyMetem      , factory); // Oh no, they've been felled with a vengeance!
        TheyreDownCanTheyRecover       = CreateMetemBtLine(8206075, onlyMetem      , factory); // And they're down! Can they recover from this?
        WhatFightingSpirit             = CreateMetemBtLine(8206076, onlyMetem      , factory); // They're up! What fighting spirit!
        BackInAction                   = CreateMetemBtLine(8206077, onlyMetem      , factory); // And they're back in action! Can they turn the tide!?
        
        // == Black Cat == 
        FirstOpponent                  = CreateMetemBtLine(8205353, blackCat       , factory); // Our challenger's first opponent: Black Cat!
        FelineFerocity                 = CreateMetemBtLine(8205354, blackCat       , factory); // Can they hold their own against her feline ferocity!?
        LitheAndLethal                 = CreateMetemBtLine(8205355, blackCat       , factory); // Here it comes! Black Cat's lithe and lethal maneuver! 
        NineLives                      = CreateMetemBtLine(8205358, blackCat       , factory); // Look alive, folks, because this cat has nine.
        FeralOnslaught                 = CreateMetemBtLine(8205360, blackCat       , factory); // It begins─Black Cat's feral onslaught!
        MyRing                         = CreateMetemBtLine(8205356, onlyMetem      , factory); // My riiing!
        RepairRing                     = CreateMetemBtLine(8205357, onlyMetem      , factory); // Ahem. Please bear with us while we repair the ring.
        
        // === Honey B. Lovely ===
        HoneyBShowBegun                = CreateMetemBtLine(8205361, honeyBee       , factory); // The Honey B. Lovely show has begun!
        SavorSting                     = CreateMetemBtLine(8205363, honeyBee       , factory); // Now to savor the sweet sting of Honey B.'s charms.
        ChangedRoutine                 = CreateMetemBtLine(8205367, honeyBee       , factory); // What's this? Honey B. has changed her routine!
        VenomStrikeFem                 = CreateMetemBtLine(8205362, femPronouns    , factory); // Here comes her fearsome venom strike!
        FeelingLoveFem                 = CreateMetemBtLine(8205364, femPronouns    , factory); // Oh, she's definitely feeling your love!
        ResistTheIrresistible          = CreateMetemBtLine(8205365, onlyMetem      , factory); // Can the challenger resist the irresistible!? I know I can't.
        HerCharmsNotDeniedFem          = CreateMetemBtLine(8205366, femPronouns    , factory); // Alas, her charms were not to be denied!
        WhatAClash                     = CreateMetemBtLine(8205368, onlyMetem      , factory); // What a clash! Neither side is willing to yield!
        
        // === Brute Bomber ===
        BBMuscled                      = CreateMetemBtLine(8205369, bruteBomber    , factory); // The Brute Bomber has muscled his way into the ring!
        BBEmbiggening                  = CreateMetemBtLine(8205371, bruteBomber    , factory); // The Brute Bomber is embiggening himself!
        KaboomBBSpecial                = CreateMetemBtLine(8205375, bruteBomber    , factory); // Kaboom! The Bombarian Special!
        BBDesprate                     = CreateMetemBtLine(8205378, bruteBomber    , factory); // The Brute Bomber is desperate!
        AssaultedRefMasc               = CreateMetemBtLine(8205370, mascPronouns   , factory); // My word, he just assaulted the referee! The scoundrel!
        FuseField                      = CreateMetemBtLine(8205376, onlyMetem      , factory); // The infernal Fusefield! How will the challenger respond!?
        ChainDeathmatch                = CreateMetemBtLine(8205377, onlyMetem      , factory); // Oho, Chain Deathmatch! There's no escaping those adamant links!
    
        // === Wicked Thunder ===
        WTReturned                     = CreateMetemBtLine(8205379, wickedThunder  , factory); // Wicked Thunder has returned to the ring!
        SomethingGrowingFem            = CreateMetemBtLine(8205383, femPronouns    , factory); // Egads! Something is growing from her body!
        GatheringAetherFem             = CreateMetemBtLine(8205385, femPronouns    , factory); // She's gathering aether...but to what end!?
        ConvertAetherFem               = CreateMetemBtLine(8205386, femPronouns    , factory); // She's using electrope to convert her body's aether!
        MassiveCannonFem               = CreateMetemBtLine(8205380, femPronouns    , factory); // Oooh, look at that massive cannon! Her skills are as sharp as ever!
        GrownWingsFem                  = CreateMetemBtLine(8205381, femPronouns    , factory); // She's grown wings! How wickedly divine!
        UnleashedANewFeralSoul         = CreateMetemBtLine(8205384, femPronouns    , factory); // She's taken electrope into her body and unleashed a new feral soul!
        DischargeAether                = CreateMetemBtLine(8205382, wickedThunder  , factory); // Wicked Thunder is discharging vast amounts of aether!
        BattleElectrifying             = CreateMetemBtLine(8205387, onlyMetem      , factory); // A battle so electrifying I dare not blink!
        
        // === Dancing Green ===
        DGSteps                        = CreateMetemBtLine(8205785, dancingGreen   , factory); // Dancing Green is pulling out all the stops with these steps!
        UpstartBegins                  = CreateMetemBtLine(8205784, onlyMetem      , factory); // The upstart's cruiserweight campaign begins!
        InvitationToDance              = CreateMetemBtLine(8205786, onlyMetem      , factory); // An invitation to dance! How will the challenger respond?
        DGFeverTakenHold               = CreateMetemBtLine(8205787, dancingGreen   , factory); // The fever's taken hold! Can the upstart keep up with Dancing Green?
        DGFeverPich                    = CreateMetemBtLine(8205788, dancingGreen   , factory); // The night's reached a fever pitch! All eyes are on Dancing Green!
        
        // === Sugar Riot ===
        SRGallery                      = CreateMetemBtLine(8205789, sugarRiot      , factory); // Welcome to Sugar Riot's gallery of glamours!
        SRBringsWorkToLife             = CreateMetemBtLine(8205790, sugarRiot      , factory); // And with a flourish, Sugar Riot brings her work to life!
        Quicksand                      = CreateMetemBtLine(8205793, onlyMetem      , factory); // Quicksand!? You don't want to get caught in that!
        River                          = CreateMetemBtLine(8205795, onlyMetem      , factory); // A crystal-clear river! How charming!
        StormParasols                  = CreateMetemBtLine(8205797, onlyMetem      , factory); // A massive, raging storm! I hope you brought your parasols!
        Lava                           = CreateMetemBtLine(8205798, onlyMetem      , factory); // Lava is ravaging the ring! Talk about destructive creativity!
        RingBecomeDesert               = CreateMetemBtLine(8205791, onlyMetem      , factory); // Goodness, the ring has become a desert!
        StormOfNeedles                 = CreateMetemBtLine(8205792, onlyMetem      , factory); // Watch out, it's a veritable storm of needles!
        SuckedIn                       = CreateMetemBtLine(8205794, onlyMetem      , factory); // Oh no! The challenger's been sucked in!
        Thunderstorm                   = CreateMetemBtLine(8205796, onlyMetem      , factory); // A thunderstorm has turned the waters muddy! How frightening!
        TransformativePiece            = CreateMetemBtLine(8205799, onlyMetem      , factory); // A Transformative Piece! The fiend has been completely remade!
    
        // === Brute Abominator ===
        BAMeansBusiness                = CreateMetemBtLine(8205800, bruteAbominator, factory); // To take it outside, the Brute Abombinator means business!
        ChimericalFoe                  = CreateMetemBtLine(8205801, onlyMetem      , factory); // Will the unenhanced warrior be able to contend with this chimerical foe!?
        FeralPowersWeapon              = CreateMetemBtLine(8205802, mascPronouns   , factory); // My word! He's used his feral powers to manifest a weapon!
        FiendishFlora                  = CreateMetemBtLine(8205803, onlyMetem      , factory); // Oh dear, fiendish flora have sprouted from the scattered seeds!
        CamerasPetrified               = CreateMetemBtLine(8205804, onlyMetem      , factory); // Oh my! Even the cameras have been petrified─and now they're crashing down all around!
        BAHanging                      = CreateMetemBtLine(8205806, bruteAbominator, factory); // Goodness me! The Brute Abominator is hanging on the building!
        BBLariat                       = CreateMetemBtLine(8205811, bruteAbominator, factory); // A lariat! The Brute Bomber must still be in there somewhere!
        PunishingAttackFusion          = CreateMetemBtLine(8205812, onlyMetem      , factory); // A punishing attack! The true power of this monstrous fusion!
        SentRivalFlyingMasc            = CreateMetemBtLine(8205805, mascPronouns   , factory); // He's sent his rival flying! What fearsome strength!
        BoundingFromWallToWallMasc     = CreateMetemBtLine(8205807, mascPronouns   , factory); // He's bounding from wall to wall!
        RoofCavedSuchDevastation       = CreateMetemBtLine(8205808, onlyMetem      , factory); // The rooftop has caved in! Such devastation!
        SendingCameras                 = CreateMetemBtLine(8205809, onlyMetem      , factory); // We'll send cameras in there at once!
        StartedFire                    = CreateMetemBtLine(8205810, onlyMetem      , factory); // The destruction appears to have started a fire!
        DodgedEverything               = CreateMetemBtLine(8205813, onlyMetem      , factory); // The contender dodged everything! Why am I not surprised!?
        BrutalBlow                     = CreateMetemBtLine(8205814, onlyMetem      , factory); // A brutal blow! But the contender is standing firm!
        ThrillingBattle                = CreateMetemBtLine(8205815, onlyMetem      , factory); // What a thrilling battle this, and it rages on!
        
        // === Howling Blade ===
        WolfLair                       = CreateMetemBtLine(8205816, howlingBlade   , factory); // Can the challenger defeat the wolf in his own lair!?
        SuchSpeedMasc                  = CreateMetemBtLine(8205817, mascPronouns   , factory); // Such speed─it's as if there's a pack of him!
        ColossalThing                  = CreateMetemBtLine(8205818, onlyMetem      , factory); // Where did that colossal thing come from!?
        ColossusSwordMasc              = CreateMetemBtLine(8205819, mascPronouns   , factory); // He's put the colossus to the sword!
        SomethingsAmissMasc            = CreateMetemBtLine(8205820, mascPronouns   , factory); // Something's amiss... What does he intend to do!?
        RingDestroyedFallen            = CreateMetemBtLine(8205821, onlyMetem      , factory); // Egads! The ring's been destroyed, and they've fallen below!
        MovedToFloatingDeathtraps      = CreateMetemBtLine(8205822, onlyMetem      , factory); // They've moved to floating deathtraps! How will they fight with so little room?
        DestroyedIsle                  = CreateMetemBtLine(8205823, mascPronouns   , factory); // He's destroyed an isle! It'll take time to restore it!
        NeitherSideHoldingBack         = CreateMetemBtLine(8205824, onlyMetem      , factory); // Neither side is holding back, but there can be only one champion!
        
        // === Vamp Fatale ===
        VFFeastEyes                    = CreateMetemBtLine(8206078, vampFatale     , factory); // Feast your eyes on the lovely─and lethal─Vamp Fatale!
        FallPreyCruelMistress          = CreateMetemBtLine(8206079, onlyMetem      , factory); // Will the challenger fall prey to the cruel mistress?
        VFWickedWeapon                 = CreateMetemBtLine(8206080, onlyMetem      , factory); // A wicked weapon! Vamp Fatale's poised to execute a fatality!
        NowhereLeft                    = CreateMetemBtLine(8206081, vampFatale     , factory); // The challenger has nowhere left to run!
        DrainingAudienceFem            = CreateMetemBtLine(8206082, femPronouns    , factory); // It's begun! She's draining the audience─of their aether!
        VFUnleashedAether              = CreateMetemBtLine(8206083, vampFatale     , factory); // Vamp Fatale's unleashed the aether! The challenger's in her realm now!
        SpectacleResumes               = CreateMetemBtLine(8206084, onlyMetem      , factory); // The spectacle of slaughter resumes! How will the challenger adapt?
        RainOfDeath                    = CreateMetemBtLine(8206085, onlyMetem      , factory); // It's a veritable rain of death!
        DrainingCrowd                  = CreateMetemBtLine(8206086, femPronouns    , factory); // She's draining the crowd's aether again! I hope our dear spectators are feeling alright...
        EvenMoreAether                 = CreateMetemBtLine(8206087, femPronouns    , factory); // She's unleashed even more aether! How will this end!?
        LightheadedFem                 = CreateMetemBtLine(8206088, vampFatale     , factory); // The seductress is at it again! I...I'm feeling a little lightheaded...
        SiphonedAether                 = CreateMetemBtLine(8206089, femPronouns    , factory); // She's siphoned the challenger's aether!
        // === Deep Blue and Red Hot ===
        TagTeam                        = CreateMetemBtLine(8206090, deepBlueRedHot , factory); // This will be a tag team match of extreme proportions!
        SuchScorn                      = CreateMetemBtLine(8206091, onlyMetem      , factory); // My word, such scorn for the challenger!
        FearsomeFlamesMasc             = CreateMetemBtLine(8206092, mascPronouns   , factory); // What fearsome flames! He's set the ring ablaze!
        FlamesSpreading                = CreateMetemBtLine(8206093, onlyMetem      , factory); // The flames are spreading! At this rate, the challenger will be trapped!
        DBTaggingIn                    = CreateMetemBtLine(8206094, deepBlueRedHot , factory); // Deep Blue's lost his cool! He's tagging in!
        EnormousWave                   = CreateMetemBtLine(8206095, onlyMetem      , factory); // Good gods, what an enormous wave!
        BothBrosEntered                = CreateMetemBtLine(8206096, deepBlueRedHot , factory); // Both brothers have entered the ring! That's grounds for disqualification!
        DoubleTrouble                  = CreateMetemBtLine(8206097, deepBlueRedHot , factory); // Er, it seems the referee is allowing it... It's double trouble for the challenger!
        Fusion                         = CreateMetemBtLine(8206098, deepBlueRedHot , factory); // The brothers have triggered a fusion explosion! Truly, they're deadly when mixed!
        HoldingNothingBack             = CreateMetemBtLine(8206099, deepBlueRedHot , factory); // Brace yourselves, folks! The Xtremes are holding nothing back!
        
        // === The Tyrant ===
        GrandChampion                  = CreateMetemBtLine(8206100, onlyMetem      , factory); // At long last, the decisive match against the grand champion!
        TTThrone                       = CreateMetemBtLine(8206101, onlyMetem      , factory); // Will the challenger triumph and seize the Tyrant's throne?
        TyrannyTimeMasc                = CreateMetemBtLine(8206102, theTyrant      , factory); // It's Tyranny Time, folks! He knows no mercy, and he's never tasted defeat!
        HoldingTheirOwn                = CreateMetemBtLine(8206103, onlyMetem      , factory); // The challenger's holding their own!
        AllTheseWeaponsMasc            = CreateMetemBtLine(8206104, mascPronouns   , factory); // I beg your pardon? What is he going to do with all those weapons?
        AbsoluteBrutality              = CreateMetemBtLine(8206105, onlyMetem      , factory); // Absolute brutality! The champion's power is unmatched!
        SuchFerocity                   = CreateMetemBtLine(8206106, onlyMetem      , factory); // Such ferocity! The champion is ruthless indeed!
        TTAlteredFormMasc              = CreateMetemBtLine(8206107, theTyrant      , factory); // The Tyrant's altered his form! He looks even more tyrannical than before!
        UnleashedFullMightMasc         = CreateMetemBtLine(8206108, mascPronouns   , factory); // What a display of power! He's unleashed the full might of the behemoth!
        SplitRingHalfMasc              = CreateMetemBtLine(8206109, mascPronouns   , factory); // Oh, he's split the ring in half! We're going to need urgent repairs!
        ThrownRingMasc                 = CreateMetemBtLine(8206110, mascPronouns   , factory); // And now he's thrown the ring! What absurd strength!
        RingRestored                   = CreateMetemBtLine(8206111, onlyMetem      , factory); // Ahem, the ring shall presently be restored.
        EitherSide                     = CreateMetemBtLine(8206112, onlyMetem      , factory); // 'Tis a clash between feral pride and mortal resolve! Either side could come out on top!
        
        PresidentMustPay               = CreateMetemBtLine(8206113, onlyMetem      , factory); // The president must pay for his crimes! Give him what for, Champion!
        ArmLifeOwnMasc                 = CreateMetemBtLine(8206114, thePresident   , factory); // His arm has a life of its own! Is this the power of the mythical Lindwurm?
        ArmSlitheringOutDisgustingMasc = CreateMetemBtLine(8206115, mascPronouns   , factory); // His arm is slithering out! That's disgusting!
        LWScatteredGore                = CreateMetemBtLine(8206116, thePresident   , factory); // The Lindwurm's scattered gore is expanding!?
        CruelCoil                      = CreateMetemBtLine(8206117, onlyMetem      , factory); // Watch out! That is one cruel coil...
        ChampCrushed                   = CreateMetemBtLine(8206118, onlyMetem      , factory); // Oh no! The champion's been crushed!
        RoundRingMasc                  = CreateMetemBtLine(8206119, mascPronouns   , factory); // His arms are snaking 'round the ring! What's he planning!?
        RingPieces                     = CreateMetemBtLine(8206120, onlyMetem      , factory); // Dear me, the ring's in pieces!
        GoreLatched                    = CreateMetemBtLine(8206121, onlyMetem      , factory); // Oh no, the gore has latched onto the champion! Don't panic!
        LWCompletelyHealed             = CreateMetemBtLine(8206122, thePresident   , factory); // The Lindwurm's completely healed! What outrageous regenerative power!
        LWOutOfControl                 = CreateMetemBtLine(8206123, thePresident   , factory); // What the─? Could the Lindwurm's regeneration be out of control!?
        PowerRunAmokMasc               = CreateMetemBtLine(8206124, mascPronouns   , factory); // His power continues to run amok! Again and again he remakes his arms!
        RegenCapacityMasc              = CreateMetemBtLine(8206125, mascPronouns   , factory); // Is there no limit to his regenerative capacity!?
        StruckWithVenon                = CreateMetemBtLine(8206127, onlyMetem      , factory); // They've been struck with venom! But they can weather it─they must!
        SomethingRevolting             = CreateMetemBtLine(8206128, onlyMetem      , factory); // Something revolting this way comes!
        VictoryChamp                   = CreateMetemBtLine(8206129, onlyMetem      , factory); // The battle is decided! Victory goes to the champion!
        
        // === Unused ===
        UnusedNoRespectMasc   = CreateMetemLine(8205372, 4, "This man has absolutely no respect for the rules!", mascPronouns, factory);
        UnusedUpOnThePost     = CreateMetemLine(8205373, 3, "He's up on the post! You know what that means.", mascPronouns, factory);
        UnusedBombarianPress  = CreateMetemLine(8205374, 3, "It's the Bombarian press!", onlyMetem, factory);
        UnusedOhMercyFem      = CreateMetemLine(8205359, 4, "Oh mercy is she doing what I think she's doing?", femPronouns, factory);
        UnusedBackTail        = CreateMetemLine(8206126, 5, "Yes! You've got the serpent on the back tail! Press the advantage and finish this!", onlyMetem, factory);
        
        // === Mahjong ===
        MjGameChanger              = CreateMetemLine(8291277, 3, "This could be a real game changer."                                                , onlyMetem   , factory);
        MjBoldMove                 = CreateMetemLine(8291276, 4, "A bold move from our challenger, but will it pay off?"                             , onlyMetem   , factory);
        MjChallengerRecover        = CreateMetemLine(8291278, 4, "How will the challenger recover from this I wonder?"                               , onlyMetem   , factory);
        MjRivalVying               = CreateMetemLine(8291279, 4, "Whats this? A rival vying for victory?"                                            , onlyMetem   , factory);
        MjHeatingUp                = CreateMetemLine(8291280, 5, "Things are really heating up. I can scarcely look away."                           , onlyMetem   , factory);
        MjHandDecided              = CreateMetemLine(8291281, 3, "There it is. The hand is decided."                                                 , onlyMetem   , factory);
        MjKissLadyLuck             = CreateMetemLine(8291282, 5, "And with a kiss from lady luck, we have our winner."                               , onlyMetem   , factory);
        MjGoodnessGracious         = CreateMetemLine(8291283, 2, "Oh goodness gracious me."                                                          , onlyMetem   , factory);
        MjOurTile                  = CreateMetemLine(8291284, 4, "That should have been our tile, the scoundrel."                                    , onlyMetem   , factory);
        MjPainfulToWatch           = CreateMetemLine(8291285, 4, "Oh the horror! It was too painful to watch."                                       , onlyMetem   , factory);
        MjClobberedWithTable       = CreateMetemLine(8291286, 5, "Ha! You all but clobbered them with a table that round."                           , onlyMetem   , factory);
        MjBeautifullyPlayed        = CreateMetemLine(8291287, 4, "Beautifully played my friend, beautifully played."                                 , onlyMetem   , factory);
        MjMadeYourMark             = CreateMetemLine(8291288, 4, "You certainly made your mark this round. Keep it up!"                              , onlyMetem   , factory);
        MjDontStandAChance         = CreateMetemLine(8291289, 5, "Mmm, letting them think they stand a chance. I like it."                           , onlyMetem   , factory);
        MjChallengerDownHardGentle = CreateMetemLine(8291290, 4, "Oh my, our challenger went down hard."                                             , onlyMetem   , factory);
        MjStillInItGentle          = CreateMetemLine(8291291, 4, "Our challenger is still in it! But for how long?"                                  , onlyMetem   , factory);
        MjStillStandingGentle      = CreateMetemLine(8291292, 5, "Ooh even I felt that one, but our challenger is still standing."                   , onlyMetem   , factory);
        MjDownNotOut               = CreateMetemLine(8291293, 4, "Our challenger is down, but not out."                                              , onlyMetem   , factory);
        MjTitanOfTable             = CreateMetemLine(8291294, 6, "The titan of the table, tactician of the tiles! Brilliantly Played."               , onlyMetem   , factory);
        MjCommendableEffort        = CreateMetemLine(8291295, 4, "A commendable effort! You should be proud."                                        , onlyMetem   , factory);
        MjReportingLive            = CreateMetemLine(8291296, 6, "This was metem, reporting live from the Mahjong table. Thank you and good night."  , onlyMetem   , factory);
        MjCompetitionTooMuch       = CreateMetemLine(8291297, 5, "Was the competition simply too much for our challenger? I should hope not."        , onlyMetem   , factory);
        MjUtterlyHumiliated        = CreateMetemLine(8291298, 6, "Oh dear our challenger has been utterly humiliated! I fear this will haunt them."  , onlyMetem   , factory);
        
        // m12sp2
        LindwurmsHeart             = CreateMetemLine(8206130, 4, "Huh? Is it? The Lindwurm’s heart!?"                                                , thePresident, factory);
        BadFeeling                 = CreateMetemLine(8206131, 4, "It’s pulsating… I’ve got a bad feeling about this!"                                , onlyMetem   , factory);
        Transforming               = CreateMetemLine(8206132, 3, "It’s contorted…transforming!"                                                      , onlyMetem   , factory);
        ItsAliveLindwurm           = CreateMetemLine(8206133, 4, "It’s alive! It’s the birth of a new Lindwurm!"                                     , thePresident, factory);
        BattleNotOverDontDespair   = CreateMetemLine(8206134, 5, "Alas, the battle isn’t over yet, but don’t despair Champion!"                      , onlyMetem   , factory);
        CreatingMore               = CreateMetemLine(8206135, 3, "The Lindwurm is creating more of itself!"                                          , thePresident, factory);
        ContinuesToMultiply        = CreateMetemLine(8206136, 6, "The Lindwurm continues to multiply! We’ve gone well beyond mortal limits now!"     , thePresident, factory);
        CuriousProps               = CreateMetemLine(8206137, 5, "Some curious props have appeared… What would they possibly be?"                    , onlyMetem   , factory);
        TakenOnChampionForm        = CreateMetemLine(8206138, 5, "Egads! They’ve taken on the Champion’s form! This bodes ill!"                      , onlyMetem   , factory);
        DejaVu                     = CreateMetemLine(8206139, 4, "Ah-I’ve got a serious case of deja vu here."                                       , onlyMetem   , factory);
        RingRendInTwo              = CreateMetemLine(8206140, 4, "What destructiveness! The ring has been rend in two!"                              , onlyMetem   , factory);
        MutationCorrupting         = CreateMetemLine(8206141, 5, "The mutation is…corrupting the champion! Let’s hope they keep it together!"        , onlyMetem   , factory);
        SomethingsComing           = CreateMetemLine(8206142, 3, "Something’s coming! Stay on your toes!"                                            , onlyMetem   , factory);
        WarpedFabric               = CreateMetemLine(8206143, 6, "Woah! What is this place? Did the Lindwurm warp the very fabric of space and time?", thePresident, factory);
        AnotherDimension           = CreateMetemLine(8206144, 5, "Is this another dimension? I must confess, I am utterly bewildered!"               , onlyMetem   , factory);
        HardPressed                = CreateMetemLine(8206145, 4, "A relentless assault! The Champion is hard-pressed!"                               , onlyMetem   , factory);
        FeverPitch                 = CreateMetemLine(8206146, 5, "The battle has reached a fever pitch, but the Champion still stands!"              , onlyMetem   , factory);
        StayStrong                 = CreateMetemLine(8206147, 4, "There’s so many of them! Stay strong, I beg you!"                                  , onlyMetem   , factory);
        NotSureIUnderstand         = CreateMetemLine(8206148, 4, "I’m not sure I understand what’s going on anymore, folks!"                         , onlyMetem   , factory);
        LeftRealmOfLogic           = CreateMetemLine(8206149, 4, "We appear to have completely left the realm of logic behind!"                      , onlyMetem   , factory);
        DefiesComprehension        = CreateMetemLine(8206150, 5, "Something that defies all comprehension is unfolding before our very eyes!"        , onlyMetem   , factory);

    }

    private static BattleTalk CreateMetemCsLine(string key, List<Personalization> p, IBattleTalkFactory factory)
    {
        return factory.CreateFromCutsceneLine(Metem, 5, 6, key, p, MetemI);
    }
    
    private static BattleTalk CreateMetemBtLine(uint voiceover, List<Personalization> p, IBattleTalkFactory factory)
    {
        return factory.CreateFromContentDirectorBattleTalk(Metem, voiceover, p, MetemI);
    }
    
    private static BattleTalk CreateMetemLine(uint voiceover, int duration, string text, List<Personalization> p, IBattleTalkFactory factory)
    {
        return factory.CreateFromNoSheet(Metem, voiceover, duration, text, p, MetemI);
    }
    public static BattleTalk GetRandomAnnouncement()
    {
        List<BattleTalk> list = [ViciousBlow, FeltThatOneStillStanding, BeautifullyDodged, SawThroughIt, WentDownHard, SuckedIn, StruckSquare, AllOverUntilNextTime, Fallen, WhatPower];
        return list[Random.Shared.Next(list.Count)];
    }
    

}
