using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class ActorControlMessage(
    uint entityId,
    uint type)
    : IActorControl
{
    public uint EntityId { get; set; } = entityId;
    public uint Type { get; set; } = type;

    public ActorControlCategory GetCategory()
    {
        return (ActorControlCategory)Type;
    }
    
}