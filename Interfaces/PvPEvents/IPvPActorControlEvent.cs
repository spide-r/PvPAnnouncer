using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public interface IPvPActorControlEvent: IPvPActorEvent
{
    ActorControlCategory ActorControlCategory { get; init; }

}
