using System;

namespace PvPAnnouncer.Data;

public static class AnnouncerLines
{
    //Announcer sound path: sound/voice/vo_line/INSERT_NUMBER_HERE_en.scd
    // en, de, fr work - havent found where jp went

    public static String GetPath(String announcement, String lang)
    {
        return "sound/voice/vo_line/" + announcement + "_" + lang + ".scd";
    }
    
    // === Generic ===
    public const String ViciousBlow = "8205341";
    public const String FeltThatOneStillStanding = "8205342";
    public const String BeautifullyDodged = "8205343";
    public const String SawThroughIt = "8205344";
    public const String WentDownHard = "8205345";
    public const String TheyreDownIsItOver = "8205346";
    public const String BackUpGrit = "8205347";
    public const String StillInIt = "8205348";
    public const String WhatPower = "8205349";
    public const String PotentMagicks = "8205350";
    public const String IroncladDefense = "8205351";
    public const String Fallen = "8205352";
    public const String AllOverUntilNextTime = "8205388";
    public const String StruckSquare = "8205772";
    public const String Oof = "8205773";
    public const String CouldntAvoid = "8205774";
    public const String MustHaveHurtNotOut = "8205775";
    public const String EffortlesslyDodged = "8205776";
    public const String ClearlyAnticipated = "8205777";
    public const String StylishEvasion = "8205778";
    public const String AvoidedWithEase = "8205779";
    public const String TooMuch = "8205780";
    public const String TheyreDownIsThisEnd = "8205781";
    public const String RisesAgain = "8205782";
    public const String BackOnFeet = "8205783";
    
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
    public const String SuchSpeed = "8205817";
    public const String ColossalThing = "8205818";
    public const String ColossalThingSwordMasc = "8205819";





















    




    




 
    const String EH_GADS = "example/egads.ogg";
}
