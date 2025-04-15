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
    private readonly Dictionary<PvPEvent, Func<IMessage, bool>> _registeredListeners = new();
    public void IngestPacket(IMessage message)
    {
        if (PluginServices.Config.Disabled)
        {
            return;
        }
        
        foreach (var keyValuePair in _registeredListeners.AsEnumerable())
        {
            PvPEvent ee = keyValuePair.Key;
            if (IsBlacklistedEvent(ee))
            {
                continue;
            }
            Func<IMessage, bool> shouldEmit = keyValuePair.Value;
            bool emit = shouldEmit.Invoke(message);
            if (emit)
            {
                EmitToSubscribers(ee);
            }
        }
    }

    private bool IsBlacklistedEvent(PvPEvent ee)
    {
        bool eventIsBlacklisted = PluginServices.Config.BlacklistedEvents.Contains(ee.Name);
        return eventIsBlacklisted;
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