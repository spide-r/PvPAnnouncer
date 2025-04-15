using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyDeathEvent : PvPActorControlEvent
{
    public override List<string> SoundPaths()
    {
        return [TheyreDownIsItOver, ChallengerDownIsThisEnd, TooMuch, WentDownHard, CouldntAvoid];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IPacket packet)
    {
        if (packet is ActorControlMessage)
        {
            if (PluginServices.PvPMatchManager.IsMonitoredUser(((ActorControlMessage) packet).EntityId))
            {
                return ((ActorControlMessage) packet).GetCategory() == ActorControlCategory.Death;

            }
        }

        return false;   
    }


    public override ActorControlCategory ActorControlCategory { get; init; } = ActorControlCategory.Death;
}
