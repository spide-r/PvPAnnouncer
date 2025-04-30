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
        //todo: gain battle high event

        // == Custom Events ==

        new AllyActionEvent((uint) ActionIds.LimitBreaksEnum.MarksmansSpite, [], [], [MassiveCannonFem],
            "Marksman's Spite (Wicked Thunder)"),
        new AllyActionEvent((uint) ActionIds.LimitBreaksEnum.Seraphism, [], [], [GrownWingsFem],
            "Seraphism (Wicked Thunder)"),
        new AllyActionEvent((uint) ActionIds.LimitBreaksEnum.TenebraeLemurum, [], [], [UnleashedANewFeralSoul],
            "Tenebrae Lemurum (Wicked Thunder)"),
        new AllyActionEvent(ActionIds.Blota, [ChainDeathmatch], [], [], "Blota"),
        new AllyActionEvent(ActionIds.RisingPhoenix, [StartedFire], [], [], "Rising Phoenix"),
        new AllyActionEvent(ActionIds.FullSwing, [], [SentRivalFlyingMasc], [], "Full Swing (Brute Bomber)"),
        new AllyActionEvent(ActionIds.Swift, [], [SuchSpeedMasc], [], "Swift (Howling Blade)"),
        new AllyActionEvent(ActionIds.Biolysis, [], [], [VenomStrikeFem], "Biolysis (Honey B. Lovely)"),
        new AllyActionEvent((uint) ActionIds.LimitBreaksEnum.Contradance, [..InternalConstants.LimitBreakList], [],
            [FeelingLoveFem, HerCharmsNotDeniedFem], "Contradance (Honey B. Lovely)"),
        new EnemyActionEvent((uint) ActionIds.LimitBreaksEnum.Contradance, [ResistTheIrresistible, InvitationToDance],
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
        new MechKilledEvent(),
        new MechSpawnEvent(),
        new MatchStormyWeatherEvent()
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