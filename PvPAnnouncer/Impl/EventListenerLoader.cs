using System;
using System.Collections.Generic;
using System.Reflection;
using PvPAnnouncer.Data;
using PvPAnnouncer.impl.PvPEvents;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.Impl;

public class EventListenerLoader : IEventListenerLoader
{
    private PvPEvent[] StandardPvpEvents { get; } = [

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
    public void LoadEventListeners()
    {
        
        PluginServices.PluginLog.Verbose("Loading Standard event listeners");
        
        foreach (var ee in StandardPvpEvents)
        {
            PluginServices.PvPEventBroker.RegisterListener(ee);
        }
    }
    
}