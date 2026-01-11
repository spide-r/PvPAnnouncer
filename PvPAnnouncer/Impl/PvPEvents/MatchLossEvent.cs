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

    public override List<BattleTalk> SoundPaths()
    {
        return [MjPainfulToWatch, MjCommendableEffort, MjCompetitionTooMuch, MjUtterlyHumiliated];
    }

    public override Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<BattleTalk>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchLossMessage;
    }
}