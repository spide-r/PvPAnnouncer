using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitHardEvent : PvPActionEvent
{
    public AllyHitHardEvent()
    {
        Name = "Hit hard by an Enemy";
        InternalName = "AllyHitHardEvent";
    }

    public override List<string> SoundPaths()
    {
        return [ViciousBlow, FeltThatOneStillStanding, StruckSquare, Oof, MustHaveHurtNotOut, CouldntAvoid, BattleElectrifying, BrutalBlow, StillInIt];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>{{Personalization.BruteBomber, [BBDesprate]}};
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
                    return ActionIds.IsLimitBreak(pp.ActionId) || ActionIds.IsBigHit(pp.ActionId);
                }
            }
        }
        return false;
    }
}
