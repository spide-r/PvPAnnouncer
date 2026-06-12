using System;
using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl.PvPEvents;

public class AllyBurstedEvent : PvPEvent
{
    private readonly Dictionary<int, long> _lastHit = []; // target - unix time of last hit 
    private readonly Dictionary<int, List<int>> _hitters = []; // target - list of hitters

    public AllyBurstedEvent() //todo - spiked w/ damage by enemy
    {
        Name = "Bursted By Enemies";
        Id = "AllyBurstedEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (Enumerable.Contains(pp.GetTargetIds(), (uint) pp.SourceId))
            {
                //if this action somehow targets the caster - ignore
                return false;
            }

            //PluginServices.PluginLog.Verbose($"{_lastHit}, {_hitters.Count}");
            foreach (int target in pp.GetTargetIds())
            {
                if (PluginServices.DutyManager.IsMonitoredUser(target))
                {
                    _lastHit.TryAdd(target, 0);
                    _hitters.TryAdd(target, []);
                    var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    if (unixTime - _lastHit[target] > 3)
                    {
                        _hitters[target].Clear();
                    }

                    if (_hitters[target].Contains(pp.SourceId))
                    {
                        // another check to ensure we arent tracking self-target stuff
                        continue;
                    }

                    if (!ActionIds.IsPvPBurst(pp.ActionId) && !pp.CritsOrDirectHits() && !pp.IsLimitBreak())
                    {
                        continue;
                    }

                    if (_hitters[target].Count > 3)
                    {
                        _hitters[target].Clear();
                        return true;
                    }

                    _hitters[target].Add(pp.SourceId);
                    _lastHit[target] = unixTime;
                    return false;
                }
            }
        }

        return false;
    }
}