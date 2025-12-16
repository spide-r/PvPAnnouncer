using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyDeathEvent : PvPActorControlEvent
{
    public AllyDeathEvent()
    {
        Name = "Deaths";
        InternalName = "AllyDeathEvent";
    }
    public override List<string> SoundPaths()
    {
        return [TheyreDownIsItOver, ChallengerDownIsThisEnd, TooMuch, WentDownHard, CouldntAvoid];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>
        {
            {Personalization.Spoilers, [BeenFelled, TheyreDownCanTheyRecover]}
        };
    }

    public override bool InvokeRule(IMessage m)
    {
        if (m is ActorControlMessage message)
        {
            if (PluginServices.PvPMatchManager.IsMonitoredUser(message.EntityId))
            {
                return message.GetCategory() == ActorControlCategory.Death;

            }
        }
        return m is UserDiedMessage;
    }


    public override ActorControlCategory ActorControlCategory { get; init; } = ActorControlCategory.Death;
}
