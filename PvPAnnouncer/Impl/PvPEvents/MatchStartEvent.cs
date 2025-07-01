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

    public override Dictionary<uint, List<string>> PersonalizedSoundPaths()
    {
        //todo: match start for all the other arcadion competitors
        return new Dictionary<uint, List<string>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchStartedMessage;
    }

}
