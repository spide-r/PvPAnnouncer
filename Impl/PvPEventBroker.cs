using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class PvPEventBroker: IPvPEventBroker
{
    private readonly Dictionary<IPvPEvent, Func<IPacket, bool>> _registeredListeners = new();
    public void ReceivePacket(IPacket packet)
    {
        foreach (var keyValuePair in _registeredListeners.AsEnumerable())
        {
            IPvPEvent ee = keyValuePair.Key;
            Func<IPacket, bool> shouldEmit = keyValuePair.Value;
            bool emit = shouldEmit.Invoke(packet);
            if (emit)
            {
                PvPAnnouncerPlugin.PvPAnnouncer?.ReceivePvPEvent(ee);
            }
        }
    }

    public void RegisterListener(IPvPEvent e)
    {
        _registeredListeners.Add(e, e.InvokeRule);
    }

    public void DeregisterListener(IPvPEvent e)
    {
        _registeredListeners.Remove(e);
    }
}