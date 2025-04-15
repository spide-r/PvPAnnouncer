using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitHardEvent : PvPActorActionEvent
{
    
    public override List<string> SoundPaths()
    {
        return [ViciousBlow, FeltThatOneStillStanding, StruckSquare, Oof, MustHaveHurtNotOut, CouldntAvoid, BattleElectrifying, ThrillingBattle, BrutalBlow, StillInIt];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IPacket packet)
    {
        if (packet is ActionEffectMessage)
        {
            ActionEffectMessage pp = (ActionEffectMessage)packet;
            foreach (var allianceMember in PluginServices.PvPMatchManager.FullParty)
            {
                if (pp.GetTargetIds().Contains(allianceMember))
                {
                    return pp.CritsOrDirectHits() || ActionIds.IsLimitBreak(pp.ActionId) || ActionIds.IsBigHit(pp.ActionId);
                }
            }
            
        }
        return false;
    }
}
