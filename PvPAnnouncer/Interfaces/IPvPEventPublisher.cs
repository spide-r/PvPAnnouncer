using System;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IPvPEventPublisher: IDisposable
{
    void EmitToBroker(IMessage pvpEvent);
}
