using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent (ulong playerId): IPvPActorEvent
{
    public string[]? SoundPaths { get; init; } = [BackUpGrit, BackOnFeet, RisesAgain];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = null;
}
