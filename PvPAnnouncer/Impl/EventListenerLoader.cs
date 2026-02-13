using System;
using System.Collections.Generic;
using System.Reflection;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.PvPEvents;
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
       // new MatchStormyWeatherEvent(), hidden b/c it only activated at start of the match and there was a gotcha where events would fire before pvp started - will probably delete 
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