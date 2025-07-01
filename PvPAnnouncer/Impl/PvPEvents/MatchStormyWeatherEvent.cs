using System.Collections.Generic;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchStormyWeatherEvent: PvPEvent
{
    public MatchStormyWeatherEvent()
    {
        Name = "Stormy Weather";
    }


    public override List<string> SoundPaths()
    {
        return [StormParasols, Thunderstorm];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchWeatherMessage {WeatherId: 7 or 8 or 9 or 10};
    }
}