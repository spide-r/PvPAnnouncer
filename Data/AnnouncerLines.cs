using System;

namespace PvPAnnouncer.Data;

public static class AnnouncerLines
{
    //Announcer sound path: sound/voice/vo_line/INSERT_NUMBER_HERE_en.scd
    // en, de, fr, ja work
    
    //Lines are found in ContentDirectorTalk as well as InstanceContentTextData - Use AlphaAOT 

    private static string GetPath(string announcement, string lang)
    {
        return "sound/voice/vo_line/" + announcement + "_" + lang + ".scd";
    }

    public static string GetPath(string announcement)
    {
        return GetPath(announcement, PluginServices.Config.Language);
    }
    
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
    
    // === Black Cat ===
    public const string MyRing = "8205356";
    public const string RepairRing = "8205357";
    
    // === Honey B. Lovely ===
    public const string VenomStrikeFem = "8205362";
    public const string FeelingLoveFem = "8205364";
    public const string ResistTheIrresistible = "8205365";
    public const string HerCharmsNotDeniedFem = "8205366";
    public const string WhatAClash = "8205368";
    
    // === Brute Bomber ===
    public const string BannedCompoundRobot = "8205440";
    public const string AssaultedRefMasc = "8205370";
    public const string FuseField = "8205376";
    public const string ChainDeathmatch = "8205377";
    
    // === Wicked Thunder ===
    public const string MassiveCannonFem = "8205380";
    public const string GrownWingsFem = "8205381";
    public const string BattleElectrifying = "8205387";
    
    // === Dancing Green ===
    public const string UpstartBegins = "8205784";
    public const string InvitationToDance = "8205786";
    
    // === Sugar Riot ===
    public const string RingBecomeDesert = "8205791";
    public const string StormOfNeedles = "8205792";
    public const string SuckedIn = "8205794";
    public const string Thunderstorm = "8205796";
    
    // === Brute Abominator ===
    public const string SentRivalFlyingMasc = "8205805";
    public const string BoundingFromWallToWallMasc = "8205807";
    public const string RoofCavedSuchDevastation = "8205808";
    public const string SendingCameras = "8205809";
    public const string StartedFire = "8205810";
    public const string DodgedEverything = "8205813";
    public const string BrutalBlow = "8205814";
    public const string ThrillingBattle = "8205815";
    
    // === Howling Blade 
    public const string SuchSpeedMasc = "8205817";
    public const string ColossalThing = "8205818";
    public const string ColossalThingSwordMasc = "8205819";
}
