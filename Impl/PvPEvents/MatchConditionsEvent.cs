using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchConditionsEvent(string[] soundPaths): IPvPEvent
{
    // Weather, Match Obstacles, Conditions Changing
    public string[]? SoundPaths { get; init; } = soundPaths;
}
