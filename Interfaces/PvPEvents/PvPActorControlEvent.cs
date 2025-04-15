using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPActorControlEvent: PvPEvent
{
    public abstract ActorControlCategory ActorControlCategory { get; init; }
}