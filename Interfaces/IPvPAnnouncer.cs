using System.Collections;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPAnnouncer
{
    Hashtable AudioFiles { get; init; } //string of the path key and Scd.Audio data value
    void ReceivePvPEvent(IPvPEvent pvpEvent);
    
    void PlaySound(IPvPEvent pvpEvent);
}
