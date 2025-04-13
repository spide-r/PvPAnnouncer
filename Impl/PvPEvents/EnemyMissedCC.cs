using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyMissedCC(ulong playerId, ulong playerTarget, uint actionId): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [BeautifullyDodged, SawThroughIt, EffortlesslyDodged, ClearlyAnticipated, StylishEvasion, AvoidedWithEase, DodgedEverything, WhatAClash, BattleElectrifying, ThrillingBattle];
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = playerTarget;
    public uint ActionId { get; init; } = actionId;
}
