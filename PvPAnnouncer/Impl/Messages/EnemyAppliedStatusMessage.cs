using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class EnemyAppliedStatusMessage(int status) : IMessage
{
    public int status = status;
}