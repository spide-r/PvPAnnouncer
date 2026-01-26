using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitByLimitBreakEvent : PvPActionEvent
{
    public AllyHitByLimitBreakEvent()
    {
        Name = "Hit by an Enemy Limit Break";
        InternalName = "AllyHitByLimitBreakEvent"; 
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [ViciousBlow, FeltThatOneStillStanding, StruckSquare, Oof, MustHaveHurtNotOut, 
            CouldntAvoid, BattleElectrifying, BrutalBlow, StillInIt, OofMustHaveHurt, NotFastEnough, 
            CantBeCareless, DirectHitStillStanding, HoldingTheirOwn, NeitherSideHoldingBack, MjDontStandAChance,
            MjStillInItGentle, MjStillStandingGentle, HardPressed, SomethingsComing, FeverPitch, BeatenToThePunch, 
            SevenHells, ContendWithThreat, SlowAndSteadyThancred, ThisBodethIll, LeaveAMark, BeStrong, TheyllPayForThatEstinien, HangInThere,
            NotGoingDownWithoutFight, TestUsButPrevail, NoDownWithoutFight, BBDesprate,
            RunBeastRun, HaveYouTheStrength, DanceForMe, VauntedFortitude, ThatWasUnbecoming];
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                return false;
            }
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    return ActionIds.IsLimitBreakAttack(pp.ActionId);
                }
            }
        }
        return false;
    }
}
