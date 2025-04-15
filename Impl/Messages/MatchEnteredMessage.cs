using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class MatchEnteredMessage(uint territoryId): IPacket
{
    public uint TerritoryId = territoryId;
}