using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitByLimitBreakEvent : PvPActionEvent
{
    public AllyHitByLimitBreakEvent()
    {
        Name = "Hit by an Enemy Limit Break";
        InternalName = "AllyHitHardEvent"; //todo migrate
    }

    public override List<string> SoundPaths()
    {
        return [ViciousBlow, FeltThatOneStillStanding, StruckSquare, Oof, MustHaveHurtNotOut, 
            CouldntAvoid, BattleElectrifying, BrutalBlow, StillInIt, OofMustHaveHurt, NotFastEnough, 
            CantBeCareless, DirectHitStillStanding, HoldingTheirOwn, NeitherSideHoldingBack, MjDontStandAChance,
            MjStillInItGentle, MjStillStandingGentle];
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
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                // we dont want self attacks triggering this todo: code duplication
                return false;
            }
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    return ActionIds.IsLimitBreakAttack(pp.ActionId); //todo mark this in the patch notes 
                }
            }
        }
        return false;
    }
}
