using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MechSpawnEvent: IPvPEvent
{
    public string[]? SoundPaths { get; init; } = [ColossalThing];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
}
