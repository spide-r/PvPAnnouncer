using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPEventPublisher
{
    void EmitToBroker(IMessage pvpEvent);
    
}
