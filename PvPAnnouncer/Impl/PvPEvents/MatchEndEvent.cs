using System;
using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchEndEvent: PvPMatchEvent
{
    public MatchEndEvent()
    {
        Name = "Matches Ending";
    }

    public override List<string> SoundPaths()
    {
        return [AllOverUntilNextTime];
    }

    public override Dictionary<uint, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<uint, List<string>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchEndMessage;
    }
}
