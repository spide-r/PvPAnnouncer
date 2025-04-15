using System;
using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchStartEvent: PvPMatchEvent
{
    public MatchStartEvent()
    {
        Name = "Matches Started";
    }

    public override List<string> SoundPaths()
    {
        return [SendingCameras, UpstartBegins];
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
        if (packet is MatchEnteredMessage)
        {
            return true;
        }
        return false;
    }

}
