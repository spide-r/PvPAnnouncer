using System;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyMissedCc: IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [BeautifullyDodged, SawThroughIt, EffortlesslyDodged, ClearlyAnticipated, StylishEvasion, AvoidedWithEase, DodgedEverything, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; } = packet =>
    {
        if (packet is ActorControlPacket)
        {
            ActionEffectPacket pp = (ActionEffectPacket)packet;
            foreach (var target in pp.GetTargetIds())
            {
                if (PvPAnnouncerPlugin.PvPMatchManager.IsMonitoredUser(target))
                {
                    if (pp.GetEffectTypes(pp.GetTargetIds()).Contains(ActionEffectType.NoEffectText)) //todo this is nasty double looping and No Effect might not be the same as a miss - need to check
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    };
    
}
