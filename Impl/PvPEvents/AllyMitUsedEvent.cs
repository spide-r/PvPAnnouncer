using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyMitUsedEvent(ulong playerId, uint actionId): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [IroncladDefense, WhatAClash, BattleElectrifying, ThrillingBattle];
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; }
    public uint ActionId { get; init; } = actionId;
}
