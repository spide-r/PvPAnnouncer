using System;
using System.Collections.Generic;

namespace PvPAnnouncer.Data;

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


// === Intro Lines ===
    public static readonly CutsceneTalk IntroBc = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000010_m", [Personalization.MetemAnnouncer, Personalization.BlackCat]); 
    public static readonly CutsceneTalk IntroBb = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000050_m", [Personalization.MetemAnnouncer, Personalization.BruteBomber]);
    public static readonly CutsceneTalk IntroHbl = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000040_m", [Personalization.MetemAnnouncer, Personalization.HoneyBLovely]);
    public static readonly CutsceneTalk IntroWt = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000060_m", [Personalization.MetemAnnouncer, Personalization.WickedThunder]);
    public static readonly CutsceneTalk VictoryWt = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000080_m", [Personalization.MetemAnnouncer]);
    public static readonly CutsceneTalk IntroDg = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000010_m", [Personalization.MetemAnnouncer, Personalization.DancingGreen]);
    public static readonly CutsceneTalk IntroSr = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000020_m",[Personalization.MetemAnnouncer, Personalization.SugarRiot]);
    public static readonly CutsceneTalk IntroBa = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000030_m", [Personalization.MetemAnnouncer, Personalization.BruteAbominator]);
    public static readonly CutsceneTalk IntroHb = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000040_m",[Personalization.MetemAnnouncer, Personalization.HowlingBlade]);
    public static readonly CutsceneTalk IntroVf = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000010_m", [Personalization.MetemAnnouncer, Personalization.VampFatale]);
    public static readonly CutsceneTalk IntroDbRh = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000020_m",[Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot]);
    public static readonly CutsceneTalk IntroTt = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000050_m", [Personalization.MetemAnnouncer, Personalization.Tyrant]);
    public static readonly CutsceneTalk IntroLw = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000080_m", [Personalization.MetemAnnouncer, Personalization.President]);

    // === Victory & Event Lines ===
    public static readonly CutsceneTalk GenericVictory = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000030_m", [Personalization.MetemAnnouncer]);
    public static readonly CutsceneTalk NewGcBornVictory = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000070_m", [Personalization.MetemAnnouncer]);
    public static readonly CutsceneTalk PresDefeated = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000090_m", [Personalization.MetemAnnouncer]);
    public static readonly CutsceneTalk RobotKo = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000050_m", [Personalization.MetemAnnouncer]);
    
    public static readonly BattleTalk ViciousBlow = new BattleTalk(Metem,  8205341, [Personalization.MetemAnnouncer], MetemI); // A vicious blow! That'll leave a mark!
    public static readonly BattleTalk FeltThatOneStillStanding = new BattleTalk(Metem,  8205342, [Personalization.MetemAnnouncer], MetemI); // Even I felt that one! But the challenger's still standing!
    public static readonly BattleTalk BeautifullyDodged = new BattleTalk(Metem,  8205343, [Personalization.MetemAnnouncer], MetemI); // Beautifully dodged!
    public static readonly BattleTalk SawThroughIt = new BattleTalk(Metem,  8205344, [Personalization.MetemAnnouncer], MetemI); // Amazing! The challenger saw straight through it!
    public static readonly BattleTalk WentDownHard = new BattleTalk(Metem,  8205345, [Personalization.MetemAnnouncer], MetemI); // Oh my! The challenger went down hard!
    public static readonly BattleTalk TheyreDownIsItOver = new BattleTalk(Metem,  8205346, [Personalization.MetemAnnouncer], MetemI); // And they're down! Is it over already!?
    public static readonly BattleTalk BackUpGrit = new BattleTalk(Metem,  8205347, [Personalization.MetemAnnouncer], MetemI); // They're back on their feet! What grit!
    public static readonly BattleTalk StillInIt = new BattleTalk(Metem,  8205348, [Personalization.MetemAnnouncer], MetemI); // They're still in it! But for how long!?
    public static readonly BattleTalk WhatPower = new BattleTalk(Metem,  8205349, [Personalization.MetemAnnouncer], MetemI); // What power! Looks like someone isn't holding back!
    public static readonly BattleTalk PotentMagicks = new BattleTalk(Metem,  8205350, [Personalization.MetemAnnouncer], MetemI); // Such potent magicks! But will they be enough to turn the tide?
    public static readonly BattleTalk IroncladDefense = new BattleTalk(Metem,  8205351, [Personalization.MetemAnnouncer], MetemI); // They're bracing for the storm with an ironclad defense!
    public static readonly BattleTalk Fallen = new BattleTalk(Metem,  8205352, [Personalization.MetemAnnouncer], MetemI); // Oh dear! The challenger has fallen out of the ring!
    public static readonly BattleTalk AllOverUntilNextTime = new BattleTalk(Metem,  8205388, [Personalization.MetemAnnouncer], MetemI); // It's all over...until next time!
    public static readonly BattleTalk StruckSquare = new BattleTalk(Metem,  8205772, [Personalization.MetemAnnouncer], MetemI); // That struck square!
    public static readonly BattleTalk Oof = new BattleTalk(Metem,  8205773, [Personalization.MetemAnnouncer], MetemI); // Oof! Will the challenger be alright!?
    public static readonly BattleTalk CouldntAvoid = new BattleTalk(Metem,  8205774, [Personalization.MetemAnnouncer], MetemI); // Oh no, they couldn't avoid that one!
    public static readonly BattleTalk MustHaveHurtNotOut = new BattleTalk(Metem,  8205775, [Personalization.MetemAnnouncer], MetemI); // That must have hurt! But they're not out of the fight yet!
    public static readonly BattleTalk EffortlesslyDodged = new BattleTalk(Metem,  8205776, [Personalization.MetemAnnouncer], MetemI); // Effortlessly dodged!
    public static readonly BattleTalk ClearlyAnticipated = new BattleTalk(Metem,  8205777, [Personalization.MetemAnnouncer], MetemI); // Amazing! They clearly anticipated that attack!
    public static readonly BattleTalk StylishEvasion = new BattleTalk(Metem,  8205778, [Personalization.MetemAnnouncer], MetemI); // A stylish evasion, well done!
    public static readonly BattleTalk AvoidedWithEase = new BattleTalk(Metem,  8205779, [Personalization.MetemAnnouncer], MetemI); // Avoided with ease! They knew just what to do!
    public static readonly BattleTalk TooMuch = new BattleTalk(Metem,  8205780, [Personalization.MetemAnnouncer], MetemI); // Oh no, that was too much for the challenger!
    public static readonly BattleTalk ChallengerDownIsThisEnd = new BattleTalk(Metem,  8205781, [Personalization.MetemAnnouncer], MetemI); // The challenger's down! Is this the end!?
    public static readonly BattleTalk RisesAgain = new BattleTalk(Metem,  8205782, [Personalization.MetemAnnouncer], MetemI); // The challenger rises again! Can they turn this around!?
    public static readonly BattleTalk BackOnFeet = new BattleTalk(Metem,  8205783, [Personalization.MetemAnnouncer], MetemI); // They're back on their feet! Let's see if they can stay standing!
    public static readonly BattleTalk OofMustHaveHurt = new BattleTalk(Metem,  8206066, [Personalization.MetemAnnouncer], MetemI); // Oof, that must've hurt! Hang in there!
    public static readonly BattleTalk NotFastEnough = new BattleTalk(Metem,  8206067, [Personalization.MetemAnnouncer], MetemI); // Not fast enough, I'm afraid!
    public static readonly BattleTalk CantBeCareless = new BattleTalk(Metem,  8206068, [Personalization.MetemAnnouncer], MetemI); // Even I felt that! Can't afford to be careless, now!
    public static readonly BattleTalk DirectHitStillStanding = new BattleTalk(Metem,  8206069, [Personalization.MetemAnnouncer], MetemI); // A direct hit! But they're still standing!
    public static readonly BattleTalk ImpressiveFootwork = new BattleTalk(Metem,  8206070, [Personalization.MetemAnnouncer], MetemI); // Impressive footwork on display today!
    public static readonly BattleTalk DancingAwayUnharmed = new BattleTalk(Metem,  8206071, [Personalization.MetemAnnouncer], MetemI); // And they dance out of the way unharmed! Unbelievable!
    public static readonly BattleTalk AnotherAttackEvaded = new BattleTalk(Metem,  8206072, [Personalization.MetemAnnouncer], MetemI); // Another attack evaded with aplomb!
    public static readonly BattleTalk SlippedBeyondReach = new BattleTalk(Metem,  8206073, [Personalization.MetemAnnouncer], MetemI); // Once again they slip beyond their opponent's reach!
    public static readonly BattleTalk BeenFelled = new BattleTalk(Metem,  8206074, [Personalization.MetemAnnouncer], MetemI); // Oh no, they've been felled with a vengeance!
    public static readonly BattleTalk TheyreDownCanTheyRecover = new BattleTalk(Metem,  8206075, [Personalization.MetemAnnouncer], MetemI); // And they're down! Can they recover from this?
    public static readonly BattleTalk WhatFightingSpirit = new BattleTalk(Metem,  8206076, [Personalization.MetemAnnouncer], MetemI); // They're up! What fighting spirit!
    public static readonly BattleTalk BackInAction = new BattleTalk(Metem,  8206077, [Personalization.MetemAnnouncer], MetemI); // And they're back in action! Can they turn the tide!?

    // === Black Cat ===
    public static readonly BattleTalk FirstOpponent = new BattleTalk(Metem,  8205353, [Personalization.MetemAnnouncer, Personalization.BlackCat], MetemI); // Our challenger's first opponent: Black Cat!
    public static readonly BattleTalk FelineFerocity = new BattleTalk(Metem,  8205354, [Personalization.MetemAnnouncer, Personalization.BlackCat], MetemI); // Can they hold their own against her feline ferocity!?
    public static readonly BattleTalk LitheAndLethal = new BattleTalk(Metem,  8205355, [Personalization.MetemAnnouncer, Personalization.BlackCat], MetemI); // Here it comes! Black Cat's lithe and lethal maneuver! 
    public static readonly BattleTalk NineLives = new BattleTalk(Metem,  8205358, [Personalization.MetemAnnouncer, Personalization.BlackCat], MetemI); // Look alive, folks, because this cat has nine.
    public static readonly BattleTalk FeralOnslaught = new BattleTalk(Metem,  8205360, [Personalization.MetemAnnouncer, Personalization.BlackCat], MetemI); // It begins─Black Cat's feral onslaught! 
    public static readonly BattleTalk MyRing = new BattleTalk(Metem,  8205356, [Personalization.MetemAnnouncer], MetemI); // My riiing!
    public static readonly BattleTalk RepairRing = new BattleTalk(Metem,  8205357, [Personalization.MetemAnnouncer], MetemI); // Ahem. Please bear with us while we repair the ring.

    // === Honey B. Lovely ===
    public static readonly BattleTalk HoneyBShowBegun = new BattleTalk(Metem,  8205361, [Personalization.MetemAnnouncer, Personalization.HoneyBLovely], MetemI); // The Honey B. Lovely show has begun!
    public static readonly BattleTalk SavorSting = new BattleTalk(Metem,  8205363, [Personalization.MetemAnnouncer, Personalization.HoneyBLovely], MetemI); // Now to savor the sweet sting of Honey B.'s charms.
    public static readonly BattleTalk ChangedRoutine = new BattleTalk(Metem,  8205367, [Personalization.MetemAnnouncer, Personalization.HoneyBLovely], MetemI); // What's this? Honey B. has changed her routine!
    public static readonly BattleTalk VenomStrikeFem = new BattleTalk(Metem,  8205362, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // Here comes her fearsome venom strike!
    public static readonly BattleTalk FeelingLoveFem = new BattleTalk(Metem,  8205364, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // Oh, she's definitely feeling your love!
    public static readonly BattleTalk ResistTheIrresistible = new BattleTalk(Metem,  8205365, [Personalization.MetemAnnouncer], MetemI); // Can the challenger resist the irresistible!? I know I can't.
    public static readonly BattleTalk HerCharmsNotDeniedFem = new BattleTalk(Metem,  8205366, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // Alas, her charms were not to be denied!
    public static readonly BattleTalk WhatAClash = new BattleTalk(Metem,  8205368, [Personalization.MetemAnnouncer], MetemI); // What a clash! Neither side is willing to yield!

    // === Brute Bomber ===
    public static readonly BattleTalk BBMuscled = new BattleTalk(Metem,  8205369, [Personalization.MascPronouns, Personalization.MetemAnnouncer, Personalization.BruteBomber], MetemI); // The Brute Bomber has muscled his way into the ring!
    public static readonly BattleTalk BBEmbiggening = new BattleTalk(Metem,  8205371, [Personalization.MetemAnnouncer, Personalization.BruteBomber], MetemI); // The Brute Bomber is embiggening himself!
    public static readonly BattleTalk KaboomBBSpecial = new BattleTalk(Metem,  8205375, [Personalization.MetemAnnouncer, Personalization.BruteBomber], MetemI); // Kaboom! The Bombarian Special!
    public static readonly BattleTalk BBDesprate = new BattleTalk(Metem,  8205378, [Personalization.MetemAnnouncer, Personalization.BruteBomber], MetemI); // The Brute Bomber is desperate!
    public static readonly BattleTalk BannedCompoundRobot = new BattleTalk("Referee", 8205440, [Personalization.MetemAnnouncer]); // Banned compound detected. Combatant disqualified.
    public static readonly BattleTalk AssaultedRefMasc = new BattleTalk(Metem,  8205370, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // My word, he just assaulted the referee! The scoundrel!
    public static readonly BattleTalk FuseField = new BattleTalk(Metem,  8205376, [Personalization.MetemAnnouncer], MetemI); // The infernal Fusefield! How will the challenger respond!?
    public static readonly BattleTalk ChainDeathmatch = new BattleTalk(Metem,  8205377, [Personalization.MetemAnnouncer], MetemI); // Oho, Chain Deathmatch! There's no escaping those adamant links!

    // === Wicked Thunder ===
    public static readonly BattleTalk WTReturned = new BattleTalk(Metem,  8205379, [Personalization.MetemAnnouncer, Personalization.WickedThunder], MetemI); // Wicked Thunder has returned to the ring!
    public static readonly BattleTalk SomethingGrowingFem = new BattleTalk(Metem,  8205383, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // Egads! Something is growing from her body!
    public static readonly BattleTalk GatheringAetherFem = new BattleTalk(Metem,  8205385, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // She's gathering aether...but to what end!?
    public static readonly BattleTalk ConvertAetherFem = new BattleTalk(Metem,  8205386, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // She's using electrope to convert her body's aether!
    public static readonly BattleTalk MassiveCannonFem = new BattleTalk(Metem,  8205380, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // Oooh, look at that massive cannon! Her skills are as sharp as ever!
    public static readonly BattleTalk GrownWingsFem = new BattleTalk(Metem,  8205381, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // She's grown wings! How wickedly divine!
    public static readonly BattleTalk UnleashedANewFeralSoul = new BattleTalk(Metem,  8205384, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // She's taken electrope into her body and unleashed a new feral soul!
    public static readonly BattleTalk DischargeAether = new BattleTalk(Metem,  8205382, [Personalization.MetemAnnouncer, Personalization.WickedThunder], MetemI); // Wicked Thunder is discharging vast amounts of aether!
    public static readonly BattleTalk BattleElectrifying = new BattleTalk(Metem,  8205387, [Personalization.MetemAnnouncer], MetemI); // A battle so electrifying I dare not blink!

    // === Dancing Green ===
    public static readonly BattleTalk DGSteps = new BattleTalk(Metem,  8205785, [Personalization.MetemAnnouncer, Personalization.DancingGreen], MetemI); // Dancing Green is pulling out all the stops with these steps!
    public static readonly BattleTalk UpstartBegins = new BattleTalk(Metem,  8205784, [Personalization.MetemAnnouncer], MetemI); // The upstart's cruiserweight campaign begins!
    public static readonly BattleTalk InvitationToDance = new BattleTalk(Metem,  8205786, [Personalization.MetemAnnouncer], MetemI); // An invitation to dance! How will the challenger respond?
    public static readonly BattleTalk DGFeverTakenHold = new BattleTalk(Metem,  8205787, [Personalization.MetemAnnouncer, Personalization.DancingGreen], MetemI); // The fever's taken hold! Can the upstart keep up with Dancing Green?
    public static readonly BattleTalk DGFeverPich = new BattleTalk(Metem,  8205788, [Personalization.MetemAnnouncer, Personalization.DancingGreen], MetemI); // The night's reached a fever pitch! All eyes are on Dancing Green!

    // === Sugar Riot ===
    public static readonly BattleTalk SRGallery = new BattleTalk(Metem,  8205789, [Personalization.MetemAnnouncer, Personalization.SugarRiot], MetemI); // Welcome to Sugar Riot's gallery of glamours!
    public static readonly BattleTalk SRBringsWorkToLife = new BattleTalk(Metem,  8205790, [Personalization.FemPronouns, Personalization.MetemAnnouncer, Personalization.SugarRiot], MetemI); // And with a flourish, Sugar Riot brings her work to life!
    public static readonly BattleTalk Quicksand = new BattleTalk(Metem,  8205793, [Personalization.MetemAnnouncer], MetemI); // Quicksand!? You don't want to get caught in that!
    public static readonly BattleTalk River = new BattleTalk(Metem,  8205795, [Personalization.MetemAnnouncer], MetemI); // A crystal-clear river! How charming!
    public static readonly BattleTalk StormParasols = new BattleTalk(Metem,  8205797, [Personalization.MetemAnnouncer], MetemI); // A massive, raging storm! I hope you brought your parasols!
    public static readonly BattleTalk Lava = new BattleTalk(Metem,  8205798, [Personalization.MetemAnnouncer], MetemI); // Lava is ravaging the ring! Talk about destructive creativity!
    public static readonly BattleTalk RingBecomeDesert = new BattleTalk(Metem,  8205791, [Personalization.MetemAnnouncer], MetemI); // Goodness, the ring has become a desert!
    public static readonly BattleTalk StormOfNeedles = new BattleTalk(Metem,  8205792, [Personalization.MetemAnnouncer], MetemI); // Watch out, it's a veritable storm of needles!
    public static readonly BattleTalk SuckedIn = new BattleTalk(Metem,  8205794, [Personalization.MetemAnnouncer], MetemI); // Oh no! The challenger's been sucked in!
    public static readonly BattleTalk Thunderstorm = new BattleTalk(Metem,  8205796, [Personalization.MetemAnnouncer], MetemI); // A thunderstorm has turned the waters muddy! How frightening!
    public static readonly BattleTalk TransformativePiece = new BattleTalk(Metem,  8205799, [Personalization.MetemAnnouncer], MetemI); // A Transformative Piece! The fiend has been completely remade!


    // === Brute Abominator ===
    public static readonly BattleTalk BAMeansBusiness = new BattleTalk(Metem,  8205800, [Personalization.MetemAnnouncer, Personalization.BruteAbominator], MetemI); // To take it outside, the Brute Abombinator means business!
    public static readonly BattleTalk ChimericalFoe = new BattleTalk(Metem,  8205801, [Personalization.MetemAnnouncer], MetemI); // Will the unenhanced warrior be able to contend with this chimerical foe!?
    public static readonly BattleTalk FeralPowersWeapon = new BattleTalk(Metem,  8205802, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // My word! He's used his feral powers to manifest a weapon!
    public static readonly BattleTalk FiendishFlora = new BattleTalk(Metem,  8205803, [Personalization.MetemAnnouncer], MetemI); // Oh dear, fiendish flora have sprouted from the scattered seeds!
    public static readonly BattleTalk CamerasPetrified = new BattleTalk(Metem,  8205804, [Personalization.MetemAnnouncer], MetemI); // Oh my! Even the cameras have been petrified─and now they're crashing down all around!
    public static readonly BattleTalk BAHanging = new BattleTalk(Metem,  8205806, [Personalization.MetemAnnouncer, Personalization.BruteAbominator], MetemI); // Goodness me! The Brute Abominator is hanging on the building!
    public static readonly BattleTalk BBLariat = new BattleTalk(Metem,  8205811, [Personalization.MetemAnnouncer, Personalization.BruteAbominator], MetemI); // A lariat! The Brute Bomber must still be in there somewhere!
    public static readonly BattleTalk PunishingAttackFusion = new BattleTalk(Metem,  8205812, [Personalization.MetemAnnouncer], MetemI); // A punishing attack! The true power of this monstrous fusion!
    public static readonly BattleTalk SentRivalFlyingMasc = new BattleTalk(Metem,  8205805, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // He's sent his rival flying! What fearsome strength!
    public static readonly BattleTalk BoundingFromWallToWallMasc = new BattleTalk(Metem,  8205807, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // He's bounding from wall to wall!
    public static readonly BattleTalk RoofCavedSuchDevastation = new BattleTalk(Metem,  8205808, [Personalization.MetemAnnouncer], MetemI); // The rooftop has caved in! Such devastation!
    public static readonly BattleTalk SendingCameras = new BattleTalk(Metem,  8205809, [Personalization.MetemAnnouncer], MetemI); // We'll send cameras in there at once!
    public static readonly BattleTalk StartedFire = new BattleTalk(Metem,  8205810, [Personalization.MetemAnnouncer], MetemI); // The destruction appears to have started a fire!
    public static readonly BattleTalk DodgedEverything = new BattleTalk(Metem,  8205813, [Personalization.MetemAnnouncer], MetemI); // The contender dodged everything! Why am I not surprised!?
    public static readonly BattleTalk BrutalBlow = new BattleTalk(Metem,  8205814, [Personalization.MetemAnnouncer], MetemI); // A brutal blow! But the contender is standing firm!
    public static readonly BattleTalk ThrillingBattle = new BattleTalk(Metem,  8205815, [Personalization.MetemAnnouncer], MetemI); // What a thrilling battle this, and it rages on!

    // === Howling Blade ===
    public static readonly BattleTalk WolfLair = new BattleTalk(Metem,  8205816, [Personalization.MetemAnnouncer, Personalization.HowlingBlade], MetemI); // Can the challenger defeat the wolf in his own lair!?
    public static readonly BattleTalk SuchSpeedMasc = new BattleTalk(Metem,  8205817, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // Such speed─it's as if there's a pack of him!
    public static readonly BattleTalk ColossalThing = new BattleTalk(Metem,  8205818, [Personalization.MetemAnnouncer], MetemI); // Where did that colossal thing come from!?
    public static readonly BattleTalk ColossusSwordMasc = new BattleTalk(Metem,  8205819, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // He's put the colossus to the sword!
    public static readonly BattleTalk SomethingsAmissMasc = new BattleTalk(Metem,  8205820, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // Something's amiss... What does he intend to do!?
    public static readonly BattleTalk RingDestroyedFallen = new BattleTalk(Metem,  8205821, [Personalization.MetemAnnouncer], MetemI);  // Egads! The ring's been destroyed, and they've fallen below!
    public static readonly BattleTalk MovedToFloatingDeathtraps = new BattleTalk(Metem,  8205822, [Personalization.MetemAnnouncer], MetemI); // They've moved to floating deathtraps!<br>How will they fight with so little room?
    public static readonly BattleTalk DestroyedIsle = new BattleTalk(Metem,  8205823, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // He's destroyed an isle! It'll take time to restore it!
    public static readonly BattleTalk NeitherSideHoldingBack = new BattleTalk(Metem,  8205824, [Personalization.MetemAnnouncer], MetemI); // Neither side is holding back, but there can be only one champion!

    // === Vamp Fatale === 
    public static readonly BattleTalk VFFeastEyes = new BattleTalk(Metem,  8206078, [Personalization.MetemAnnouncer, Personalization.VampFatale], MetemI); // Feast your eyes on the lovely─and lethal─Vamp Fatale!
    public static readonly BattleTalk FallPreyCruelMistress = new BattleTalk(Metem,  8206079, [Personalization.MetemAnnouncer], MetemI); // Will the challenger fall prey to the cruel mistress?
    public static readonly BattleTalk VFWickedWeapon = new BattleTalk(Metem,  8206080, [Personalization.MetemAnnouncer], MetemI); // A wicked weapon! Vamp Fatale's poised to execute a fatality!
    public static readonly BattleTalk NowhereLeft = new BattleTalk(Metem,  8206081, [Personalization.MetemAnnouncer, Personalization.VampFatale], MetemI); // The challenger has nowhere left to run!
    public static readonly BattleTalk DrainingAudienceFem = new BattleTalk(Metem,  8206082, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // It's begun! She's draining the audience─of their aether!
    public static readonly BattleTalk VFUnleashedAether = new BattleTalk(Metem,  8206083, [Personalization.MetemAnnouncer, Personalization.VampFatale], MetemI); // Vamp Fatale's unleashed the aether! The challenger's in her realm now!
    public static readonly BattleTalk SpectacleResumes = new BattleTalk(Metem,  8206084, [Personalization.MetemAnnouncer], MetemI); // The spectacle of slaughter resumes! How will the challenger adapt?
    public static readonly BattleTalk RainOfDeath = new BattleTalk(Metem,  8206085, [Personalization.MetemAnnouncer], MetemI); // It's a veritable rain of death!
    public static readonly BattleTalk DrainingCrowd = new BattleTalk(Metem,  8206086, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // She's draining the crowd's aether again! I hope our dear spectators are feeling alright...
    public static readonly BattleTalk EvenMoreAether = new BattleTalk(Metem,  8206087, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // She's unleashed even more aether! How will this end!?
    public static readonly BattleTalk LightheadedFem = new BattleTalk(Metem,  8206088, [Personalization.MetemAnnouncer, Personalization.VampFatale], MetemI); // The seductress is at it again! I...I'm feeling a little lightheaded...
    public static readonly BattleTalk SiphonedAether = new BattleTalk(Metem,  8206089, [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // She's siphoned the challenger's aether!

    // === Deep Blue & Red Hot ===
    public static readonly BattleTalk TagTeam = new BattleTalk(Metem,  8206090, [Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot], MetemI); // This will be a tag team match of extreme proportions!
    public static readonly BattleTalk SuchScorn = new BattleTalk(Metem,  8206091, [Personalization.MetemAnnouncer], MetemI); // My word, such scorn for the challenger!
    public static readonly BattleTalk FearsomeFlamesMasc = new BattleTalk(Metem,  8206092, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // What fearsome flames! He's set the ring ablaze!
    public static readonly BattleTalk FlamesSpreading = new BattleTalk(Metem,  8206093, [Personalization.MetemAnnouncer], MetemI); // The flames are spreading! At this rate, the challenger will be trapped!
    public static readonly BattleTalk DBTaggingIn = new BattleTalk(Metem,  8206094, [Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot], MetemI); // Deep Blue's lost his cool! He's tagging in!
    public static readonly BattleTalk EnormousWave = new BattleTalk(Metem,  8206095, [Personalization.MetemAnnouncer], MetemI); // Good gods, what an enormous wave!
    public static readonly BattleTalk BothBrosEntered = new BattleTalk(Metem,  8206096, [Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot], MetemI); // Both brothers have entered the ring! That's grounds for disqualification!
    public static readonly BattleTalk DoubleTrouble = new BattleTalk(Metem,  8206097, [Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot], MetemI); // Er, it seems the referee is allowing it... It's double trouble for the challenger!
    public static readonly BattleTalk Fusion = new BattleTalk(Metem,  8206098, [Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot], MetemI); // The brothers have triggered a fusion explosion! Truly, they're deadly when mixed!
    public static readonly BattleTalk HoldingNothingBack = new BattleTalk(Metem,  8206099, [Personalization.MetemAnnouncer, Personalization.DeepBlueRedHot], MetemI); // Brace yourselves, folks! The Xtremes are holding nothing back!

    // === The Tyrant === 
    public static readonly BattleTalk GrandChampion = new BattleTalk(Metem,  8206100, [Personalization.MetemAnnouncer], MetemI); // At long last, the decisive match against the grand champion!
    public static readonly BattleTalk TTThrone = new BattleTalk(Metem,  8206101, [Personalization.MetemAnnouncer], MetemI); // Will the challenger triumph and seize the Tyrant's throne?
    public static readonly BattleTalk TyrannyTimeMasc = new BattleTalk(Metem,  8206102, [Personalization.MetemAnnouncer, Personalization.Tyrant], MetemI); // It's Tyranny Time, folks! He knows no mercy, and he's never tasted defeat!
    public static readonly BattleTalk HoldingTheirOwn = new BattleTalk(Metem,  8206103, [Personalization.MetemAnnouncer], MetemI); // The challenger's holding their own!
    public static readonly BattleTalk AllTheseWeaponsMasc = new BattleTalk(Metem,  8206104, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // I beg your pardon? What is he going to do with all those weapons?
    public static readonly BattleTalk AbsoluteBrutality = new BattleTalk(Metem,  8206105, [Personalization.MetemAnnouncer], MetemI); // Absolute brutality! The champion's power is unmatched!
    public static readonly BattleTalk SuchFerocity = new BattleTalk(Metem,  8206106, [Personalization.MetemAnnouncer], MetemI); // Such ferocity! The champion is ruthless indeed!
    public static readonly BattleTalk TTAlteredFormMasc = new BattleTalk(Metem,  8206107, [Personalization.MetemAnnouncer, Personalization.Tyrant], MetemI); // The Tyrant's altered his form! He looks even more tyrannical than before!
    public static readonly BattleTalk UnleashedFullMightMasc = new BattleTalk(Metem,  8206108, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // What a display of power! He's unleashed the full might of the behemoth!
    public static readonly BattleTalk SplitRingHalfMasc = new BattleTalk(Metem,  8206109, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // Oh, he's split the ring in half! We're going to need urgent repairs!
    public static readonly BattleTalk ThrownRingMasc = new BattleTalk(Metem,  8206110, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // And now he's thrown the ring! What absurd strength!
    public static readonly BattleTalk RingRestored = new BattleTalk(Metem,  8206111, [Personalization.MetemAnnouncer], MetemI); // Ahem, the ring shall presently be restored.
    public static readonly BattleTalk EitherSide = new BattleTalk(Metem,  8206112, [Personalization.MetemAnnouncer], MetemI); // 'Tis a clash between feral pride and mortal resolve! Either side could come out on top!

    // === The President ===
    public static readonly BattleTalk PresidentMustPay = new BattleTalk(Metem,  8206113, [Personalization.MetemAnnouncer], MetemI); // The president must pay for his crimes! Give him what for, Champion!
    public static readonly BattleTalk ArmLifeOwnMasc = new BattleTalk(Metem,  8206114, [Personalization.MascPronouns, Personalization.MetemAnnouncer, Personalization.President], MetemI); // His arm has a life of its own! Is this the power of the mythical Lindwurm?
    public static readonly BattleTalk ArmSlitheringOutDisgustingMasc = new BattleTalk(Metem,  8206115, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // His arm is slithering out! That's disgusting!
    public static readonly BattleTalk LWScatteredGore = new BattleTalk(Metem,  8206116, [Personalization.MetemAnnouncer, Personalization.President], MetemI); // The Lindwurm's scattered gore is expanding!?
    public static readonly BattleTalk CruelCoil = new BattleTalk(Metem,  8206117, [Personalization.MetemAnnouncer], MetemI); // Watch out! That is one cruel coil...
    public static readonly BattleTalk ChampCrushed = new BattleTalk(Metem,  8206118, [Personalization.MetemAnnouncer], MetemI); // Oh no! The champion's been crushed!
    public static readonly BattleTalk RoundRingMasc = new BattleTalk(Metem,  8206119, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // His arms are snaking 'round the ring! What's he planning!?
    public static readonly BattleTalk RingPieces = new BattleTalk(Metem,  8206120, [Personalization.MetemAnnouncer], MetemI); // Dear me, the ring's in pieces!
    public static readonly BattleTalk GoreLatched = new BattleTalk(Metem,  8206121, [Personalization.MetemAnnouncer], MetemI); // Oh no, the gore has latched onto the champion! Don't panic!
    public static readonly BattleTalk LWCompletelyHealed = new BattleTalk(Metem,  8206122, [Personalization.MetemAnnouncer, Personalization.President], MetemI); // The Lindwurm's completely healed! What outrageous regenerative power!
    public static readonly BattleTalk LWOutOfControl = new BattleTalk(Metem,  8206123, [Personalization.MetemAnnouncer, Personalization.President], MetemI); // What the─? Could the Lindwurm's regeneration be out of control!?
    public static readonly BattleTalk PowerRunAmokMasc = new BattleTalk(Metem,  8206124, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // His power continues to run amok! Again and again he remakes his arms!
    public static readonly BattleTalk RegenCapacityMasc = new BattleTalk(Metem,  8206125, [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // Is there no limit to his regenerative capacity!?
    public static readonly BattleTalk StruckWithVenon = new BattleTalk(Metem,  8206127, [Personalization.MetemAnnouncer], MetemI); // They've been struck with venom! But they can weather it─they must!
    public static readonly BattleTalk SomethingRevolting = new BattleTalk(Metem,  8206128, [Personalization.MetemAnnouncer], MetemI); // Something revolting this way comes!
    public static readonly BattleTalk VictoryChamp = new BattleTalk(Metem,  8206129, [Personalization.MetemAnnouncer], MetemI); // The battle is decided! Victory goes to the champion!

    // == 
    // NOTE: The following voice lines are unused but the audio remains in the game
    public static readonly BattleTalk UnusedNoRespectMasc = new BattleTalk(Metem,  8205372, 4, "This man has absolutely no respect for the rules!", [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // This man has absolutely no respect for the rules! - 
    public static readonly BattleTalk UnusedUpOnThePost = new BattleTalk(Metem,  8205373, 3, "He's up on the post! You know what that means.", [Personalization.MascPronouns, Personalization.MetemAnnouncer], MetemI); // He's up on the post! You know what that means... 
    public static readonly BattleTalk UnusedBombarianPress = new BattleTalk(Metem,  8205374, 3, "It's the Bombarian press!", [Personalization.MetemAnnouncer], MetemI); // It's the Bombarian press! 
    public static readonly BattleTalk UnusedOhMercyFem = new BattleTalk(Metem,  8205359, 4, "Oh mercy is she doing what I think she's doing?", [Personalization.FemPronouns, Personalization.MetemAnnouncer], MetemI); // Oh mercy is she doing what I think she's doing?
    public static readonly BattleTalk UnusedBackTail = new BattleTalk(Metem,  8206126, 5, "Yes! You've got the serpent on the back tail! Press the advantage and finish this!", [Personalization.MetemAnnouncer], MetemI); // Yes! You've got the serpent on the back tail! Press the advantage and finish this!
    // ==

    // MAHJONG
    public static readonly BattleTalk MjGameChanger = new BattleTalk(Metem,  8291277, 3, "This could be a real game changer.", [Personalization.MetemAnnouncer], MetemI); // This could be a real game changer.
    public static readonly BattleTalk MjBoldMove = new BattleTalk(Metem,  8291276, 4, "A bold move from our challenger, but will it pay off?", [Personalization.MetemAnnouncer], MetemI); // A bold move from our challenger, but will it pay off?
    public static readonly BattleTalk MjChallengerRecover = new BattleTalk(Metem,  8291278, 4, "How will the challenger recover from this I wonder?", [Personalization.MetemAnnouncer], MetemI); // How will the challenger recover from this I wonder?
    public static readonly BattleTalk MjRivalVying = new BattleTalk(Metem,  8291279, 4, "Whats this? A rival vying for victory?", [Personalization.MetemAnnouncer], MetemI); // Whats this? A rival vying for victory? 
    public static readonly BattleTalk MjHeatingUp = new BattleTalk(Metem,  8291280, 5, "Things are really heating up. I can scarcely look away.", [Personalization.MetemAnnouncer], MetemI); // Things are really heating up. I can scarcely look away.
    public static readonly BattleTalk MjHandDecided = new BattleTalk(Metem,  8291281, 3, "There it is. The hand is decided.", [Personalization.MetemAnnouncer], MetemI); // There it is. The hand is decided.
    public static readonly BattleTalk MjKissLadyLuck = new BattleTalk(Metem,  8291282, 5, "And with a kiss from lady luck, we have our winner.", [Personalization.MetemAnnouncer], MetemI); // And with a kiss from lady luck, we have our winner.
    public static readonly BattleTalk MjGoodnessGracious = new BattleTalk(Metem,  8291283, 2, "Oh goodness gracious me.", [Personalization.MetemAnnouncer], MetemI); // Oh goodness gracious me.
    public static readonly BattleTalk MjOurTile = new BattleTalk(Metem,  8291284, 4, "That should have been our tile, the scoundrel.", [Personalization.MetemAnnouncer], MetemI); // That should have been our tile, the scoundrel.
    public static readonly BattleTalk MjPainfulToWatch = new BattleTalk(Metem,  8291285, 4, "Oh the horror! It was too painful to watch.", [Personalization.MetemAnnouncer], MetemI); // Oh the horror! It was too painful to watch.
    public static readonly BattleTalk MjClobberedWithTable = new BattleTalk(Metem,  8291286, 5, "Ha! You all but clobbered them with a table that round.", [Personalization.MetemAnnouncer], MetemI); // Ha! You all but clobbered them with the table that round.
    public static readonly BattleTalk MjBeautifullyPlayed = new BattleTalk(Metem,  8291287, 4, "Beautifully played my friend, beautifully played.", [Personalization.MetemAnnouncer], MetemI); // Beautifully played my friend, beautifully played.
    public static readonly BattleTalk MjMadeYourMark = new BattleTalk(Metem,  8291288, 4, "You certainly made your mark this round. Keep it up!", [Personalization.MetemAnnouncer], MetemI); // You certainly made your mark this round. Keep it up!
    public static readonly BattleTalk MjDontStandAChance = new BattleTalk(Metem,  8291289, 5, "Mmm, letting them think they stand a chance. I like it.", [Personalization.MetemAnnouncer], MetemI); // Mmm, letting them think they stand a chance. I like it.
    public static readonly BattleTalk MjChallengerDownHardGentle = new BattleTalk(Metem,  8291290, 4, "Oh my, our challenger went down hard.", [Personalization.MetemAnnouncer], MetemI); // Oh my, our challenger went down hard.
    public static readonly BattleTalk MjStillInItGentle = new BattleTalk(Metem,  8291291, 4, "Our challenger is still in it! But for how long?", [Personalization.MetemAnnouncer], MetemI); // Our challenger is still in it! But for how long?  
    public static readonly BattleTalk MjStillStandingGentle = new BattleTalk(Metem,  8291292, 5, "Ooh even I felt that one, but our challenger is still standing.", [Personalization.MetemAnnouncer], MetemI); // Ooh even I felt that one, but our challenger is still standing. 
    public static readonly BattleTalk MjDownNotOut = new BattleTalk(Metem,  8291293, 4, "Our challenger is down, but not out.", [Personalization.MetemAnnouncer], MetemI); // Our challenger is down, but not out.
    public static readonly BattleTalk MjTitanOfTable = new BattleTalk(Metem,  8291294, 6, "The titan of the table, tactician of the tiles! Brilliantly Played.", [Personalization.MetemAnnouncer], MetemI); // The titan of the table, tactician of the tiles! Brilliantly Played.
    public static readonly BattleTalk MjCommendableEffort = new BattleTalk(Metem,  8291295, 4, "A commendable effort! You should be proud.", [Personalization.MetemAnnouncer], MetemI); // A commendable effort! You should be proud.
    public static readonly BattleTalk MjReportingLive = new BattleTalk(Metem,  8291296, 6, "This was metem, reporting live from the Mahjong table. Thank you and good night.", [Personalization.MetemAnnouncer], MetemI); // This was Metem, reporting live from the Mahjong table. Thank you and good night.
    public static readonly BattleTalk MjCompetitionTooMuch = new BattleTalk(Metem,  8291297, 5, "Was the competition simply too much for our challenger? I should hope not.", [Personalization.MetemAnnouncer], MetemI); // Was the competition simply too much for our challenger? I should hope not.
    public static readonly BattleTalk MjUtterlyHumiliated = new BattleTalk(Metem,  8291298, 6, "Oh dear our challenger has been utterly humiliated! I fear this will haunt them.", [Personalization.MetemAnnouncer], MetemI); // Oh dear our challenger has been utterly humiliated! I fear this will haunt them...

    // m12sp2
    public static readonly BattleTalk LindwurmsHeart = new BattleTalk(Metem,  8206130, 4, "Huh? Is it? The Lindwurm’s heart!?", [Personalization.MetemAnnouncer, Personalization.President], MetemI); // Huh? Is it? The Lindwurm’s heart!?
    public static readonly BattleTalk BadFeeling = new BattleTalk(Metem,  8206131, 4, "It’s pulsating… I’ve got a bad feeling about this!", [Personalization.MetemAnnouncer], MetemI); // It’s pulsating… I’ve got a bad feeling about this!
    public static readonly BattleTalk Transforming = new BattleTalk(Metem,  8206132, 3, "It’s contorted…transforming!", [Personalization.MetemAnnouncer], MetemI); // It’s contorted…transforming!
    public static readonly BattleTalk ItsAliveLindwurm = new BattleTalk(Metem,  8206133, 4, "It’s alive! It’s the birth of a new Lindwurm!", [Personalization.MetemAnnouncer, Personalization.President], MetemI); // It’s alive! It’s the birth of a new Lindwurm!
    public static readonly BattleTalk BattleNotOverDontDespair = new BattleTalk(Metem,  8206134, 5, "Alas, the battle isn’t over yet, but don’t despair Champion!", [Personalization.MetemAnnouncer], MetemI); // Alas, the battle isn’t over yet, but don’t despair Champion!
    public static readonly BattleTalk CreatingMore = new BattleTalk(Metem,  8206135, 3, "The Lindwurm is creating more of itself!", [Personalization.MetemAnnouncer, Personalization.President], MetemI); // The Lindwurm is creating more of itself!
    public static readonly BattleTalk ContinuesToMultiply = new BattleTalk(Metem,  8206136, 6, "The Lindwurm continues to multiply! We’ve gone well beyond mortal limits now!", [Personalization.MetemAnnouncer, Personalization.President], MetemI); // The Lindwurm continues to multiply! We’ve gone well beyond mortal limits now!
    public static readonly BattleTalk CuriousProps = new BattleTalk(Metem,  8206137, 5, "Some curious props have appeared… What would they possibly be?", [Personalization.MetemAnnouncer], MetemI); // Some curious props have appeared… What would they possibly be?
    public static readonly BattleTalk TakenOnChampionForm = new BattleTalk(Metem,  8206138, 5, "Egads! They’ve taken on the Champion’s form! This bodes ill!", [Personalization.MetemAnnouncer], MetemI); // Egads! They’ve taken on the Champion’s form! This bodes ill!
    public static readonly BattleTalk DejaVu = new BattleTalk(Metem,  8206139, 4, "Ah-I’ve got a serious case of deja vu here.", [Personalization.MetemAnnouncer], MetemI); // Ah-I’ve got a serious case of deja vu here.
    public static readonly BattleTalk RingRendInTwo = new BattleTalk(Metem,  8206140, 4, "What destructiveness! The ring has been rend in two!", [Personalization.MetemAnnouncer], MetemI); // What destructiveness! The ring has been rend in two!
    public static readonly BattleTalk MutationCorrupting = new BattleTalk(Metem,  8206141, 5, "The mutation is…corrupting the champion! Let’s hope they keep it together!", [Personalization.MetemAnnouncer], MetemI); // The mutation is…corrupting the champion! Let’s hope they keep it together!
    public static readonly BattleTalk SomethingsComing = new BattleTalk(Metem,  8206142, 3, "Something’s coming! Stay on your toes!", [Personalization.MetemAnnouncer], MetemI); // Something’s coming! Stay on your toes!
    public static readonly BattleTalk WarpedFabric = new BattleTalk(Metem,  8206143, 6, "Woah! What is this place? Did the Lindwurm warp the very fabric of space and time?", [Personalization.MetemAnnouncer, Personalization.President], MetemI); // Woah! What is this place? Did the Lindwurm warp the very fabric of space and time?
    public static readonly BattleTalk AnotherDimension = new BattleTalk(Metem,  8206144, 5, "Is this another dimension? I must confess, I am utterly bewildered!", [Personalization.MetemAnnouncer], MetemI); // Is this another dimension? I must confess, I am utterly bewildered!
    public static readonly BattleTalk HardPressed = new BattleTalk(Metem,  8206145, 4, "A relentless assault! The Champion is hard-pressed!", [Personalization.MetemAnnouncer], MetemI); // A relentless assault! The Champion is hard-pressed!
    public static readonly BattleTalk FeverPitch = new BattleTalk(Metem,  8206146, 5, "The battle has reached a fever pitch, but the Champion still stands!", [Personalization.MetemAnnouncer], MetemI); // The battle has reached a fever pitch, but the Champion still stands!
    public static readonly BattleTalk StayStrong = new BattleTalk(Metem,  8206147, 4, "There’s so many of them! Stay strong, I beg you!", [Personalization.MetemAnnouncer], MetemI); // There’s so many of them! Stay strong, I beg you!
    public static readonly BattleTalk NotSureIUnderstand = new BattleTalk(Metem,  8206148, 4, "I’m not sure I understand what’s going on anymore, folks!", [Personalization.MetemAnnouncer], MetemI); // I’m not sure I understand what’s going on anymore, folks!
    public static readonly BattleTalk LeftRealmOfLogic = new BattleTalk(Metem,  8206149, 4, "We appear to have completely left the realm of logic behind!", [Personalization.MetemAnnouncer], MetemI); // We appear to have completely left the realm of logic behind!
    public static readonly BattleTalk DefiesComprehension = new BattleTalk(Metem,  8206150, 5, "Something that defies all comprehension is unfolding before our very eyes!", [Personalization.MetemAnnouncer], MetemI); // Something that defies all comprehension is unfolding before our very eyess!

    public static BattleTalk GetRandomAnnouncement()
    {
        List<BattleTalk> list = [ViciousBlow, FeltThatOneStillStanding, BeautifullyDodged, SawThroughIt, WentDownHard, SuckedIn, StruckSquare, AllOverUntilNextTime, Fallen, WhatPower];
        return list[Random.Shared.Next(list.Count)];
    }
    

}
