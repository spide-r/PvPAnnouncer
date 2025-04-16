using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class PvPEventBroker: IPvPEventBroker
{
    private readonly List<Tuple<PvPEvent, Func<IMessage, bool>>> _registeredListeners = new();
    public void IngestPacket(IMessage message)
    {
        if (PluginServices.Config.Disabled)
        {
            return;
        }

        if (message is ActionEffectMessage aaa)
        {
            string s = "S:" + aaa.SourceId + " SN: " + aaa.GetSource() + "|A: " + aaa.ActionId + "|AN: " + aaa.GetAction()?.Name.ToString();
            bool shouldEmitttt = PluginServices.PvPMatchManager.IsMonitoredUser(aaa.SourceId);
            if (shouldEmitttt)
            {
                PluginServices.PluginLog.Verbose(s);
 
            }
            //PluginServices.PluginLog.Verbose(shouldEmitttt.ToString());
        }
        
        //PluginServices.PluginLog.Logger.Debug("Injesting packet " + message.GetType().Name);
        //PluginServices.PluginLog.Debug("asdasd" + _registeredListeners.Count);
        foreach (var keyValuePair in _registeredListeners)
        {
            PvPEvent ee = keyValuePair.Item1;
            if (IsBlacklistedEvent(ee))
            {
                continue;
            }
            Func<IMessage, bool> shouldEmit = keyValuePair.Item2;
            bool emit = shouldEmit.Invoke(message);
            if (emit)
            {
                PluginServices.PluginLog.Debug("Emitted: " + ee.Name);

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
        PluginServices.PluginLog.Debug("Registered listener: " + e.Name);
        _registeredListeners.Add(new Tuple<PvPEvent, Func<IMessage, bool>>(e, e.InvokeRule));
    }

    public void DeregisterListener(PvPEvent e)
    {
        _registeredListeners.RemoveAll(aa =>
        {
            return aa.Item1.Name.Equals(e.Name);
        });
    }
}