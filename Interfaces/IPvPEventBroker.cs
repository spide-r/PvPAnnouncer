using System;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPEventBroker
{
    void ReceivePacket(IPacket packet);
    void RegisterListener(IPvPEvent e);
}