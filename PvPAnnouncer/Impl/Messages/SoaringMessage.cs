using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class SoaringMessage(int amount): IMessage
{
    public int Amount = amount;
}