using System;
using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyBurstedEvent: PvPEvent
{
    private long _lastHit = 0;
    private readonly HashSet<int> _hitters = [];
    
    public AllyBurstedEvent()
    {
        Name = "Bursted By Enemy Team";
        InternalName = "AllyBurstedEvent";
    }

    public override List<string> SoundPaths()
    {
        return [CantBeCareless, OofMustHaveHurt, NotFastEnough, NowhereLeft, RainOfDeath, SuchScorn];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>
        {
            {Personalization.BruteBomber, [BBDesprate]},
        };
        
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (Enumerable.Contains(pp.GetTargetIds(), (uint) pp.SourceId))
            {
                // we dont want self attacks triggering this todo: code duplication
                return false;
            }
            
            PluginServices.PluginLog.Verbose($"{_lastHit}, {_hitters.Count}");
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {

                    var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    if (unixTime - _lastHit > 3)
                    {
                        _hitters.Clear();
                    }

                    if (_hitters.Contains(pp.SourceId))
                    {
                        return false;
                    }

                    if (!ActionIds.IsBurst(pp.ActionId))
                    {
                        return false;
                    }

                    if (_hitters.Count > 4)
                    {
                        _hitters.Clear();
                        return true;
                    }

                    _hitters.Add(pp.SourceId);
                    _lastHit = unixTime;
                    return false;
                }
            }
        }
        return false;    }
}