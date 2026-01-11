using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchVictoryEvent: PvPMatchEvent
{
    public MatchVictoryEvent()
    {
        Name = "Match Victory";
        InternalName = "MatchVictoryEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [GenericVictory,  NewGcBornVictory, RobotKo, VictoryChamp, MjBeautifullyPlayed, 
            MjMadeYourMark, MjKissLadyLuck, MjClobberedWithTable];
    }

    public override Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<BattleTalk>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchVictoryMessage;
    }
}