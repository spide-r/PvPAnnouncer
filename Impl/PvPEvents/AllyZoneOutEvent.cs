using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyZoneOutEvent(ulong playerId): IPvPActorEvent
{
    public string[]? SoundPaths { get; init; } = [Fallen, TheyreDownIsItOver, TheyreDownIsThisEnd, TooMuch];
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; }
}
