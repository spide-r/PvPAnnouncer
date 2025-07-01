using System;
using System.Linq;

namespace PvPAnnouncer.Data;

public class ActionIds
{
    public enum LimitBreaks: uint
    {
        Phalanx = 29069,
        PrimalScream = 29083,
        Eventide = 29097,
        RelentlessRush = 29130,
        AfflatusPurgation = 29230,
        Seraphism = 41502,
        CelestialRiver = 29255,
        Mesotes = 29266,
        Mesotes2 = 29267,
        Meteodrive = 29485,
        SkyHigh = 29497,
        SeitonTenchu = 29515,
        SeitonTenchu2 = 29516,
        Zantetsuken = 29537,
        TenebraeLemurum = 29553,
        WorldSwallower = 39190,
        FinalFantasia = 29401,
        MarksmansSpite = 29415,
        Contradance = 29432,
        SoulResonance = 29662,
        SummonBahamut = 29673,
        SummonPhoenix = 29678,
        SouthernCross = 41498,
        AdventOfChocobastion = 39215,
    }

    public enum MechActions : uint
    {
        OpticalSight = 9971,
        SpinCrusher = 9973,
        LaserXSword = 9974,
        
        SteamRelease = 9977,
        A3000TonzeMissile = 9975,
        
        DoubleRocketPunch = 9979,
        MegaBeam = 9980,
        Flarethrower = 9978 
        
    }
        
    
    
    public enum Mitigation: uint
    {
        Guard = 29054, 
        OtherGuard = 29735, // what??
        Phalanx = 29069,
        Guardian = 29066,
        TheBlackestNight = 29063,
        Eventide = 29097,
        HeartOfCorundum = 41443,
        Expedient = 29236,
        Rampart = 43244,
        Stoneskin2 = 43256,
    }
    
    public enum BigHits: uint
    {
        ShieldSmite = 41430,
        BladeOfValor = 29073,
        PrimalRend = 29084,
        TerminalTrigger = 29131,
        TerminalTrigger2 = 29469,
        Macrocosmos = 29253,
        Toxicon2 = 29263,
        PhantomRush = 29478,
        SkyShatter = 29498,
        SkyShatter2 = 29499,
        Assassinate = 29503,
        PlentifulHarvest = 29546,
        Communio = 29554,
        SaberDance = 29420,
        FlareStar = 41480,
        FrostStar = 41481,
        Comet = 43252,
        
    }

    public static bool IsLimitBreak(uint id)
    {
        foreach (var value in Enum.GetValues<LimitBreaks>())
        {
            if (id == (uint) value)
            {
                return true;
            }
        }
        return false;
    }
    
    public static bool IsBigHit(uint id)
    {
        foreach (var value in Enum.GetValues<BigHits>())
        {
            if (id == (uint) value)
            {
                return true;
            }
        }

        foreach (var action in Enum.GetValues<MechActions>().Except([MechActions.SteamRelease, MechActions.A3000TonzeMissile]))
        {
            if (id == (uint) action)
            {
                return true;
            }
        }
        return false;
    }
    
    public static bool IsMitigation(uint id)
    {
        foreach (var value in Enum.GetValues(typeof (Mitigation)))
        {
            if (id == (uint) value)
            {
                return true;
            }
        }
        return false;
    }
    public static readonly uint SaltedEarth = 29094;
    public static readonly uint Smite = 43248;
    public static readonly uint Biolysis = 29233;
    public static readonly uint Blota = 29081;
    public static readonly uint RisingPhoenix = 29481;
    public static readonly uint Swift = 43247;
    public static readonly uint FullSwing = 43245;
    public static readonly uint WindsReply = 41509;
    public static readonly uint Perfectio = 41458;


}