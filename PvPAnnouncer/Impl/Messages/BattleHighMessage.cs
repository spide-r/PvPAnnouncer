using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Messages;

public class BattleHighMessage(int level): IMessage
{
    public int Level = level;
}