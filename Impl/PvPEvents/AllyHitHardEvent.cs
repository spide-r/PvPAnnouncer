using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitHardEvent(ulong playerId, uint actionId): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [ViciousBlow, FeltThatOneStillStanding, StruckSquare, Oof, MustHaveHurtNotOut, CouldntAvoid, WhatAClash, BattleElectrifying, ThrillingBattle, BrutalBlow, StillInIt];
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = null;
    public uint ActionId { get; init; } = actionId;
}
