namespace PvPAnnouncer.Interfaces;

public interface IActorControl: IMessage
{
    uint EntityId {get; set;}
    uint Type {get; set;}
    uint StatusId {get; set;}
    uint Amount {get; set;}
    uint A5  {get; set;}
    uint Source {get; set;}
    uint A7  {get; set;}
    uint A8  {get; set;}
    ulong A9 {get; set;}
    byte Flag {get; set;}
}