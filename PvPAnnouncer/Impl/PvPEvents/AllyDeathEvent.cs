using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyDeathEvent : PvPActorControlEvent
{
    public AllyDeathEvent()
    {
        Name = "Deaths";
        InternalName = "AllyDeathEvent";
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
