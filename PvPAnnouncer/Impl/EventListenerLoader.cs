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

    [Obsolete(message:"Replace me with something idk")]
    public PvPEvent[] GetPvPEvents()
    {
        return StandardPvpEvents;
    }
    public void LoadEventListeners()
    {
        PluginServices.PluginLog.Verbose("Loading Standard event listeners");
        
        foreach (var ee in StandardPvpEvents)
        {
            PluginServices.PvPEventBroker.RegisterListener(ee);
        }

       

    }
    
}