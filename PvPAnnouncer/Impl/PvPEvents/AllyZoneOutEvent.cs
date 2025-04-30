using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyZoneOutEvent: PvPActorEvent
{
    public AllyZoneOutEvent()
    {
        Name = "Fall Damage";
    }

    public override List<string> SoundPaths()
    {
        return [Fallen];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IMessage m) 
    {
        if (m is UserZoneOutMessage)
        {
            return true;
        }
        return false;
    }

}
