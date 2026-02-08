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
    private readonly List<Tuple<PvPEvent, Func<IMessage, bool>>> _registeredListeners = new();
    private readonly Dictionary<string, PvPEvent> _pvpEventIdBinding = new(); //todo bodge? maybe make the broker provide the list as well
    private string LastUsedAction = "";
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
                LastUsedAction = aaa.ActionId.ToString();

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
                PluginServices.PluginLog.Verbose("Emitted: " + ee.Id);

                EmitToSubscribers(ee);
            }
        }
    }

    private bool IsBlacklistedEvent(PvPEvent ee)
    {
        bool eventIsBlacklisted = PluginServices.Config.BlacklistedEvents.Contains(ee.Id);
        return eventIsBlacklisted;
    }


    private void EmitToSubscribers(PvPEvent ee)
    {
        PluginServices.Announcer.ReceivePvPEvent(ee);

    }

    public void RegisterListener(PvPEvent e)
    {
        _pvpEventIdBinding.Add(e.Id, e);
        PluginServices.PluginLog.Verbose("Registered listener: " + e.Id);
        _registeredListeners.Add(new Tuple<PvPEvent, Func<IMessage, bool>>(e, e.InvokeRule));
    }

    public string GetLastAction()
    {
        return LastUsedAction;
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
        _registeredListeners.RemoveAll(aa => aa.Item1.Id.Equals(e.Id));
        _pvpEventIdBinding.Remove(e.Id);
    }
}