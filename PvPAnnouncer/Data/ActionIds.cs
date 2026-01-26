using System.Collections.Generic;
using System.Linq;

namespace PvPAnnouncer.Data;

public class ActionIds
{


// Mitigation
public static readonly uint Guard = 29054;
public static readonly uint OtherGuard = 29735; // what??
public static readonly uint Guardian = 29066;
public static readonly uint TheBlackestNight = 29063;
public static readonly uint HeartOfCorundum = 41443;
public static readonly uint Expedient = 29236;
public static readonly uint Rampart = 43244;
public static readonly uint Stoneskin2 = 43256;

// Mech Actions
public static readonly uint OpticalSight = 9971;
public static readonly uint SpinCrusher = 9973;
public static readonly uint LaserXSword = 9974;
public static readonly uint SteamRelease = 9977;
public static readonly uint A3000TonzeMissile = 9975;
public static readonly uint DoubleRocketPunch = 9979;
public static readonly uint MegaBeam = 9980;
public static readonly uint Flarethrower = 9978;

// Limit Breaks
public static readonly uint Phalanx = 29069;
public static readonly uint PrimalScream = 29083;
public static readonly uint RelentlessRush = 29130;
public static readonly uint Eventide = 29097;
public static readonly uint AfflatusPurgation = 29230;
public static readonly uint Seraphism = 41502;
public static readonly uint CelestialRiver = 29255;
public static readonly uint Mesotes = 29266;
public static readonly uint Mesotes2 = 29267;
public static readonly uint Meteodrive = 29485;
public static readonly uint SkyHigh = 29497;
public static readonly uint SeitonTenchu = 29515;
public static readonly uint SeitonTenchu2 = 29516;
public static readonly uint Zantetsuken = 29537;
public static readonly uint TenebraeLemurum = 29553;
public static readonly uint WorldSwallower = 39190;
public static readonly uint FinalFantasia = 29401;
public static readonly uint MarksmansSpite = 29415;
public static readonly uint Contradance = 29432;
public static readonly uint SoulResonance = 29662;
public static readonly uint SummonBahamut = 29673;
public static readonly uint SummonPhoenix = 29678;
public static readonly uint SouthernCross = 41498;
public static readonly uint AdventOfChocobastion = 39215;

// Limit Break Related Accessories
public static readonly uint BladeOfFaith = 29071;
public static readonly uint BladeOfTruth = 29072;
public static readonly uint BladeOfValor = 29073;
public static readonly uint StarPrism = 39217;
public static readonly uint Oracle = 41508;
public static readonly uint Ouroboros = 39173;
public static readonly uint TerminalTrigger = 29131;
public static readonly uint TerminalTrigger2 = 29469;
public static readonly uint SkyShatter = 29498;
public static readonly uint SkyShatter2 = 29499;
public static readonly uint FlareStar = 41480;
public static readonly uint FrostStar = 41481;
public static readonly uint Deathflare = 41484;
public static readonly uint BrandOfPurgatory = 41485;
public static readonly uint Communio = 29554;
public static readonly uint Perfectio = 41458;
public static readonly uint EncoreOfLight = 41467;


// Burst Actions

public static readonly uint Scorch = 41491;
public static readonly uint Prefulgence = 41495;
public static readonly uint Xenoglossy = 29658;
public static readonly uint MountainBuster = 29671;
public static readonly uint MogOfTheAges = 39782;
public static readonly uint Madeen = 39783;
public static readonly uint SaltedEarth = 29094;
public static readonly uint ShieldSmite = 41430;
public static readonly uint Confetior = 42194;
public static readonly uint PrimalRend = 29084;
public static readonly uint FatedCircle = 41511;
public static readonly uint Macrocosmos = 29253;
public static readonly uint Lord = 41505;
public static readonly uint Toxicon2 = 29263;
public static readonly uint Pneuma = 29706;
public static readonly uint ChainStrat = 29716;
public static readonly uint PhantomRush = 29478;
public static readonly uint Assassinate = 29503;
public static readonly uint Hyosho = 29506;
public static readonly uint PlentifulHarvest = 29546;
public static readonly uint Wyrmwind = 29495;
public static readonly uint Ogi1 = 29530;
public static readonly uint Ogi2 = 29531;
public static readonly uint Backlash = 39187;
public static readonly uint SaberDance = 29420;
public static readonly uint Starfall = 29421;
public static readonly uint HarmonicArrow = 41964;
public static readonly uint PitchPerfect = 29392;
public static readonly uint Drill = 29405;
public static readonly uint AirAnchor = 29407;
public static readonly uint Chainsaw = 29408;
public static readonly uint FullMetalField = 41469;



// Other Actions
public static readonly uint Smite = 43248;
public static readonly uint Biolysis = 29233;
public static readonly uint Blota = 29081;
public static readonly uint RisingPhoenix = 29481;
public static readonly uint Swift = 43247;
public static readonly uint FullSwing = 43245;
public static readonly uint WindsReply = 41509;
public static readonly uint Comet = 43252;
public static readonly uint Slither = 39184;
public static readonly uint HorridRoar = 29496;





    private static HashSet<uint> _limitBreaksSet =
    [
        Phalanx, PrimalScream, Eventide, RelentlessRush,
        AfflatusPurgation, Seraphism, CelestialRiver, Mesotes,
        Mesotes2, Meteodrive, SkyHigh, SeitonTenchu,
        SeitonTenchu2, Zantetsuken, TenebraeLemurum, WorldSwallower,
        FinalFantasia, MarksmansSpite, Contradance, SoulResonance,
        SummonBahamut, SummonPhoenix, SouthernCross,
        AdventOfChocobastion
    ];

    private static readonly HashSet<uint> MechActionsSet =
    [
        OpticalSight, SpinCrusher, LaserXSword, SteamRelease,
        A3000TonzeMissile, DoubleRocketPunch, MegaBeam, Flarethrower
    ];
    
    
    private static readonly HashSet<uint> MitigationSet = [
        Guard, OtherGuard, Phalanx, Guardian, TheBlackestNight,
        Eventide, HeartOfCorundum, Expedient, Rampart,
        Stoneskin2 
    ];
    
    private static readonly HashSet<uint> BurstActions =
    [
        Scorch, Prefulgence, Xenoglossy, MountainBuster, MogOfTheAges, Madeen, SaltedEarth,
        ShieldSmite, Confetior, PrimalRend, FatedCircle, Macrocosmos, Lord, Toxicon2, Pneuma, ChainStrat,
        PhantomRush, Assassinate, Hyosho, PlentifulHarvest, Wyrmwind, Ogi1, Ogi2, Backlash, SaberDance,
        Starfall, HarmonicArrow, PitchPerfect, Drill, AirAnchor, Chainsaw, FullMetalField
    ];




    public static bool IsLimitBreak(uint id)
    {
        return _limitBreaksSet.Contains(id);
    }

    public static bool IsLimitBreakAttack(uint id)
    {
        return LimitBreaksHitSet.Contains(id);
    }
    
    private static readonly HashSet<uint> LimitBreaksHitSet =
    [
        BladeOfFaith,BladeOfTruth,BladeOfValor, PrimalScream, Eventide, RelentlessRush, TerminalTrigger, TerminalTrigger2,
        AfflatusPurgation, CelestialRiver, Oracle, Mesotes,
        Mesotes2, Meteodrive, SkyShatter, SkyShatter2, SeitonTenchu,
        SeitonTenchu2, Zantetsuken, TenebraeLemurum, Communio, Perfectio, WorldSwallower, Ouroboros,
        EncoreOfLight, MarksmansSpite, Contradance, FlareStar, FrostStar, 
        Deathflare, BrandOfPurgatory, SouthernCross,
        AdventOfChocobastion, StarPrism
    ];
    
    public static bool IsBurst(uint id)
    {
        if (BurstActions.Contains(id))
        {
            return true;
        }

        if (LimitBreaksHitSet.Contains(id))
        {
            return true;
        }

        if (MechActionsSet.Except([SteamRelease, A3000TonzeMissile])
            .Contains(id))
        {
            return true;
        }
        return false;
    }
    
    public static bool IsMitigation(uint id)
    {
        return MitigationSet.Contains(id);
    }


}