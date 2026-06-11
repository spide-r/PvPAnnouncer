using System;

namespace PvPAnnouncer.Interfaces;

public interface IEventPublisher : IDisposable
{
    void EmitToBroker(IMessage pvpEvent);
}