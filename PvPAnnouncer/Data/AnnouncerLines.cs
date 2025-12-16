using System;
using System.Collections.Generic;

namespace PvPAnnouncer.Data;

public static class AnnouncerLines
{
    //Announcer sound path: sound/voice/vo_line/INSERT_NUMBER_HERE_en.scd
    // en, de, fr, ja work
    
    //Lines are found in ContentDirectorBattleTalk as well as InstanceContentTextData - Use AlphaAOT 

    private static string GetVoPath(string announcement, string lang)
    {
        return "sound/voice/vo_line/" + announcement + "_" + lang + ".scd";
    }

    public const string IntroBc = "cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000010_m";
    public const string IntroBb = "cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000050_m";
    public const string IntroHbl = "cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000040_m";
    public const string IntroWt = "cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000060_m";
    public const string VictoryWt = "cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000080_m";
    public const string IntroDg = "cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000010_m";
    public const string IntroSr = "cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000020_m";
    public const string IntroBa = "cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000030_m";
    public const string IntroHb = "cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000040_m";
    
    public const string GenericVictory = "cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000030_m";
    public const string RobotKo = "cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000050_m";

    public static string GetPath(string announcement)
    {
        if (announcement.StartsWith("cut"))
        {
            return announcement + "_" + PluginServices.Config.Language  + ".scd";
        }
        return GetVoPath(announcement, PluginServices.Config.Language);
    }

    public static string GetRandomAnnouncement()
    {
        List<string> list = [ViciousBlow, FeltThatOneStillStanding, BeautifullyDodged, SawThroughIt, WentDownHard, SuckedIn, StruckSquare, AllOverUntilNextTime, Fallen, WhatPower];
        return list[Random.Shared.Next(list.Count)];
    }

    public static string GetAnnouncementStringFromUnusedVo(string id)
    {
        return id switch
        {
            UnusedBombarianPress => InternalConstants.BombarianPressText,
            UnusedNoRespectMasc => InternalConstants.NoRespectText,
            UnusedUpOnThePost => InternalConstants.UpOnThePostText,
            UnusedOhMercyFem => InternalConstants.OhMercyText,
            _ => "Uh oh! You shouldn't see this! Please report this to the PvPAnnouncer Dev!"
        };
    }
    
    public static int GetAnnouncementLengthFromUnusedVo(string id)
    {
        return id switch
        {
            UnusedBombarianPress => 3,
            UnusedNoRespectMasc => 4,
            UnusedUpOnThePost => 3,
            UnusedOhMercyFem => 4,
            _ => 5
        };
    }
    
    public const string InvalidPath = "4444444444444"; // testing purposes
    // === Generic ===
    public const string ViciousBlow = "8205341"; // A vicious blow! That'll leave a mark!
    public const string FeltThatOneStillStanding = "8205342"; // Even I felt that one! But the challenger's still standing!
    public const string BeautifullyDodged = "8205343"; // Beautifully dodged!
    public const string SawThroughIt = "8205344"; // Amazing! The challenger saw straight through it!
    public const string WentDownHard = "8205345"; // Oh my! The challenger went down hard!
    public const string TheyreDownIsItOver = "8205346"; // And they're down! Is it over already!?
    public const string BackUpGrit = "8205347"; // They're back on their feet! What grit!
    public const string StillInIt = "8205348"; // They're still in it! But for how long!?
    public const string WhatPower = "8205349"; // What power! Looks like someone isn't holding back!
    public const string PotentMagicks = "8205350"; // Such potent magicks! But will they be enough to turn the tide?
    public const string IroncladDefense = "8205351"; // They're bracing for the storm with an ironclad defense!
    public const string Fallen = "8205352"; // Oh dear! The challenger has fallen out of the ring!
    public const string AllOverUntilNextTime = "8205388"; // It's all over...until next time!
    public const string StruckSquare = "8205772"; // That struck square!
    public const string Oof = "8205773"; // Oof! Will the challenger be alright!?
    public const string CouldntAvoid = "8205774"; // Oh no, they couldn't avoid that one!
    public const string MustHaveHurtNotOut = "8205775"; // That must have hurt! But they're not out of the fight yet!
    public const string EffortlesslyDodged = "8205776"; // Effortlessly dodged!
    public const string ClearlyAnticipated = "8205777"; // Amazing! They clearly anticipated that attack!
    public const string StylishEvasion = "8205778"; // A stylish evasion, well done!
    public const string AvoidedWithEase = "8205779"; // Avoided with ease! They knew just what to do!
    public const string TooMuch = "8205780"; // Oh no, that was too much for the challenger!
    public const string ChallengerDownIsThisEnd = "8205781"; // The challenger's down! Is this the end!?
    public const string RisesAgain = "8205782"; // The challenger rises again! Can they turn this around!?
    public const string BackOnFeet = "8205783"; // They're back on their feet! Let's see if they can stay standing!
    
    // === 7.4 Generic ===
    public const string OofMustHaveHurt = "8206066";
    public const string NotFastEnough = "8206067";
    public const string CantBeCareless = "8206068";
    public const string DirectHitStillStanding = "8206069";
    public const string ImpressiveFootwork = "8206070";
    public const string DancingAwayUnharmed = "8206071";
    public const string AnotherAttackEvaded = "8206072";
    public const string SlippedBeyondReach = "8206073";
    public const string BeenFelled = "8206074";
    public const string TheyreDownCanTheyRecover = "8206075";
    public const string WhatFightingSpirit = "8206076";
    public const string BackInAction = "8206077";
    
    // === Black Cat ===
    public const string FirstOpponent = "8205353";// Our challenger's first opponent: Black Cat!
    public const string FelineFerocity = "8205354";// Can they hold their own against her feline ferocity!?
    public const string LitheAndLethal = "8205355";// Here it comes! Black Cat's lithe and lethal maneuver! 
    public const string NineLives = "8205358";// Look alive, folks, because this cat has nine.
    public const string FeralOnslaught = "8205360"; // It begins─Black Cat's feral onslaught! 
    public const string MyRing = "8205356"; // My riiing!
    public const string RepairRing = "8205357"; // Ahem. Please bear with us while we repair the ring.
    
    // === Honey B. Lovely ===
    public const string HoneyBShowBegun = "8205361"; // The Honey B. Lovely show has begun!
    public const string SavorSting = "8205363"; // Now to savor the sweet sting of Honey B.'s charms.
    public const string ChangedRoutine = "8205367"; // What's this? Honey B. has changed her routine!
    public const string VenomStrikeFem = "8205362"; // Here comes her fearsome venom strike!
    public const string FeelingLoveFem = "8205364"; // Oh, she's definitely feeling your love!
    public const string ResistTheIrresistible = "8205365"; // Can the challenger resist the irresistible!? I know I can't.
    public const string HerCharmsNotDeniedFem = "8205366"; // Alas, her charms were not to be denied!
    public const string WhatAClash = "8205368"; // What a clash! Neither side is willing to yield!
    
    // === Brute Bomber ===
    public const string BBMuscled = "8205369"; // The Brute Bomber has muscled his way into the ring!
    public const string BBEmbiggening = "8205371"; // The Brute Bomber is embiggening himself!
    public const string KaboomBBSpecial = "8205375";// Kaboom! The Bombarian Special!
    public const string BBDesprate = "8205378"; // The Brute Bomber is desperate!
    public const string BannedCompoundRobot = "8205440"; // Banned compound detected. Combatant disqualified.
    public const string AssaultedRefMasc = "8205370"; // My word, he just assaulted the referee! The scoundrel!
    public const string FuseField = "8205376"; // The infernal Fusefield! How will the challenger respond!?
    public const string ChainDeathmatch = "8205377"; // Oho, Chain Deathmatch! There's no escaping those adamant links!
    
    // === Wicked Thunder ===
    public const string WTReturned = "8205379"; // Wicked Thunder has returned to the ring!
    public const string SomethingGrowingFem = "8205383"; // Egads! Something is growing from her body!
    public const string GatheringAetherFem = "8205385"; // She's gathering aether...but to what end!?
    public const string ConvertAetherFem = "8205386"; // She's using electrope to convert her body's aether!
    public const string MassiveCannonFem = "8205380"; // Oooh, look at that massive cannon! Her skills are as sharp as ever!
    public const string GrownWingsFem = "8205381"; // She's grown wings! How wickedly divine!
    public const string UnleashedANewFeralSoul = "8205384"; // She's taken electrope into her body and unleashed a new feral soul!
    public const string DischargeAether = "8205382"; // Wicked Thunder is discharging vast amounts of aether!
    public const string BattleElectrifying = "8205387"; // A battle so electrifying I dare not blink!
    
    // === Dancing Green ===
    public const string DGSteps = "8205785";// Dancing Green is pulling out all the stops with these steps!
    public const string UpstartBegins = "8205784"; // The upstart's cruiserweight campaign begins!
    public const string InvitationToDance = "8205786"; // An invitation to dance! How will the challenger respond?
    public const string DGFeverTakenHold = "8205787";// The fever's taken hold! Can the upstart keep up with Dancing Green?
    public const string DGFeverPich = "8205788";// The night's reached a fever pitch! All eyes are on Dancing Green!
    
    // === Sugar Riot ===
    public const string SRGallery = "8205789"; // Welcome to Sugar Riot's gallery of glamours!
    public const string SRBringsWorkToLife = "8205790"; // And with a flourish, Sugar Riot brings her work to life!
    public const string Quicksand = "8205793"; // Quicksand!? You don't want to get caught in that!
    public const string River = "8205795"; // A crystal-clear river! How charming!
    public const string StormParasols = "8205797"; // A massive, raging storm! I hope you brought your parasols!
    public const string Lava = "8205798"; // Lava is ravaging the ring! Talk about destructive creativity!
    public const string RingBecomeDesert = "8205791"; // Goodness, the ring has become a desert!
    public const string StormOfNeedles = "8205792"; // Watch out, it's a veritable storm of needles!
    public const string SuckedIn = "8205794"; // Oh no! The challenger's been sucked in!
    public const string Thunderstorm = "8205796"; // A thunderstorm has turned the waters muddy! How frightening!
    public const string TransformativePiece = "8205799"; // A Transformative Piece! The fiend has been completely remade!
    
    
    // === Brute Abominator ===
    public const string BAMeansBusiness = "8205800"; // To take it outside, the Brute Abombinator means business!
    public const string ChimericalFoe = "8205801"; // Will the unenhanced warrior be able to contend with this chimerical foe!?
    public const string FeralPowersWeapon = "8205802"; // My word! He's used his feral powers to manifest a weapon!
    public const string FiendishFlora = "8205803"; // Oh dear, fiendish flora have sprouted from the scattered seeds!
    public const string CamerasPetrified = "8205804"; // Oh my! Even the cameras have been petrified─and now they're crashing down all around!
    public const string BAHanging = "8205806"; // Goodness me! The Brute Abominator is hanging on the building!
    public const string BBLariat = "8205811"; // A lariat! The Brute Bomber must still be in there somewhere!
    public const string PunishingAttackFusion = "8205812"; // A punishing attack! The true power of this monstrous fusion!
    public const string SentRivalFlyingMasc = "8205805"; // He's sent his rival flying! What fearsome strength!
    public const string BoundingFromWallToWallMasc = "8205807"; // He's bounding from wall to wall!
    public const string RoofCavedSuchDevastation = "8205808"; // The rooftop has caved in! Such devastation!
    public const string SendingCameras = "8205809"; // We'll send cameras in there at once!
    public const string StartedFire = "8205810"; // The destruction appears to have started a fire!
    public const string DodgedEverything = "8205813"; // The contender dodged everything! Why am I not surprised!?
    public const string BrutalBlow = "8205814"; // A brutal blow! But the contender is standing firm!
    public const string ThrillingBattle = "8205815"; // What a thrilling battle this, and it rages on!
    
    // === Howling Blade ===
    public const string WolfLair = "8205816"; // Can the challenger defeat the wolf in his own lair!?
    public const string SuchSpeedMasc = "8205817"; // Such speed─it's as if there's a pack of him!
    public const string ColossalThing = "8205818"; // Where did that colossal thing come from!?
    public const string ColossalThingSwordMasc = "8205819"; // He's put the colossus to the sword!
    public const string RingDestroyedFallen = "8205821"; 
    public const string MovedToFloatingDeathtraps = "8205822"; 
    public const string DestroyedIsle = "8205823"; 
    public const string NeitherSideHoldingBack = "8205824"; 
    
    //if you're reading this you cant complain about spoilers
    
    // === Vamp Fatale === 
    public const string FeastEyes = "8206078";
    public const string FallPreyCruelMistress = "8206079";
    public const string VFWickedWeapon = "8206080";
    public const string NowhereLeft = "8206081";
    public const string DrainingAudience= "8206082";
    public const string VFUnleashedAether = "8206083";
    public const string SpectacleResumes = "8206084";
    public const string RainOfDeath = "8206085";
    public const string LightheadedFem = "8206088";

    public const string SiphonedEather = "8206089";
    // === Deep Blue & Red Hot ===
    public const string TagTeam = "8206090";
    public const string SuchScorn = "8206091";
    public const string FearsomeFlamesMasc = "8206092";
    public const string FlamesSpreading = "8206093";
    public const string DBTaggingIn = "8206094";
    public const string EnormousWave = "8206095";
    public const string BothBrosEntered = "8206096";
    public const string ExtremesFightAsOne = "8206167";
    public const string DoubleTrouble = "8206097";
    public const string Fuzion = "8206098";
    public const string HoldingNothingBack = "8206099";
    
    // === The Tyrant === 
    public const string GrandChampion = "8206100";
    public const string TyrantThrone = "8206101";
    public const string TyrranyTimeMasc = "8206102";
    public const string HoldingTheirOwn = "8206103";
    public const string AllTheseWeapons = "8206104";
    public const string AbsoluteBrutality = "8206105";
    public const string SuchFerocity = "8206106";
    public const string TheTyrantAlteredForm = "8206107";
    public const string UnleashedFullMight = "8206108";
    public const string SplitRingHalf = "8206109";
    public const string ThrownRing = "8206110";
    public const string RingRestored = "8206111";
    public const string EitherSide = "8206112";
    
    // === The Commissioner ===
    public const string PresidentMustPay = "8206113";
    public const string ArmLifeOwn = "8206114";
    public const string ArmSlitheringOutDisgusting = "8206115";
    public const string LindwurmScatteredGore = "8206116";
    public const string CruelCoil = "8206117";
    public const string ChampCrushed = "8206118";
    public const string RoundRingMasc = "8206119";
    public const string RingPieces = "8206120";
    public const string GoreLatched = "8206121";
    public const string CompletelyHealed = "8206122";
    public const string HealingOutOfControl = "8206123";
    public const string PowerRunAmokMasc = "8206124";
    public const string RegenCapacityMasc = "8206125";
    
    
    // == 
    // NOTE: The following voice lines are unused but the audio remains in the game
    public const string UnusedNoRespectMasc = "8205372"; // This man has absolutely no respect for the rules! - 
    public const string UnusedUpOnThePost = "8205373"; // He's up on the post! You know what that means... 
    public const string UnusedBombarianPress = "8205374"; // It's the Bombarian press! 
    public const string UnusedOhMercyFem = "8205359"; // Oh mercy is she doing what I think she's doing?
    // ==

}
