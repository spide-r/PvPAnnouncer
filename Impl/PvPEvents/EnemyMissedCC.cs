using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyMissedCC(ulong playerId, ulong playerTarget, uint actionId): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [BeautifullyDodged, SawThroughIt, EffortlesslyDodged, ClearlyAnticipated, StylishEvasion, AvoidedWithEase, DodgedEverything, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = playerTarget;
    public uint ActionId { get; init; } = actionId;
}
