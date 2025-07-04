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

        new AllyActionEvent([(uint) ActionIds.LimitBreaks.MarksmansSpite], [], new Dictionary<Personalization, List<string>>{{ Personalization.FemPronouns, [MassiveCannonFem]}},
            "Marksman's Spite (Wicked Thunder)", "AllyMchLBEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Seraphism], [..InternalConstants.LimitBreakList], 
            new Dictionary<Personalization, List<string>>{{Personalization.FemPronouns, [GrownWingsFem, SomethingGrowingFem]}},
            "Seraphism (Wicked Thunder)", "AllySchLbEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.TenebraeLemurum], [..InternalConstants.LimitBreakList], 
            new Dictionary<Personalization, List<string>>
            {
                {Personalization.FemPronouns, [UnleashedANewFeralSoul, ConvertAetherFem, SomethingGrowingFem]}
            },
            "Tenebrae Lemurum (Wicked Thunder)", "AllyRprLBEvent"),
        new AllyActionEvent([ActionIds.Blota], [ChainDeathmatch], [], "Blota", "AllyPullEvent"),
        new AllyActionEvent([ActionIds.RisingPhoenix, (uint) ActionIds.BigHits.FlareStar], [StartedFire], 
            [], "Rising Phoenix & Flare Star", "AllyFireEvent"),
        new AllyActionEvent([ActionIds.FullSwing, ActionIds.WindsReply], [], 
            new Dictionary<Personalization, List<string>>{
            { Personalization.MascPronouns, [SentRivalFlyingMasc]}
            }, "Full Swing, Wind's Reply (Brute Bomber)", "AllyKBActionEvent"),
        new AllyActionEvent([ActionIds.Swift], [], new Dictionary<Personalization, 
            List<string>>{{ Personalization.MascPronouns, [SuchSpeedMasc]}}, "Swift (Howling Blade)", "AllySwiftEvent"),
        new AllyActionEvent([ActionIds.Biolysis], [], new Dictionary<Personalization, 
            List<string>>{{ Personalization.FemPronouns, [VenomStrikeFem]}}, "Biolysis (Honey B. Lovely)", "AllyBiolysisEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Zantetsuken, (uint) ActionIds.LimitBreaks.SeitonTenchu, 
            (uint) ActionIds.LimitBreaks.SeitonTenchu2, ActionIds.Perfectio], 
            [BannedCompoundRobot], new Dictionary<Personalization, List<string>>
            {
                {Personalization.MascPronouns, [UnusedNoRespectMasc]}, {Personalization.FemPronouns, [UnusedOhMercyFem]}
            }, "Instant Kills (Brute Bomber + Black Cat)", "AllyInstantKillEvent"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Contradance], [..InternalConstants.LimitBreakList], 
            new Dictionary<Personalization, List<string>>{{Personalization.FemPronouns, [HerCharmsNotDeniedFem, FeelingLoveFem]}}, "Contradance (Honey B. Lovely)", "AllyDncLBEvent"),
        
        new AllyActionEvent([(uint) ActionIds.MechActions.Flarethrower], [StartedFire], [], "Flarethrower (Rival Wings)", "AllyFlarethrowerEvent"),
        
        new EnemyActionEvent([(uint) ActionIds.MechActions.Flarethrower], [StartedFire], [], "Flarethrower from Enemies (Rival Wings)", "EnemyFlarethrowerEvent"),
        
        new EnemyActionEvent([(uint) ActionIds.LimitBreaks.Contradance], [ResistTheIrresistible, InvitationToDance],
            [], "Contradance from enemies.", "EnemyDncLBEvent"),
        
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