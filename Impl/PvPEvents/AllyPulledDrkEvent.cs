using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyPulledDrkEvent(ulong playerId, ulong playerTarget): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [SuckedIn, WhatAClash, BattleElectrifying, ThrillingBattle];
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = playerTarget;
    public uint ActionId { get; init; } //todo drk pull action id
}
