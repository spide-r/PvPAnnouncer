using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.impl.PvPEvents;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.Impl;

public class EventListenerLoader: IEventListenerLoader
{
    public PvPEvent[] PvpEvents { get; } =
    [
        
        // == Custom Events ==
        
        new AllyActionEvent([ ActionIds.WorldSwallower], [..InternalConstants.LimitBreakList, CruelCoil, EitherSide, SomethingRevolting], "World Swallower", "AllyVprLBEvent"),
        new AllyActionEvent([ActionIds.Backlash], [RegenCapacityMasc, LWCompletelyHealed, LWOutOfControl], "Backlash", "AllyBacklashEvent"),
        new AllyActionEvent([ActionIds.Slither], [ArmSlitheringOutDisgustingMasc, RoundRingMasc], "Slither (Masculine Pronouns)", "AllySlitherEvent"),
        new AllyActionEvent([ ActionIds.Phalanx], [..InternalConstants.LimitBreakList, AllTheseWeaponsMasc], "Phalanx (Masculine Pronouns)", "AllyPldLBEvent"),
        new AllyActionEvent([ ActionIds.Comet], [UnleashedFullMightMasc], "Comet (Masculine Pronouns)", "AllyCometEvent"),
        new AllyActionEvent([ ActionIds.MarksmansSpite], [..InternalConstants.LimitBreakList, MassiveCannonFem],
            "Marksman's Spite (Feminine Pronouns)", "AllyMchLBEvent"),
        new AllyActionEvent([ ActionIds.SoulResonance], [..InternalConstants.LimitBreakList, GatheringAetherFem, ConvertAetherFem, DischargeAether, UnleashedANewFeralSoul],
            "Soul Resonance (Feminine Pronouns)", "AllyBlmLBEvent"),
        new AllyActionEvent([ActionIds.Communio], [..InternalConstants.LimitBreakList, BoundingFromWallToWallMasc],"Meteodrive (Masculine Pronouns)", "AllyMnkLbEvent"),
        new AllyActionEvent([ ActionIds.Seraphism], [..InternalConstants.LimitBreakList, GrownWingsFem, SomethingGrowingFem],
            "Seraphism (Feminine Pronouns)", "AllySchLbEvent"),
        new AllyActionEvent([ ActionIds.TenebraeLemurum], [..InternalConstants.LimitBreakList, EitherSide, UnleashedANewFeralSoul, ConvertAetherFem, 
                SomethingGrowingFem, EvenMoreAether, TTAlteredFormMasc, BadFeeling, Transforming, ItsAliveLindwurm], 
            "Tenebrae Lemurum", "AllyRprLBEvent"),
        new AllyActionEvent([ActionIds.FullSwing, ActionIds.WindsReply], [SentRivalFlyingMasc], 
            "Full Swing, Wind's Reply (Masculine Pronouns)", "AllyKBActionEvent"),
        new AllyActionEvent([ActionIds.Swift], [SuchSpeedMasc], "Swift (Masculine Pronouns)", "AllySwiftEvent"),
        new AllyActionEvent([ActionIds.Biolysis], [VenomStrikeFem], "Biolysis (Feminine Pronouns)", "AllyBiolysisEvent"),
        new EnemyActionEvent([ActionIds.Biolysis], [StruckWithVenon, MutationCorrupting], [], "Biolysis From Enemies", "EnemyBiolysisEvent"),
        new AllyActionEvent([ ActionIds.Contradance], [..InternalConstants.LimitBreakList, HerCharmsNotDeniedFem, FeelingLoveFem],"Contradance (Feminine Pronouns)", "AllyDncLBEvent"),
        new AllyActionEvent([ ActionIds.Flarethrower], [StartedFire], "Flarethrower (Rival Wings)", "AllyFlarethrowerEvent"),
        new EnemyActionEvent([ ActionIds.Flarethrower], [StartedFire], [], "Enemy Flarethrower (Rival Wings)", "EnemyFlarethrowerEvent"),
        new EnemyActionEvent([ ActionIds.Contradance], [ResistTheIrresistible, InvitationToDance],
            [], "Enemy Contradance", "EnemyDncLBEvent"),
        new AllyActionEvent([ ActionIds.Zantetsuken,  ActionIds.SeitonTenchu, 
                 ActionIds.SeitonTenchu2, ActionIds.Perfectio], 
            [BannedCompoundRobot, VFWickedWeapon, UnusedOhMercyFem, UnusedNoRespectMasc], "Instant Kills", "AllyInstantKillEvent"),
        new AllyActionEvent([ActionIds.RisingPhoenix,  ActionIds.FlareStar], [StartedFire], "Rising Phoenix & Flare Star", "AllyFireEvent"),
        new AllyActionEvent([ActionIds.Blota], [ChainDeathmatch], "Blota", "AllyPullEvent"),
        new AllyActionEvent([ActionIds.Communio], [PunishingAttackFusion], "Communio", "AllyCommunioEvent"),
        new AllyActionEvent([ ActionIds.CelestialRiver], [..InternalConstants.LimitBreakList, River, StarsFavorUs, DanceOfTheStars],
            "Celestial River", "AllyAstLbEvent"),

        
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

    public PvPEvent[] GetPvPEvents() => PvpEvents;
    public void LoadEventListeners()
    {
        PluginServices.PluginLog.Verbose("Loading event listeners");
        foreach (var ee in PvpEvents)
        {
            PluginServices.PvPEventBroker.RegisterListener(ee);
        }
    }
}