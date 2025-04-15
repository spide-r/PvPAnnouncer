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
    private readonly Dictionary<PvPEvent, Func<IPacket, bool>> _registeredListeners = new();
    public void ReceivePacket(IPacket packet)
    {
        foreach (var keyValuePair in _registeredListeners.AsEnumerable())
        {
            PvPEvent ee = keyValuePair.Key;
            Func<IPacket, bool> shouldEmit = keyValuePair.Value;
            bool emit = shouldEmit.Invoke(packet);
            if (emit)
            {
                EmitToSubscribers(ee);
            }
        }
    }


    private void EmitToSubscribers(PvPEvent ee)
    {
        PluginServices.Announcer.ReceivePvPEvent(ee);

    }

    public void RegisterListener(PvPEvent e)
    {
        _registeredListeners.Add(e, e.InvokeRule);
    }

    public void DeregisterListener(PvPEvent e)
    {
        _registeredListeners.Remove(e);
    }
}