using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPEventReader
{
    void ProcessActorControl();
    void ProcessAction();
    void ReadToast();
    void Emit(IPvPEvent pvpEvent);
    
}
