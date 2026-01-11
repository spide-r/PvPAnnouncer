using System;
using System.Collections.Generic;

namespace PvPAnnouncer.Data;

public static class AnnouncerLines
{
    
    //Announcer sound path: sound/voice/vo_line/INSERT_NUMBER_HERE_en.scd
    // en, de, fr, ja work
    
    //Lines are found in ContentDirectorBattleTalk as well as InstanceContentTextData - Use AlphaAOT 
    
    //todo: huge recovery from earths reply/microcosmos (regen voice lines) (8206125 for huge heal from others, 8206122 from pot?)

    private static string GetVoPath(string announcement, string lang)
    {
        return "sound/voice/vo_line/" + announcement + "_" + lang + ".scd";
    }

// === Intro Lines ===
    public static readonly CutsceneTalk IntroBc = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000010_m");
    public static readonly CutsceneTalk IntroBb = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000050_m");
    public static readonly CutsceneTalk IntroHbl = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000040_m");
    public static readonly CutsceneTalk IntroWt = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000060_m");
    public static readonly CutsceneTalk VictoryWt = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000080_m");
    public static readonly CutsceneTalk IntroDg = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000010_m");
    public static readonly CutsceneTalk IntroSr = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000020_m");
    public static readonly CutsceneTalk IntroBa = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000030_m");
    public static readonly CutsceneTalk IntroHb = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000040_m");
    public static readonly CutsceneTalk IntroVf = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000010_m");
    public static readonly CutsceneTalk IntroDbRh = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000020_m");
    public static readonly CutsceneTalk IntroTt = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000050_m");
    public static readonly CutsceneTalk IntroLw = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000080_m");

    // === Victory & Event Lines ===
    public static readonly CutsceneTalk GenericVictory = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07010/vo_voiceman_07010_000030_m");
    public static readonly CutsceneTalk NewGcBornVictory = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000070_m");
    public static readonly CutsceneTalk PresDefeated = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07410/vo_voiceman_07410_000090_m");
    public static readonly CutsceneTalk RobotKo = new CutsceneTalk("cut/ex5/sound/voicem/voiceman_07210/vo_voiceman_07210_000050_m");
    
    public static readonly BattleTalk ViciousBlow = new BattleTalk("8205341"); // A vicious blow! That'll leave a mark!
    public static readonly BattleTalk FeltThatOneStillStanding = new BattleTalk("8205342"); // Even I felt that one! But the challenger's still standing!
    public static readonly BattleTalk BeautifullyDodged = new BattleTalk("8205343"); // Beautifully dodged!
    public static readonly BattleTalk SawThroughIt = new BattleTalk("8205344"); // Amazing! The challenger saw straight through it!
    public static readonly BattleTalk WentDownHard = new BattleTalk("8205345"); // Oh my! The challenger went down hard!
    public static readonly BattleTalk TheyreDownIsItOver = new BattleTalk("8205346"); // And they're down! Is it over already!?
    public static readonly BattleTalk BackUpGrit = new BattleTalk("8205347"); // They're back on their feet! What grit!
    public static readonly BattleTalk StillInIt = new BattleTalk("8205348"); // They're still in it! But for how long!?
    public static readonly BattleTalk WhatPower = new BattleTalk("8205349"); // What power! Looks like someone isn't holding back!
    public static readonly BattleTalk PotentMagicks = new BattleTalk("8205350"); // Such potent magicks! But will they be enough to turn the tide?
    public static readonly BattleTalk IroncladDefense = new BattleTalk("8205351"); // They're bracing for the storm with an ironclad defense!
    public static readonly BattleTalk Fallen = new BattleTalk("8205352"); // Oh dear! The challenger has fallen out of the ring!
    public static readonly BattleTalk AllOverUntilNextTime = new BattleTalk("8205388"); // It's all over...until next time!
    public static readonly BattleTalk StruckSquare = new BattleTalk("8205772"); // That struck square!
    public static readonly BattleTalk Oof = new BattleTalk("8205773"); // Oof! Will the challenger be alright!?
    public static readonly BattleTalk CouldntAvoid = new BattleTalk("8205774"); // Oh no, they couldn't avoid that one!
    public static readonly BattleTalk MustHaveHurtNotOut = new BattleTalk("8205775"); // That must have hurt! But they're not out of the fight yet!
    public static readonly BattleTalk EffortlesslyDodged = new BattleTalk("8205776"); // Effortlessly dodged!
    public static readonly BattleTalk ClearlyAnticipated = new BattleTalk("8205777"); // Amazing! They clearly anticipated that attack!
    public static readonly BattleTalk StylishEvasion = new BattleTalk("8205778"); // A stylish evasion, well done!
    public static readonly BattleTalk AvoidedWithEase = new BattleTalk("8205779"); // Avoided with ease! They knew just what to do!
    public static readonly BattleTalk TooMuch = new BattleTalk("8205780"); // Oh no, that was too much for the challenger!
    public static readonly BattleTalk ChallengerDownIsThisEnd = new BattleTalk("8205781"); // The challenger's down! Is this the end!?
    public static readonly BattleTalk RisesAgain = new BattleTalk("8205782"); // The challenger rises again! Can they turn this around!?
    public static readonly BattleTalk BackOnFeet = new BattleTalk("8205783"); // They're back on their feet! Let's see if they can stay standing!
    public static readonly BattleTalk OofMustHaveHurt = new BattleTalk("8206066"); // Oof, that must've hurt! Hang in there!
    public static readonly BattleTalk NotFastEnough = new BattleTalk("8206067"); // Not fast enough, I'm afraid!
    public static readonly BattleTalk CantBeCareless = new BattleTalk("8206068"); // Even I felt that! Can't afford to be careless, now!
    public static readonly BattleTalk DirectHitStillStanding = new BattleTalk("8206069"); // A direct hit! But they're still standing!
    public static readonly BattleTalk ImpressiveFootwork = new BattleTalk("8206070"); // Impressive footwork on display today!
    public static readonly BattleTalk DancingAwayUnharmed = new BattleTalk("8206071"); // And they dance out of the way unharmed! Unbelievable!
    public static readonly BattleTalk AnotherAttackEvaded = new BattleTalk("8206072"); // Another attack evaded with aplomb!
    public static readonly BattleTalk SlippedBeyondReach = new BattleTalk("8206073"); // Once again they slip beyond their opponent's reach!
    public static readonly BattleTalk BeenFelled = new BattleTalk("8206074"); // Oh no, they've been felled with a vengeance!
    public static readonly BattleTalk TheyreDownCanTheyRecover = new BattleTalk("8206075"); // And they're down! Can they recover from this?
    public static readonly BattleTalk WhatFightingSpirit = new BattleTalk("8206076"); // They're up! What fighting spirit!
    public static readonly BattleTalk BackInAction = new BattleTalk("8206077"); // And they're back in action! Can they turn the tide!?

    // === Black Cat ===
    public static readonly BattleTalk FirstOpponent = new BattleTalk("8205353"); // Our challenger's first opponent: Black Cat!
    public static readonly BattleTalk FelineFerocity = new BattleTalk("8205354"); // Can they hold their own against her feline ferocity!?
    public static readonly BattleTalk LitheAndLethal = new BattleTalk("8205355"); // Here it comes! Black Cat's lithe and lethal maneuver! 
    public static readonly BattleTalk NineLives = new BattleTalk("8205358"); // Look alive, folks, because this cat has nine.
    public static readonly BattleTalk FeralOnslaught = new BattleTalk("8205360"); // It begins─Black Cat's feral onslaught! 
    public static readonly BattleTalk MyRing = new BattleTalk("8205356"); // My riiing!
    public static readonly BattleTalk RepairRing = new BattleTalk("8205357"); // Ahem. Please bear with us while we repair the ring.

    // === Honey B. Lovely ===
    public static readonly BattleTalk HoneyBShowBegun = new BattleTalk("8205361"); // The Honey B. Lovely show has begun!
    public static readonly BattleTalk SavorSting = new BattleTalk("8205363"); // Now to savor the sweet sting of Honey B.'s charms.
    public static readonly BattleTalk ChangedRoutine = new BattleTalk("8205367"); // What's this? Honey B. has changed her routine!
    public static readonly BattleTalk VenomStrikeFem = new BattleTalk("8205362"); // Here comes her fearsome venom strike!
    public static readonly BattleTalk FeelingLoveFem = new BattleTalk("8205364"); // Oh, she's definitely feeling your love!
    public static readonly BattleTalk ResistTheIrresistible = new BattleTalk("8205365"); // Can the challenger resist the irresistible!? I know I can't.
    public static readonly BattleTalk HerCharmsNotDeniedFem = new BattleTalk("8205366"); // Alas, her charms were not to be denied!
    public static readonly BattleTalk WhatAClash = new BattleTalk("8205368"); // What a clash! Neither side is willing to yield!

    // === Brute Bomber ===
    public static readonly BattleTalk BBMuscled = new BattleTalk("8205369"); // The Brute Bomber has muscled his way into the ring!
    public static readonly BattleTalk BBEmbiggening = new BattleTalk("8205371"); // The Brute Bomber is embiggening himself!
    public static readonly BattleTalk KaboomBBSpecial = new BattleTalk("8205375"); // Kaboom! The Bombarian Special!
    public static readonly BattleTalk BBDesprate = new BattleTalk("8205378"); // The Brute Bomber is desperate!
    public static readonly BattleTalk BannedCompoundRobot = new BattleTalk("8205440"); // Banned compound detected. Combatant disqualified.
    public static readonly BattleTalk AssaultedRefMasc = new BattleTalk("8205370"); // My word, he just assaulted the referee! The scoundrel!
    public static readonly BattleTalk FuseField = new BattleTalk("8205376"); // The infernal Fusefield! How will the challenger respond!?
    public static readonly BattleTalk ChainDeathmatch = new BattleTalk("8205377"); // Oho, Chain Deathmatch! There's no escaping those adamant links!

    // === Wicked Thunder ===
    public static readonly BattleTalk WTReturned = new BattleTalk("8205379"); // Wicked Thunder has returned to the ring!
    public static readonly BattleTalk SomethingGrowingFem = new BattleTalk("8205383"); // Egads! Something is growing from her body!
    public static readonly BattleTalk GatheringAetherFem = new BattleTalk("8205385"); // She's gathering aether...but to what end!?
    public static readonly BattleTalk ConvertAetherFem = new BattleTalk("8205386"); // She's using electrope to convert her body's aether!
    public static readonly BattleTalk MassiveCannonFem = new BattleTalk("8205380"); // Oooh, look at that massive cannon! Her skills are as sharp as ever!
    public static readonly BattleTalk GrownWingsFem = new BattleTalk("8205381"); // She's grown wings! How wickedly divine!
    public static readonly BattleTalk UnleashedANewFeralSoul = new BattleTalk("8205384"); // She's taken electrope into her body and unleashed a new feral soul!
    public static readonly BattleTalk DischargeAether = new BattleTalk("8205382"); // Wicked Thunder is discharging vast amounts of aether!
    public static readonly BattleTalk BattleElectrifying = new BattleTalk("8205387"); // A battle so electrifying I dare not blink!

    // === Dancing Green ===
    public static readonly BattleTalk DGSteps = new BattleTalk("8205785"); // Dancing Green is pulling out all the stops with these steps!
    public static readonly BattleTalk UpstartBegins = new BattleTalk("8205784"); // The upstart's cruiserweight campaign begins!
    public static readonly BattleTalk InvitationToDance = new BattleTalk("8205786"); // An invitation to dance! How will the challenger respond?
    public static readonly BattleTalk DGFeverTakenHold = new BattleTalk("8205787"); // The fever's taken hold! Can the upstart keep up with Dancing Green?
    public static readonly BattleTalk DGFeverPich = new BattleTalk("8205788"); // The night's reached a fever pitch! All eyes are on Dancing Green!

    // === Sugar Riot ===
    public static readonly BattleTalk SRGallery = new BattleTalk("8205789"); // Welcome to Sugar Riot's gallery of glamours!
    public static readonly BattleTalk SRBringsWorkToLife = new BattleTalk("8205790"); // And with a flourish, Sugar Riot brings her work to life!
    public static readonly BattleTalk Quicksand = new BattleTalk("8205793"); // Quicksand!? You don't want to get caught in that!
    public static readonly BattleTalk River = new BattleTalk("8205795"); // A crystal-clear river! How charming!
    public static readonly BattleTalk StormParasols = new BattleTalk("8205797"); // A massive, raging storm! I hope you brought your parasols!
    public static readonly BattleTalk Lava = new BattleTalk("8205798"); // Lava is ravaging the ring! Talk about destructive creativity!
    public static readonly BattleTalk RingBecomeDesert = new BattleTalk("8205791"); // Goodness, the ring has become a desert!
    public static readonly BattleTalk StormOfNeedles = new BattleTalk("8205792"); // Watch out, it's a veritable storm of needles!
    public static readonly BattleTalk SuckedIn = new BattleTalk("8205794"); // Oh no! The challenger's been sucked in!
    public static readonly BattleTalk Thunderstorm = new BattleTalk("8205796"); // A thunderstorm has turned the waters muddy! How frightening!
    public static readonly BattleTalk TransformativePiece = new BattleTalk("8205799"); // A Transformative Piece! The fiend has been completely remade!


    // === Brute Abominator ===
    public static readonly BattleTalk BAMeansBusiness = new BattleTalk("8205800"); // To take it outside, the Brute Abombinator means business!
    public static readonly BattleTalk ChimericalFoe = new BattleTalk("8205801"); // Will the unenhanced warrior be able to contend with this chimerical foe!?
    public static readonly BattleTalk FeralPowersWeapon = new BattleTalk("8205802"); // My word! He's used his feral powers to manifest a weapon!
    public static readonly BattleTalk FiendishFlora = new BattleTalk("8205803"); // Oh dear, fiendish flora have sprouted from the scattered seeds!
    public static readonly BattleTalk CamerasPetrified = new BattleTalk("8205804"); // Oh my! Even the cameras have been petrified─and now they're crashing down all around!
    public static readonly BattleTalk BAHanging = new BattleTalk("8205806"); // Goodness me! The Brute Abominator is hanging on the building!
    public static readonly BattleTalk BBLariat = new BattleTalk("8205811"); // A lariat! The Brute Bomber must still be in there somewhere!
    public static readonly BattleTalk PunishingAttackFusion = new BattleTalk("8205812"); // A punishing attack! The true power of this monstrous fusion!
    public static readonly BattleTalk SentRivalFlyingMasc = new BattleTalk("8205805"); // He's sent his rival flying! What fearsome strength!
    public static readonly BattleTalk BoundingFromWallToWallMasc = new BattleTalk("8205807"); // He's bounding from wall to wall!
    public static readonly BattleTalk RoofCavedSuchDevastation = new BattleTalk("8205808"); // The rooftop has caved in! Such devastation!
    public static readonly BattleTalk SendingCameras = new BattleTalk("8205809"); // We'll send cameras in there at once!
    public static readonly BattleTalk StartedFire = new BattleTalk("8205810"); // The destruction appears to have started a fire!
    public static readonly BattleTalk DodgedEverything = new BattleTalk("8205813"); // The contender dodged everything! Why am I not surprised!?
    public static readonly BattleTalk BrutalBlow = new BattleTalk("8205814"); // A brutal blow! But the contender is standing firm!
    public static readonly BattleTalk ThrillingBattle = new BattleTalk("8205815"); // What a thrilling battle this, and it rages on!

    // === Howling Blade ===
    public static readonly BattleTalk WolfLair = new BattleTalk("8205816"); // Can the challenger defeat the wolf in his own lair!?
    public static readonly BattleTalk SuchSpeedMasc = new BattleTalk("8205817"); // Such speed─it's as if there's a pack of him!
    public static readonly BattleTalk ColossalThing = new BattleTalk("8205818"); // Where did that colossal thing come from!?
    public static readonly BattleTalk ColossusSwordMasc = new BattleTalk("8205819"); // He's put the colossus to the sword!
    public static readonly BattleTalk SomethingsAmissMasc = new BattleTalk("8205820"); // Something's amiss... What does he intend to do!?
    public static readonly BattleTalk RingDestroyedFallen = new BattleTalk("8205821");  // Egads! The ring's been destroyed, and they've fallen below!
    public static readonly BattleTalk MovedToFloatingDeathtraps = new BattleTalk("8205822"); // They've moved to floating deathtraps!<br>How will they fight with so little room?
    public static readonly BattleTalk DestroyedIsle = new BattleTalk("8205823"); // He's destroyed an isle! It'll take time to restore it!
    public static readonly BattleTalk NeitherSideHoldingBack = new BattleTalk("8205824"); // Neither side is holding back, but there can be only one champion!

    // === Vamp Fatale === 
    public static readonly BattleTalk VFFeastEyes = new BattleTalk("8206078"); // Feast your eyes on the lovely─and lethal─Vamp Fatale!
    public static readonly BattleTalk FallPreyCruelMistress = new BattleTalk("8206079"); // Will the challenger fall prey to the cruel mistress?
    public static readonly BattleTalk VFWickedWeapon = new BattleTalk("8206080"); // A wicked weapon! Vamp Fatale's poised to execute a fatality!
    public static readonly BattleTalk NowhereLeft = new BattleTalk("8206081"); // The challenger has nowhere left to run!
    public static readonly BattleTalk DrainingAudienceFem = new BattleTalk("8206082"); // It's begun! She's draining the audience─of their aether!
    public static readonly BattleTalk VFUnleashedAether = new BattleTalk("8206083"); // Vamp Fatale's unleashed the aether! The challenger's in her realm now!
    public static readonly BattleTalk SpectacleResumes = new BattleTalk("8206084"); // The spectacle of slaughter resumes! How will the challenger adapt?
    public static readonly BattleTalk RainOfDeath = new BattleTalk("8206085"); // It's a veritable rain of death!
    public static readonly BattleTalk DrainingCrowd = new BattleTalk("8206086"); // She's draining the crowd's aether again! I hope our dear spectators are feeling alright...
    public static readonly BattleTalk EvenMoreAether = new BattleTalk("8206087"); // She's unleashed even more aether! How will this end!?
    public static readonly BattleTalk LightheadedFem = new BattleTalk("8206088"); // The seductress is at it again! I...I'm feeling a little lightheaded...
    public static readonly BattleTalk SiphonedAether = new BattleTalk("8206089"); // She's siphoned the challenger's aether!

    // === Deep Blue & Red Hot ===
    public static readonly BattleTalk TagTeam = new BattleTalk("8206090"); // This will be a tag team match of extreme proportions!
    public static readonly BattleTalk SuchScorn = new BattleTalk("8206091"); // My word, such scorn for the challenger!
    public static readonly BattleTalk FearsomeFlamesMasc = new BattleTalk("8206092"); // What fearsome flames! He's set the ring ablaze!
    public static readonly BattleTalk FlamesSpreading = new BattleTalk("8206093"); // The flames are spreading! At this rate, the challenger will be trapped!
    public static readonly BattleTalk DBTaggingIn = new BattleTalk("8206094"); // Deep Blue's lost his cool! He's tagging in!
    public static readonly BattleTalk EnormousWave = new BattleTalk("8206095"); // Good gods, what an enormous wave!
    public static readonly BattleTalk BothBrosEntered = new BattleTalk("8206096"); // Both brothers have entered the ring! That's grounds for disqualification!
    public static readonly BattleTalk DoubleTrouble = new BattleTalk("8206097"); // Er, it seems the referee is allowing it... It's double trouble for the challenger!
    public static readonly BattleTalk Fusion = new BattleTalk("8206098"); // The brothers have triggered a fusion explosion! Truly, they're deadly when mixed!
    public static readonly BattleTalk HoldingNothingBack = new BattleTalk("8206099"); // Brace yourselves, folks! The Xtremes are holding nothing back!

    // === The Tyrant === 
    public static readonly BattleTalk GrandChampion = new BattleTalk("8206100"); // At long last, the decisive match against the grand champion!
    public static readonly BattleTalk TTThrone = new BattleTalk("8206101"); // Will the challenger triumph and seize the Tyrant's throne?
    public static readonly BattleTalk TyrannyTimeMasc = new BattleTalk("8206102"); // It's Tyranny Time, folks! He knows no mercy, and he's never tasted defeat!
    public static readonly BattleTalk HoldingTheirOwn = new BattleTalk("8206103"); // The challenger's holding their own!
    public static readonly BattleTalk AllTheseWeaponsMasc = new BattleTalk("8206104"); // I beg your pardon? What is he going to do with all those weapons?
    public static readonly BattleTalk AbsoluteBrutality = new BattleTalk("8206105"); // Absolute brutality! The champion's power is unmatched!
    public static readonly BattleTalk SuchFerocity = new BattleTalk("8206106"); // Such ferocity! The champion is ruthless indeed!
    public static readonly BattleTalk TTAlteredFormMasc = new BattleTalk("8206107"); // The Tyrant's altered his form! He looks even more tyrannical than before!
    public static readonly BattleTalk UnleashedFullMightMasc = new BattleTalk("8206108"); // What a display of power! He's unleashed the full might of the behemoth!
    public static readonly BattleTalk SplitRingHalfMasc = new BattleTalk("8206109"); // Oh, he's split the ring in half! We're going to need urgent repairs!
    public static readonly BattleTalk ThrownRingMasc = new BattleTalk("8206110"); // And now he's thrown the ring! What absurd strength!
    public static readonly BattleTalk RingRestored = new BattleTalk("8206111"); // Ahem, the ring shall presently be restored.
    public static readonly BattleTalk EitherSide = new BattleTalk("8206112"); // 'Tis a clash between feral pride and mortal resolve! Either side could come out on top!

    // === The President ===
    public static readonly BattleTalk PresidentMustPay = new BattleTalk("8206113"); // The president must pay for his crimes! Give him what for, Champion!
    public static readonly BattleTalk ArmLifeOwnMasc = new BattleTalk("8206114"); // His arm has a life of its own! Is this the power of the mythical Lindwurm?
    public static readonly BattleTalk ArmSlitheringOutDisgustingMasc = new BattleTalk("8206115"); // His arm is slithering out! That's disgusting!
    public static readonly BattleTalk LWScatteredGore = new BattleTalk("8206116"); // The Lindwurm's scattered gore is expanding!?
    public static readonly BattleTalk CruelCoil = new BattleTalk("8206117"); // Watch out! That is one cruel coil...
    public static readonly BattleTalk ChampCrushed = new BattleTalk("8206118"); // Oh no! The champion's been crushed!
    public static readonly BattleTalk RoundRingMasc = new BattleTalk("8206119"); // His arms are snaking 'round the ring! What's he planning!?
    public static readonly BattleTalk RingPieces = new BattleTalk("8206120"); // Dear me, the ring's in pieces!
    public static readonly BattleTalk GoreLatched = new BattleTalk("8206121"); // Oh no, the gore has latched onto the champion! Don't panic!
    public static readonly BattleTalk LWCompletelyHealed = new BattleTalk("8206122"); // The Lindwurm's completely healed! What outrageous regenerative power!
    public static readonly BattleTalk LWOutOfControl = new BattleTalk("8206123"); // What the─? Could the Lindwurm's regeneration be out of control!?
    public static readonly BattleTalk PowerRunAmokMasc = new BattleTalk("8206124"); // His power continues to run amok! Again and again he remakes his arms!
    public static readonly BattleTalk RegenCapacityMasc = new BattleTalk("8206125"); // Is there no limit to his regenerative capacity!?
    public static readonly BattleTalk StruckWithVenon = new BattleTalk("8206127"); // They've been struck with venom! But they can weather it─they must!
    public static readonly BattleTalk SomethingRevolting = new BattleTalk("8206128"); // Something revolting this way comes!
    public static readonly BattleTalk VictoryChamp = new BattleTalk("8206129"); // The battle is decided! Victory goes to the champion!

    // == 
    // NOTE: The following voice lines are unused but the audio remains in the game
    public static readonly BattleTalk UnusedNoRespectMasc = new BattleTalk("8205372", 4, InternalConstants.NoRespectText); // This man has absolutely no respect for the rules! - 
    public static readonly BattleTalk UnusedUpOnThePost = new BattleTalk("8205373", 3, InternalConstants.UpOnThePostText); // He's up on the post! You know what that means... 
    public static readonly BattleTalk UnusedBombarianPress = new BattleTalk("8205374", 3, InternalConstants.BombarianPressText); // It's the Bombarian press! 
    public static readonly BattleTalk UnusedOhMercyFem = new BattleTalk("8205359", 4, InternalConstants.OhMercyText); // Oh mercy is she doing what I think she's doing?
    public static readonly BattleTalk UnusedBackTail = new BattleTalk("8206126", 5, InternalConstants.BackTailText); // Yes! You've got the serpent on the back tail! Press the advantage and finish this!
    // ==

    // MAHJONG
    public static readonly BattleTalk MjGameChanger = new BattleTalk("8291277", 3, InternalConstants.MjTextGameChanger); // This could be a real game changer.
    public static readonly BattleTalk MjBoldMove = new BattleTalk("8291276", 4, InternalConstants.MjTextBoldMove); // A bold move from our challenger, but will it pay off?
    public static readonly BattleTalk MjChallengerRecover = new BattleTalk("8291278", 4, InternalConstants.MjTextChallengerRecover); // How will the challenger recover from this I wonder?
    public static readonly BattleTalk MjRivalVying = new BattleTalk("8291279", 4, InternalConstants.MjTextRivalVying); // Whats this? A rival vying for victory? 
    public static readonly BattleTalk MjHeatingUp = new BattleTalk("8291280", 5, InternalConstants.MjTextHeatingUp); // Things are really heating up. I can scarcely look away.
    public static readonly BattleTalk MjHandDecided = new BattleTalk("8291281", 3, InternalConstants.MjTextHandDecided); // There it is. The hand is decided.
    public static readonly BattleTalk MjKissLadyLuck = new BattleTalk("8291282", 5, InternalConstants.MjTextKissLadyLuck); // And with a kiss from lady luck, we have our winner.
    public static readonly BattleTalk MjGoodnessGracious = new BattleTalk("8291283", 2, InternalConstants.MjTextGoodnessGracious); // Oh goodness gracious me.
    public static readonly BattleTalk MjOurTile = new BattleTalk("8291284", 4, InternalConstants.MjTextOurTile); // That should have been our tile, the scoundrel.
    public static readonly BattleTalk MjPainfulToWatch = new BattleTalk("8291285", 4, InternalConstants.MjTextPainfulToWatch); // Oh the horror! It was too painful to watch.
    public static readonly BattleTalk MjClobberedWithTable = new BattleTalk("8291286", 5, InternalConstants.MjTextClobberedTable); // Ha! You all but clobbered them with the table that round.
    public static readonly BattleTalk MjBeautifullyPlayed = new BattleTalk("8291287", 4, InternalConstants.MjTextBeautifullyPlayed); // Beautifully played my friend, beautifully played.
    public static readonly BattleTalk MjMadeYourMark = new BattleTalk("8291288", 4, InternalConstants.MjTextMadeYourMark); // You certainly made your mark this round. Keep it up!
    public static readonly BattleTalk MjDontStandAChance = new BattleTalk("8291289", 5, InternalConstants.MjTextDontStandAChance); // Mmm, letting them think they stand a chance. I like it.
    public static readonly BattleTalk MjChallengerDownHardGentle = new BattleTalk("8291290", 4, InternalConstants.MjTextChallengerDownHard); // Oh my, our challenger went down hard.
    public static readonly BattleTalk MjStillInItGentle = new BattleTalk("8291291", 4, InternalConstants.MjTextStillInIt); // Our challenger is still in it! But for how long?  
    public static readonly BattleTalk MjStillStandingGentle = new BattleTalk("8291292", 5, InternalConstants.MjTextStillStanding); // Ooh even I felt that one, but our challenger is still standing. 
    public static readonly BattleTalk MjDownNotOut = new BattleTalk("8291293", 4, InternalConstants.MjTextDownNotOut); // Our challenger is down, but not out.
    public static readonly BattleTalk MjTitanOfTable = new BattleTalk("8291294", 6, InternalConstants.MjTextTitanOfTable); // The titan of the table, tactician of the tiles! Brilliantly Played.
    public static readonly BattleTalk MjCommendableEffort = new BattleTalk("8291295", 4, InternalConstants.MjTextCommendableEffort); // A commendable effort! You should be proud.
    public static readonly BattleTalk MjReportingLive = new BattleTalk("8291296", 6, InternalConstants.MjTextReportingLive); // This was Metem, reporting live from the Mahjong table. Thank you and good night.
    public static readonly BattleTalk MjCompetitionTooMuch = new BattleTalk("8291297", 5, InternalConstants.MjTextCompetitionTooMuch); // Was the competition simply too much for our challenger? I should hope not.
    public static readonly BattleTalk MjUtterlyHumiliated = new BattleTalk("8291298", 6, InternalConstants.MjTextUtterlyHumiliated); // Oh dear our challenger has been utterly humiliated! I fear this will haunt them...

    // m12sp2
    public static readonly BattleTalk LindwurmsHeart = new BattleTalk("8206130", 4, InternalConstants.TextLindwurmsHeart); // Huh? Is it? The Lindwurm’s heart!?
    public static readonly BattleTalk BadFeeling = new BattleTalk("8206131", 4, InternalConstants.TextBadFeeling); // It’s pulsating… I’ve got a bad feeling about this!
    public static readonly BattleTalk Transforming = new BattleTalk("8206132", 3, InternalConstants.TextTransforming); // It’s contorted…transforming!
    public static readonly BattleTalk ItsAliveLindwurm = new BattleTalk("8206133", 4, InternalConstants.TextItsAliveLindwurm); // It’s alive! It’s the birth of a new Lindwurm!
    public static readonly BattleTalk BattleNotOverDontDespair = new BattleTalk("8206134", 5, InternalConstants.TextBattleNotOverDontDespair); // Alas, the battle isn’t over yet, but don’t despair Champion!
    public static readonly BattleTalk CreatingMore = new BattleTalk("8206135", 3, InternalConstants.TextCreatingMore); // The Lindwurm is creating more of itself!
    public static readonly BattleTalk ContinuesToMultiply = new BattleTalk("8206136", 6, InternalConstants.TextContinuesToMultiply); // The Lindwurm continues to multiply! We’ve gone well beyond mortal limits now!
    public static readonly BattleTalk CuriousProps = new BattleTalk("8206137", 5, InternalConstants.TextCuriousProps); // Some curious props have appeared… What would they possibly be?
    public static readonly BattleTalk TakenOnChampionForm = new BattleTalk("8206138", 5, InternalConstants.TextTakenOnChampionForm); // Egads! They’ve taken on the Champion’s form! This bodes ill!
    public static readonly BattleTalk DejaVu = new BattleTalk("8206139", 4, InternalConstants.TextDejaVu); // Ah-I’ve got a serious case of deja vu here.
    public static readonly BattleTalk RingRendInTwo = new BattleTalk("8206140", 4, InternalConstants.TextRingRendInTwo); // What destructiveness! The ring has been rend in two!
    public static readonly BattleTalk MutationCorrupting = new BattleTalk("8206141", 5, InternalConstants.TextMutationCorrupting); // The mutation is…corrupting the champion! Let’s hope they keep it together!
    public static readonly BattleTalk SomethingsComing = new BattleTalk("8206142", 3, InternalConstants.TextSomethingsComing); // Something’s coming! Stay on your toes!
    public static readonly BattleTalk WarpedFabric = new BattleTalk("8206143", 6, InternalConstants.TextWarpedFabric); // Woah! What is this place? Did the Lindwurm warp the very fabric of space and time?
    public static readonly BattleTalk AnotherDimension = new BattleTalk("8206144", 5, InternalConstants.TextAnotherDimension); // Is this another dimension? I must confess, I am utterly bewildered!
    public static readonly BattleTalk HardPressed = new BattleTalk("8206145", 4, InternalConstants.TextHardPressed); // A relentless assault! The Champion is hard-pressed!
    public static readonly BattleTalk FeverPitch = new BattleTalk("8206146", 5, InternalConstants.TextFeverPitch); // The battle has reached a fever pitch, but the Champion still stands!
    public static readonly BattleTalk StayStrong = new BattleTalk("8206147", 4, InternalConstants.TextStayStrong); // There’s so many of them! Stay strong, I beg you!
    public static readonly BattleTalk NotSureIUnderstand = new BattleTalk("8206148", 4, InternalConstants.TextNotSureIUnderstand); // I’m not sure I understand what’s going on anymore, folks!
    public static readonly BattleTalk LeftRealmOfLogic = new BattleTalk("8206149", 4, InternalConstants.TextLeftRealmOfLogic); // We appear to have completely left the realm of logic behind!
    public static readonly BattleTalk DefiesComprehension = new BattleTalk("8206150", 5, InternalConstants.TextDefiesComprehension); // Something that defies all comprehension is unfolding before our very eyess!

    public static readonly List<BattleTalk> LimitBreakList =
    [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, NeitherSideHoldingBack, 
        BattleElectrifying, MjGameChanger, MjBoldMove, MjHeatingUp, AbsoluteBrutality, SuchFerocity, SomethingsComing];
    public static BattleTalk GetRandomAnnouncement()
    {
        List<BattleTalk> list = [ViciousBlow, FeltThatOneStillStanding, BeautifullyDodged, SawThroughIt, WentDownHard, SuckedIn, StruckSquare, AllOverUntilNextTime, Fallen, WhatPower];
        return list[Random.Shared.Next(list.Count)];
    }

}
