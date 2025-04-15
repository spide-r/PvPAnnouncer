using System;
using System.Dynamic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitHardEvent : IPvPActorActionEvent
{
    public AllyHitHardEvent()
    {
        InvokeRule = Invoke;
    }

    public string[]? SoundPaths { get; init; } = [ViciousBlow, FeltThatOneStillStanding, StruckSquare, Oof, MustHaveHurtNotOut, CouldntAvoid, BattleElectrifying, ThrillingBattle, BrutalBlow, StillInIt];
    public Func<IPacket, bool> InvokeRule { get; init; }

    public bool Invoke(IPacket packet)
    {
        if (packet is ActionEffectPacket)
        {
            ActionEffectPacket pp = (ActionEffectPacket)packet;
            foreach (var allianceMember in PvPAnnouncerPlugin.PvPMatchManager!.AllianceMembers)
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
