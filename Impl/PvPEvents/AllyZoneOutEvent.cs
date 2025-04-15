using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyZoneOutEvent: PvPActorEvent
{
    public override List<string> SoundPaths()
    {
        return [Fallen, TheyreDownIsItOver, ChallengerDownIsThisEnd, TooMuch];
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
        if (packet is UserZoneOutMessage)
        {
            UserZoneOutMessage p = (UserZoneOutMessage)packet;
            if (PluginServices.PvPMatchManager.IsMonitoredUser(p.UserId))
            {
                return true;
            }
        }
        return false;
    }

}
