namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPEvent(string name = "PvP Event")
{
    public abstract bool InvokeRule(IMessage message); //todo maybe make this static since we no longer need an instanced pvp event?
    public string Name = name;
    public string InternalName = "InternalName";
}
