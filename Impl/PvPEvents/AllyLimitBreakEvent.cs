using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyLimitBreakEvent(ulong playerId, uint actionId): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; }
    public uint ActionId { get; init; } = actionId;
}
