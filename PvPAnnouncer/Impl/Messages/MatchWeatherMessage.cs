using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class MatchWeatherMessage(uint weatherId) : IMessage
{
    public uint WeatherId { get; } = weatherId;
}