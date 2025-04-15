using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class UserZoneOutMessage(uint userId): IPacket
{
    //todo: determine how fall damage death is logged
    public uint UserId = userId;
}