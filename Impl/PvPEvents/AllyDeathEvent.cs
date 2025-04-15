using System;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyDeathEvent : IPvPActorControlEvent
{ 
    public string[]? SoundPaths { get; init; } = [TheyreDownIsItOver, TheyreDownIsThisEnd, TooMuch, WentDownHard, CouldntAvoid];

    public Func<IPacket, bool> InvokeRule { get; init; } = packet =>
    {
        if (packet is ActorControlPacket)
        {
            if (PvPAnnouncerPlugin.PvPMatchManager!.IsMonitoredUser(((ActorControlPacket) packet).EntityId))
            {
                return ((ActorControlPacket) packet).GetCategory() == ActorControlCategory.Death;

            }
        }

        return false;
    };
    
    public ActorControlCategory ActorControlCategory { get; init; } = ActorControlCategory.Death;
}
