using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.impl.PvPEvents;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.Impl;

public class EventListenerLoader : IEventListenerLoader
{
    public PvPEvent[] CustomPvPEvents { get; } = //todo sad! bad! delete!
    [
        new AllyActionEvent([ActionIds.HorridRoar], [RoarNidhogg], "Horrid Roar", "HorridRoarEvent"),
        new AllyActionEvent([ActionIds.WorldSwallower],
            [..InternalConstants.LimitBreakList, CruelCoil, EitherSide, SomethingRevolting], "World Swallower",
            "AllyVprLBEvent"),
        new AllyActionEvent([ActionIds.Backlash], [RegenCapacityMasc, LWCompletelyHealed, LWOutOfControl], "Backlash",
            "AllyBacklashEvent"),
        new AllyActionEvent([ActionIds.Slither], [ArmSlitheringOutDisgustingMasc, RoundRingMasc], "Slither",
            "AllySlitherEvent"),
        new AllyActionEvent([ActionIds.Phalanx], [..InternalConstants.LimitBreakList, AllTheseWeaponsMasc], "Phalanx",
            "AllyPldLBEvent"),
        new AllyActionEvent([ActionIds.Comet], [UnleashedFullMightMasc], "Comet", "AllyCometEvent"),
        new AllyActionEvent([ActionIds.MarksmansSpite], [..InternalConstants.LimitBreakList, MassiveCannonFem],
            "Marksman's Spite", "AllyMchLBEvent"),
        new AllyActionEvent([ActionIds.SoulResonance],
            [
                ..InternalConstants.LimitBreakList, GatheringAetherFem, ConvertAetherFem, DischargeAether,
                UnleashedANewFeralSoul
            ],
            "Soul Resonance", "AllyBlmLBEvent"),
        new AllyActionEvent([ActionIds.Communio], [..InternalConstants.LimitBreakList, BoundingFromWallToWallMasc],
            "Meteodrive", "AllyMnkLbEvent"),
        new AllyActionEvent([ActionIds.Seraphism],
            [..InternalConstants.LimitBreakList, GrownWingsFem, SomethingGrowingFem],
            "Seraphism", "AllySchLbEvent"),
        new AllyActionEvent([ActionIds.TenebraeLemurum], [
                ..InternalConstants.LimitBreakList, EitherSide, UnleashedANewFeralSoul, ConvertAetherFem,
                SomethingGrowingFem, EvenMoreAether, TTAlteredFormMasc, BadFeeling, Transforming, ItsAliveLindwurm
            ],
            "Tenebrae Lemurum", "AllyRprLBEvent"),
        new AllyActionEvent([ActionIds.FullSwing, ActionIds.WindsReply], [SentRivalFlyingMasc],
            "Full Swing, Wind's Reply", "AllyKBActionEvent"),
        new AllyActionEvent([ActionIds.Swift], [SuchSpeedMasc], "Swift", "AllySwiftEvent"),
        new AllyActionEvent([ActionIds.Biolysis], [VenomStrikeFem], "Biolysis", "AllyBiolysisEvent"),
        new EnemyActionEvent([ActionIds.Biolysis], [StruckWithVenon, MutationCorrupting], [], "Biolysis From Enemies",
            "EnemyBiolysisEvent"),
        new AllyActionEvent([ActionIds.Contradance],
            [..InternalConstants.LimitBreakList, HerCharmsNotDeniedFem, FeelingLoveFem], "Contradance",
            "AllyDncLBEvent"),
        new AllyActionEvent([ActionIds.Flarethrower], [StartedFire, BurnBurn], "Flarethrower (Rival Wings)",
            "AllyFlarethrowerEvent"),
        new EnemyActionEvent([ActionIds.Flarethrower], [StartedFire, BurnBurn], [], "Enemy Flarethrower (Rival Wings)",
            "EnemyFlarethrowerEvent"),
        new EnemyActionEvent([ActionIds.Contradance], [ResistTheIrresistible, InvitationToDance],
            [], "Enemy Contradance", "EnemyDncLBEvent"),
        new AllyActionEvent([
                ActionIds.Zantetsuken, ActionIds.SeitonTenchu,
                ActionIds.SeitonTenchu2, ActionIds.Perfectio
            ],
            [BannedCompoundRobot, VFWickedWeapon, UnusedOhMercyFem, UnusedNoRespectMasc, Hahahahahaha, Kill, More, Rend], "Instant Kills",
            "AllyInstantKillEvent"),
        new AllyActionEvent([ActionIds.RisingPhoenix, ActionIds.FlareStar, ActionIds.Scorch], [StartedFire, BurnBurn, InfernoSwelling],
            "Rising Phoenix/Flare Star/Scorch", "AllyFireEvent"),
        new AllyActionEvent([ActionIds.Blota], [ChainDeathmatch], "Blota", "AllyPullEvent"),
        new AllyActionEvent([ActionIds.Communio], [PunishingAttackFusion], "Communio", "AllyCommunioEvent"),
        new AllyActionEvent([ActionIds.CelestialRiver],
            [..InternalConstants.LimitBreakList, River, StarsFavorUs, DanceOfTheStars],
            "Celestial River", "AllyAstLbEvent")
    ];

    public PvPEvent[] StandardPvpEvents { get; } = [


        // == Standard Events ==
        new MatchVictoryEvent(),
        new MatchLossEvent(),
        new AllyBurstedEvent(),
        new AllyDeathEvent(),
        new AllyHitEnemyHardEvent(),
        new AllyHitByLimitBreakEvent(),
        new AllyHitUnderGuardEvent(),
        new AllyLimitBreakEvent(),
        new AllyMitUsedEvent(),
        new AllyPulledByDrkEvent(),
        new AllyResurrectEvent(),
        new AllyZoneOutEvent(),
        new EnemyMissedCc(),
        new MatchEndEvent(),
        new MatchStartEvent(),
        new EnteredMechEvent(),
        new MatchStormyWeatherEvent(),
        new MaxBattleFeverEvent()
    ];

    public PvPEvent[] GetPvPEvents()
    {
        return [ ..CustomPvPEvents, ..StandardPvpEvents];
    }
    public void LoadEventListeners()
    {
        PluginServices.PluginLog.Verbose("Loading event listeners");
        foreach (var ee in CustomPvPEvents)
        {
            PluginServices.PvPEventBroker.RegisterListener(ee);

        }
        
        foreach (var ee in StandardPvpEvents)
        {
            PluginServices.PvPEventBroker.RegisterListener(ee);
        }

       

    }
    
}