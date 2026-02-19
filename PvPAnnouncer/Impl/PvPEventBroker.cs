using System;
using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using PvPAnnouncer.Windows;

namespace PvPAnnouncer.Impl;

public class PvPEventBroker: IPvPEventBroker
{
    private readonly Dictionary<string, PvPEvent> _pvpEventIdBinding = new();
    private string _lastUsedAction = "";
    public void IngestMessage(IMessage message)
    {
        if (PluginServices.Config.Disabled)
        {
            return;
        }

        if (!PluginServices.PlayerStateTracker.IsPvP())
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
                _lastUsedAction = aaa.ActionId.ToString();

            }
        } else if (message is ActorControlMessage ac) {
            bool shouldEmit = PluginServices.PvPMatchManager.IsMonitoredUser(ac.EntityId);
            if (shouldEmit)
            {
                if (ac.GetCategory() != ActorControlCategory.DirectorUpdate)
                {
                    /*PluginServices.PluginLog.Verbose($"Actor control: {ac.EntityId}, " +
                                                     $"type: {ac.GetCategory()} source: {ac.Source}, statusId: {ac.StatusId}" +
                                                     $" amount: {ac.Amount} a5: {ac.A5}, a7: {ac.A7} a8: {ac.A8} a9: {ac.A9} flag: {ac.Flag}");*/ 

                }
                
            }
        }
        
        foreach (var pvpEvent in _pvpEventIdBinding.Values)
        {
            if (IsBlacklistedEvent(pvpEvent))
            {
                continue;
            }
            var emit = pvpEvent.InvokeRule(message);
            if (emit)
            {
                PluginServices.PluginLog.Verbose("Emitted: " + pvpEvent.Id);

                EmitToSubscribers(pvpEvent);
            }
        }
    }

    private bool IsBlacklistedEvent(PvPEvent ee)
    {
        var eventIsBlacklisted = PluginServices.Config.BlacklistedEvents.Contains(ee.Id);
        return eventIsBlacklisted;
    }


    private void EmitToSubscribers(PvPEvent ee)
    {
        PluginServices.Announcer.ReceivePvPEvent(ee);

    }

    public void RegisterListener(PvPEvent e)
    {
        _pvpEventIdBinding[e.Id] = e;
        PluginServices.PluginLog.Verbose("Registered listener: " + e.Id);
    }

    public string GetLastAction()
    {
        return _lastUsedAction;
    }

    public PvPEvent? GetEvent(string eventId)
    {
        var found = _pvpEventIdBinding.TryGetValue(eventId, out var obj);
        return !found ? null : obj;
    }

    public List<PvPEvent> GetPvPEvents()
    {
        return _pvpEventIdBinding.Values.ToList();
    }

    public List<string> GetPvPEventIDs()
    {
        return _pvpEventIdBinding.Keys.ToList();
    }

    public void DeregisterListener(PvPEvent e)
    {
        _pvpEventIdBinding.Remove(e.Id);
    }
}