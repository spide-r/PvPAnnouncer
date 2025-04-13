using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MechSpawnEvent: IPvPEvent
{
    public string[]? SoundPaths { get; init; } = [ColossalThing];
}
