namespace PvPAnnouncer.Interfaces;

public interface IActorControl: IMessage
{
    uint EntityId {get; set;}
    uint Type {get; set;}
}