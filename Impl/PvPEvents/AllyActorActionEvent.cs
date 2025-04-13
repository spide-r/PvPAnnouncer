using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyActionEvent(ulong playerId, ulong playerTarget, uint actionId, string[] soundPaths): IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = soundPaths;
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = playerTarget;
    public uint ActionId { get; init; } = actionId;
}
