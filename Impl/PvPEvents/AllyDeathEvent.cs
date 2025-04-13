using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyDeathEvent(ulong playerId) : IPvPActorControlEvent
{
    public ulong PlayerId { get; init; } = playerId;
    public ulong? PlayerTarget { get; init; } = null; 
    public string[]? SoundPaths { get; init; } = [TheyreDownIsItOver, TheyreDownIsThisEnd, TooMuch, WentDownHard, CouldntAvoid];
    public ActorControlCategory ActorControlCategory { get; init; } = ActorControlCategory.Death;
    //todo special implementation for PvPDeathEvent - need to get the actual source of whoever killed a person - I dont think I can just work off of the actor control packet 
}
