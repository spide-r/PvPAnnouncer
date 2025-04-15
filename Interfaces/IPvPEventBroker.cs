using System;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPEventBroker
{
    void IngestPacket(IMessage message);
    void RegisterListener(PvPEvent e);
}