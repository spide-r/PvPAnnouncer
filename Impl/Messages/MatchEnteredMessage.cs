using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class MatchEnteredMessage(uint territoryId): IMessage
{
    public uint TerritoryId = territoryId;
}