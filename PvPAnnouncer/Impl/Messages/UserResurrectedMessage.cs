using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class UserResurrectedMessage(uint userId): IMessage
{
    public readonly uint UserId = userId;
}