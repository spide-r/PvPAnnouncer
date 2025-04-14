using System;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyDeathEvent() : IPvPActorControlEvent
{ 
    public string[]? SoundPaths { get; init; } = [TheyreDownIsItOver, TheyreDownIsThisEnd, TooMuch, WentDownHard, CouldntAvoid];

    public Func<IPacket, bool> InvokeRule { get; init; } = packet =>
    {
        if (packet is ActorControlPacket)
        {
            return ((ActorControlPacket) packet).GetCategory() == ActorControlCategory.Death;
        }

        return false;
    };
    

    public ActorControlCategory ActorControlCategory { get; init; } = ActorControlCategory.Death;
    //todo special implementation for PvPDeathEvent - need to get the actual source of whoever killed a person - I dont think I can just work off of the actor control packet 
}
