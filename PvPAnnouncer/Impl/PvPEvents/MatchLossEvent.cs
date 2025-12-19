using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchLossEvent: PvPMatchEvent
{
    public MatchLossEvent()
    {
        Name = "Match Loss";
        InternalName = "MatchLossEvent";
    }

    public override List<string> SoundPaths()
    {
        return [MjPainfulToWatch, MjCommendableEffort, MjCompetitionTooMuch, MjUtterlyHumiliated];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchLossMessage;
    }
}