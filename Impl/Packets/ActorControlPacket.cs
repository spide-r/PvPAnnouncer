using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Packets;

public class ActorControlPacket(
    uint entityId,
    uint type,
    uint statusId,
    uint amount,
    uint a5,
    uint source,
    uint a7,
    uint a8,
    ulong a9,
    byte flag)
    : IActorControl
{
    public uint EntityId { get; set; } = entityId;
    public uint Type { get; set; } = type;
    public uint StatusId { get; set; } = statusId;
    public uint Amount { get; set; } = amount;
    public uint A5 { get; set; } = a5;
    public uint Source { get; set; } = source;
    public uint A7 { get; set; } = a7;
    public uint A8 { get; set; } = a8;
    public ulong A9 { get; set; } = a9;
    public byte Flag { get; set; } = flag;

    public ActorControlCategory GetCategory()
    {
        return (ActorControlCategory)Type;
    }
    
}