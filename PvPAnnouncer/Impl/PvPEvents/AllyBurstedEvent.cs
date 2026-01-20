using System;
using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;

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

    public override List<BattleTalk> SoundPaths()
    {
        return [ViciousBlow, FeltThatOneStillStanding, StruckSquare, Oof, MustHaveHurtNotOut, 
            CouldntAvoid, BattleElectrifying, BrutalBlow, StillInIt, OofMustHaveHurt, NotFastEnough, 
            CantBeCareless, DirectHitStillStanding, HoldingTheirOwn, NeitherSideHoldingBack, MjDontStandAChance,
            MjStillInItGentle, MjStillStandingGentle, NowhereLeft, RainOfDeath, SuchScorn, HardPressed, StayStrong, 
            FeverPitch, BBDesprate, TreadWarily, ThaliakProtectUs, DoNotLoseHeart, PainfulThrashing, ScramblingToKeepUp, SevenHells,
            AmidstGreatChaos, DangerousMomentIndeed, MakeReady, BeVigilant, LeaveAMark, TheyHaveUpperHand, WatchYourself, 
            ThereIsNoEndToThem, HangInThere, Courage, StayStrongEstinien, FightHardNotHardEnough, StandFast, WeveComeTooFar, 
            FindCoverEnergy, DontLoseNerveWuk, GettingInterestingWuk, StillWinWuk, StayAlert, GodsHelpUs, CarefulPlanning
        ];
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (Enumerable.Contains(pp.GetTargetIds(), (uint) pp.SourceId))
            {
                return false;
            }
            
            //PluginServices.PluginLog.Verbose($"{_lastHit}, {_hitters.Count}");
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

                    if (!ActionIds.IsBurst(pp.ActionId) && !pp.CritsOrDirectHits())
                    {
                        return false;
                    }

                    if (_hitters.Count > 3)
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