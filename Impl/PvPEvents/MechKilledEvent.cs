using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MechKilledEvent: IPvPEvent
{
    public string[]? SoundPaths { get; init; } = [ColossalThingSwordMasc];
}
