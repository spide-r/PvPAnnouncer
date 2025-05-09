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

        new AllyActionEvent([(uint) ActionIds.LimitBreaksEnum.MarksmansSpite], [], [], [MassiveCannonFem],
            "Marksman's Spite (Wicked Thunder)"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaksEnum.Seraphism], [..InternalConstants.LimitBreakList], [], [GrownWingsFem],
            "Seraphism (Wicked Thunder)"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaksEnum.TenebraeLemurum], [], [], [UnleashedANewFeralSoul, ConvertAetherFem],
            "Tenebrae Lemurum (Wicked Thunder)"),
        new AllyActionEvent([ActionIds.Blota], [ChainDeathmatch], [], [], "Blota"),
        new AllyActionEvent([ActionIds.RisingPhoenix, (uint) ActionIds.BigHitsEnum.FlareStar], [StartedFire], [], [], "Rising Phoenix & Flare Star"),
        new AllyActionEvent([ActionIds.FullSwing, ActionIds.WindsReply], [], [SentRivalFlyingMasc], [], "Full Swing, Wind's Reply (Brute Bomber)"),
        new AllyActionEvent([ActionIds.Swift], [], [SuchSpeedMasc], [], "Swift (Howling Blade)"),
        new AllyActionEvent([ActionIds.Biolysis], [], [], [VenomStrikeFem], "Biolysis (Honey B. Lovely)"),
        new AllyActionEvent([(uint) ActionIds.LimitBreaksEnum.Contradance], [..InternalConstants.LimitBreakList], [],
            [FeelingLoveFem, HerCharmsNotDeniedFem], "Contradance (Honey B. Lovely)"),
        new EnemyActionEvent([(uint) ActionIds.LimitBreaksEnum.Contradance], [ResistTheIrresistible, InvitationToDance],
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
        new EnteredMechEvent(),
        new MatchStormyWeatherEvent(),
        new MaxBattleHighFlyingHighEvent()
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