using System;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPEventBroker
{
    void IngestMessage(IMessage message);
    void RegisterListener(PvPEvent e);
}