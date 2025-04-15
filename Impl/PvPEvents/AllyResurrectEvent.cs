using System;
using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent: PvPActorEvent
{
    public override List<string> SoundPaths()
    {
        return [BackUpGrit, BackOnFeet, RisesAgain];
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
        if (packet is UserResurrectedMessage)
        {
            UserResurrectedMessage p = (UserResurrectedMessage)packet;
            if (PluginServices.PvPMatchManager.IsMonitoredUser(p.UserId))
            {
                return true;
            }
        }
        return false;
    }

}
