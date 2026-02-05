using System.Collections;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IAnnouncer
{
    void ReceivePvPEvent(PvPEvent pvpEvent);
    void ReceivePvPEvent(bool bypass, PvPEvent pvpEvent);
    void PlaySound(string sound);
    void SendBattleTalk(Shoutcast shoutcast);
    void ClearQueue();
}
