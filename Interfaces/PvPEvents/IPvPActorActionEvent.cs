namespace PvPAnnouncer.Interfaces.PvPEvents;

public interface IPvPActorActionEvent: IPvPActorEvent
{
    uint ActionId { get; init; } 
}
