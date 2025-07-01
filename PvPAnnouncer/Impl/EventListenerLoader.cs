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

        new AllyActionEvent([(uint) ActionIds.LimitBreaks.MarksmansSpite], [], [], [MassiveCannonFem],
            "Marksman's Spite (Wicked Thunder)"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Seraphism], [..InternalConstants.LimitBreakList], [], [GrownWingsFem],
            "Seraphism (Wicked Thunder)"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.TenebraeLemurum], [..InternalConstants.LimitBreakList], [], [UnleashedANewFeralSoul, ConvertAetherFem],
            "Tenebrae Lemurum (Wicked Thunder)"),
        new AllyActionEvent([ActionIds.Blota], [ChainDeathmatch], [], [], "Blota"),
        new AllyActionEvent([ActionIds.RisingPhoenix, (uint) ActionIds.BigHits.FlareStar], [StartedFire], [], [], "Rising Phoenix & Flare Star"),
        new AllyActionEvent([ActionIds.FullSwing, ActionIds.WindsReply], [], [SentRivalFlyingMasc], [], "Full Swing, Wind's Reply (Brute Bomber)"),
        new AllyActionEvent([ActionIds.Swift], [], [SuchSpeedMasc], [], "Swift (Howling Blade)"),
        new AllyActionEvent([ActionIds.Biolysis], [], [], [VenomStrikeFem], "Biolysis (Honey B. Lovely)"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Zantetsuken, (uint) ActionIds.LimitBreaks.SeitonTenchu, (uint) ActionIds.LimitBreaks.SeitonTenchu2, ActionIds.Perfectio], [], [UnusedNoRespectMasc], [UnusedOhMercyFem], "Instant Kills (Brute Bomber + Black Cat)"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaks.Contradance], [..InternalConstants.LimitBreakList], [],
            [FeelingLoveFem, HerCharmsNotDeniedFem], "Contradance (Honey B. Lovely)"),
        
        new AllyActionEvent([(uint) ActionIds.MechActions.Flarethrower], [StartedFire], [],
            [], "Flarethrower (Rival Wings)"),
        
        new EnemyActionEvent([(uint) ActionIds.MechActions.Flarethrower], [StartedFire], [],
            [], "Flarethrower from Enemies (Rival Wings)"),
        
        new EnemyActionEvent([(uint) ActionIds.LimitBreaks.Contradance], [ResistTheIrresistible, InvitationToDance],
            [], [], "Contradance from enemies."),
        
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