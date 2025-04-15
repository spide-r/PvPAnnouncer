using System.Collections;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IAnnouncer
{
    void ReceivePvPEvent(IPvPEvent pvpEvent);
    
    void PlaySound(IPvPEvent pvpEvent);
}
