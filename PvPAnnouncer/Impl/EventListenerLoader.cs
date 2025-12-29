using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.impl.PvPEvents;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.Impl;

public class EventListenerLoader: IEventListenerLoader
{
    public PvPEvent[] PvpEvents { get; } =
    [
        
        // == Custom Events ==
        
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.WorldSwallower], [..InternalConstants.LimitBreakList, CruelCoil, EitherSide, SomethingRevolting], [], "World Swallower", "AllyVprLBEvent"),
        new AllyActionEvent([ActionIds.Backlash], [], new Dictionary<Personalization, List<string>>{
            {Personalization.MascPronouns, [RegenCapacityMasc]}, {Personalization.President, [LWCompletelyHealed, LWOutOfControl]}}, "Backlash", "AllyBacklashEvent"),
        new AllyActionEvent([ActionIds.Slither], [], new Dictionary<Personalization, List<string>>{
            {Personalization.MascPronouns, [ArmSlitheringOutDisgustingMasc, RoundRingMasc]}}, "Slither (Masculine Pronouns)", "AllySlitherEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Phalanx], [..InternalConstants.LimitBreakList, AllTheseWeaponsMasc], [], "Phalanx (Masculine Pronouns)", "AllyPldLBEvent"),
        new AllyActionEvent([ ActionIds.Comet], [], new Dictionary<Personalization, List<string>>{
            {Personalization.MascPronouns, [UnleashedFullMightMasc]}}, "Comet (Masculine Pronouns)", "AllyCometEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.MarksmansSpite], [..InternalConstants.LimitBreakList], new Dictionary<Personalization, List<string>>{{ Personalization.FemPronouns, [MassiveCannonFem]}},
            "Marksman's Spite (Feminine Pronouns)", "AllyMchLBEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.SoulResonance], [..InternalConstants.LimitBreakList], new Dictionary<Personalization, List<string>>{{ Personalization.FemPronouns, [GatheringAetherFem, ConvertAetherFem, DischargeAether, UnleashedANewFeralSoul]}},
            "Soul Resonance (Feminine Pronouns)", "AllyBlmLBEvent"),
        new AllyActionEvent([ActionIds.Communio], [..InternalConstants.LimitBreakList, BoundingFromWallToWallMasc], [], "Meteodrive (Masculine Pronouns)", "AllyMnkLbEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Seraphism], [..InternalConstants.LimitBreakList], 
            new Dictionary<Personalization, List<string>>{{Personalization.FemPronouns, [GrownWingsFem, SomethingGrowingFem]}},
            "Seraphism (Feminine Pronouns)", "AllySchLbEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.TenebraeLemurum], [..InternalConstants.LimitBreakList, EitherSide], 
            new Dictionary<Personalization, List<string>>
            {
                {Personalization.FemPronouns, [UnleashedANewFeralSoul, ConvertAetherFem, SomethingGrowingFem, EvenMoreAether]},
                {Personalization.Tyrant, [TTAlteredFormMasc]}
            },
            "Tenebrae Lemurum", "AllyRprLBEvent"),
        new AllyActionEvent([ActionIds.FullSwing, ActionIds.WindsReply], [], 
            new Dictionary<Personalization, List<string>>{
            { Personalization.MascPronouns, [SentRivalFlyingMasc]}
            }, "Full Swing, Wind's Reply (Masculine Pronouns)", "AllyKBActionEvent"),
        new AllyActionEvent([ActionIds.Swift], [], new Dictionary<Personalization, 
            List<string>>{{ Personalization.MascPronouns, [SuchSpeedMasc]}}, "Swift (Masculine Pronouns)", "AllySwiftEvent"),
        new AllyActionEvent([ActionIds.Biolysis], [], new Dictionary<Personalization, 
            List<string>>{{ Personalization.FemPronouns, [VenomStrikeFem]}}, "Biolysis (Feminine Pronouns)", "AllyBiolysisEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Contradance], [..InternalConstants.LimitBreakList], 
            new Dictionary<Personalization, List<string>>{{Personalization.FemPronouns, [HerCharmsNotDeniedFem, FeelingLoveFem]}}, "Contradance (Feminine Pronouns)", "AllyDncLBEvent"),
        new AllyActionEvent([(uint) ActionIds.MechActions.Flarethrower], [StartedFire], [], "Flarethrower (Rival Wings)", "AllyFlarethrowerEvent"),
        new EnemyActionEvent([(uint) ActionIds.MechActions.Flarethrower], [StartedFire], [], "Enemy Flarethrower (Rival Wings)", "EnemyFlarethrowerEvent"),
        new EnemyActionEvent([(uint) ActionIds.LimitBreaks.Contradance], [ResistTheIrresistible, InvitationToDance],
            [], "Enemy Contradance", "EnemyDncLBEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Zantetsuken, (uint) ActionIds.LimitBreaks.SeitonTenchu, 
                (uint) ActionIds.LimitBreaks.SeitonTenchu2, ActionIds.Perfectio], 
            [BannedCompoundRobot], new Dictionary<Personalization, List<string>>
            {
                {Personalization.MascPronouns, [UnusedNoRespectMasc]}, {Personalization.FemPronouns, [UnusedOhMercyFem]},
                {Personalization.VampFatale, [VFWickedWeapon]}
            }, "Instant Kills", "AllyInstantKillEvent"),
        new AllyActionEvent([ActionIds.RisingPhoenix, (uint) ActionIds.BigHits.FlareStar], [StartedFire], 
            [], "Rising Phoenix & Flare Star", "AllyFireEvent"),
        new AllyActionEvent([ActionIds.Blota], [ChainDeathmatch], [], "Blota", "AllyPullEvent"),
        new AllyActionEvent([ActionIds.Communio], [PunishingAttackFusion], [], "Communio", "AllyCommunioEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.CelestialRiver], [..InternalConstants.LimitBreakList, River], 
            [],
            "Celestial River", "AllyAstLbEvent"),

        
        // == Standard Events ==
        new AllyDeathEvent(),
        new AllyHitEnemyHardEvent(),
        new AllyHitHardEvent(),
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