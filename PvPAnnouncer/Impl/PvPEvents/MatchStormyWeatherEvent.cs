using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class MatchStormyWeatherEvent: PvPEvent
{
    public MatchStormyWeatherEvent()
    {
        Name = "Stormy Weather";
        InternalName = "MatchStormyWeatherEvent";
    }


    public override bool InvokeRule(IMessage message)
    {
        return message is MatchWeatherMessage {WeatherId: 7 or 8 or 9 or 10};
    }
}