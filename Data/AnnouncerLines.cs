using System;

namespace PvPAnnouncer.Data;

public static class AnnouncerLines
{
    //Announcer sound path: sound/voice/vo_line/INSERT_NUMBER_HERE_en.scd
    // en, de, fr, ja work
    
    //Lines are found in ContentDirectorTalk as well as InstanceContentTextData - Use AlphaAOT 

    public static String GetPath(String announcement, String lang)
    {
        return "sound/voice/vo_line/" + announcement + "_" + lang + ".scd";
    }

    public static String GetPath(String announcement)
    {
        return GetPath(announcement, "en"); //todo: config
    }
    
    // === Generic ===
    public const String ViciousBlow = "8205341"; // A vicious blow! That'll leave a mark!
    public const String FeltThatOneStillStanding = "8205342"; // Even I felt that one! But the challenger's still standing!
    public const String BeautifullyDodged = "8205343"; // Beautifully dodged!
    public const String SawThroughIt = "8205344"; // Amazing! The challenger saw straight through it!
    public const String WentDownHard = "8205345"; // Oh my! The challenger went down hard!
    public const String TheyreDownIsItOver = "8205346"; // And they're down! Is it over already!?
    public const String BackUpGrit = "8205347"; // They're back on their feet! What grit!
    public const String StillInIt = "8205348"; // They're still in it! But for how long!?
    public const String WhatPower = "8205349"; // What power! Looks like someone isn't holding back!
    public const String PotentMagicks = "8205350"; // Such potent magicks! But will they be enough to turn the tide?
    public const String IroncladDefense = "8205351"; // They're bracing for the storm with an ironclad defense!
    public const String Fallen = "8205352"; // Oh dear! The challenger has fallen out of the ring!
    public const String AllOverUntilNextTime = "8205388"; // It's all over...until next time!
    public const String StruckSquare = "8205772"; // That struck square!
    public const String Oof = "8205773"; // Oof! Will the challenger be alright!?
    public const String CouldntAvoid = "8205774"; // Oh no, they couldn't avoid that one!
    public const String MustHaveHurtNotOut = "8205775"; // That must have hurt! But they're not out of the fight yet!
    public const String EffortlesslyDodged = "8205776"; // Effortlessly dodged!
    public const String ClearlyAnticipated = "8205777"; // Amazing! They clearly anticipated that attack!
    public const String StylishEvasion = "8205778"; // A stylish evasion, well done!
    public const String AvoidedWithEase = "8205779"; // Avoided with ease! They knew just what to do!
    public const String TooMuch = "8205780"; // Oh no, that was too much for the challenger!
    public const String ChallengerDownIsThisEnd = "8205781"; // The challenger's down! Is this the end!?
    public const String RisesAgain = "8205782"; // The challenger rises again! Can they turn this around!?
    public const String BackOnFeet = "8205783"; // They're back on their feet! Let's see if they can stay standing!
    
    // === Black Cat ===
    public const String MyRing = "8205356";
    public const String RepairRing = "8205357";
    
    // === Honey B. Lovely ===
    public const String VenomStrikeFem = "8205362";
    public const String FeelingLoveFem = "8205364";
    public const String ResistTheIrresistible = "8205365";
    public const String HerCharmsNotDeniedFem = "8205366";
    public const String WhatAClash = "8205368";
    
    // === Brute Bomber ===
    public const String BannedCompoundRobot = "8205440";
    public const String AssaultedRefMasc = "8205370";
    public const String FuseField = "8205376";
    public const String ChainDeathmatch = "8205377";
    
    // === Wicked Thunder ===
    public const String MassiveCannonFem = "8205380";
    public const String GrownWingsFem = "8205381";
    public const String BattleElectrifying = "8205387";
    
    // === Dancing Green ===
    public const String UpstartBegins = "8205784";
    public const String InvitationToDance = "8205786";
    
    // === Sugar Riot ===
    public const String RingBecomeDesert = "8205791";
    public const String StormOfNeedles = "8205792";
    public const String SuckedIn = "8205794";
    public const String Thunderstorm = "8205796";
    
    // === Brute Abominator ===
    public const String SentRivalFlyingMasc = "8205805";
    public const String BoundingFromWallToWallMasc = "8205807";
    public const String RoofCavedSuchDevastation = "8205808";
    public const String SendingCameras = "8205809";
    public const String StartedFire = "8205810";
    public const String DodgedEverything = "8205813";
    public const String BrutalBlow = "8205814";
    public const String ThrillingBattle = "8205815";
    
    // === Howling Blade 
    public const String SuchSpeedMasc = "8205817";
    public const String ColossalThing = "8205818";
    public const String ColossalThingSwordMasc = "8205819";
}
