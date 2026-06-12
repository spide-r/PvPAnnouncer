using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class AppliedStatusMessage(int status) : IMessage
{
    public int status = status;
}