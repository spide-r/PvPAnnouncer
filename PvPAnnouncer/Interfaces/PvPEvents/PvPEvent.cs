namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPEvent(string name = "PvP Event")
{
    public abstract bool InvokeRule(IMessage message);
    public string Name = name;
    public string InternalName = "InternalName";
}
