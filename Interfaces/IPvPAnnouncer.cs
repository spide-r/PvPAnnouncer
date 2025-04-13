using System.Collections;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPAnnouncer
{
    void ReceivePvPEvent(IPvPEvent pvpEvent);
    
    void PlaySound(IPvPEvent pvpEvent);
}
