using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class UserResurrectedMessage(uint userId): IMessage
{
    //todo: determine if there is a better way to determine ressurection status 
    public readonly uint UserId = userId;
}