using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyZoneOutEvent: IPvPActorEvent
{
    public string[]? SoundPaths { get; init; } = [Fallen, TheyreDownIsItOver, TheyreDownIsThisEnd, TooMuch];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
}
