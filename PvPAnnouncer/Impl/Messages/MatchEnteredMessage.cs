using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class MatchEnteredMessage(ushort territoryId): IMessage
{
    public ushort TerritoryId = territoryId;
}