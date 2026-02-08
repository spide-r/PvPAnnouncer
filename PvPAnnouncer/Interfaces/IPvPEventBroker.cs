using System;
using System.Collections.Generic;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPEventBroker
{
    void IngestMessage(IMessage message);
    void RegisterListener(PvPEvent e);
    string GetLastAction();
    PvPEvent? GetEvent(string eventId);
    List<PvPEvent> GetPvPEvents();
    List<string> GetPvPEventIDs();
}