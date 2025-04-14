using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent: IPvPActorEvent
{
    public string[]? SoundPaths { get; init; } = [BackUpGrit, BackOnFeet, RisesAgain];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
}
