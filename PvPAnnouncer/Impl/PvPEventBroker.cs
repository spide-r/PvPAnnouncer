using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class PvPEventBroker: IPvPEventBroker
{
    private readonly List<Tuple<PvPEvent, Func<IMessage, bool>>> _registeredListeners = new();
    public void IngestMessage(IMessage message)
    {
        if (PluginServices.Config.Disabled)
        {
            return;
        }

        if (message is ActionEffectMessage aaa)
        {
            string s = "S:" + aaa.SourceId + " SN: " + aaa.GetSource() + "|A: " + aaa.ActionId + "|AN: " + aaa.GetAction()?.Name.ToString();
            bool shouldEmit = PluginServices.PvPMatchManager.IsMonitoredUser(aaa.SourceId);
            if (shouldEmit)
            {
                PluginServices.PluginLog.Verbose(s);
 
            }
        } else if (message is ActorControlMessage ac) {
            bool shouldEmit = PluginServices.PvPMatchManager.IsMonitoredUser(ac.EntityId);
            if (shouldEmit)
            {
                if (ac.GetCategory() != ActorControlCategory.DirectorUpdate)
                {
                    PluginServices.PluginLog.Verbose($"Actor control: {ac.EntityId}, " +
                                                     $"type: {(ActorControlCategory)ac.GetCategory()} source: {ac.Source}, statusId: {ac.StatusId}" +
                                                     $" amount: {ac.Amount} a5: {ac.A5}, a7: {ac.A7} a8: {ac.A8} a9: {ac.A9} flag: {ac.Flag}"); 

                }
                
            }
        }
        
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
                PluginServices.PluginLog.Verbose("Emitted: " + ee.Name);

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