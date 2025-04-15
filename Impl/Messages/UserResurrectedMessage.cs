using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class UserResurrectedMessage(uint userId): IMessage
{
    //todo: determine how resurrection is logged/sent to the client
    public readonly uint UserId = userId;
}