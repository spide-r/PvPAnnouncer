using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;


public class AllyHitEnemyEvent(ulong playerId, ulong playerTarget, uint actionId): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [StruckSquare, WhatAClash, BattleElectrifying, ThrillingBattle];
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = playerTarget;
    public uint ActionId { get; init; } = actionId;
}
