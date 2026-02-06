using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using PvPAnnouncer.Data;
using PvPAnnouncer.impl.PvPEvents;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class JsonFileLoader(IPvPEventBroker pvPEventBroker, IShoutcastRepository shoutcastRepository, IEventShoutcastMapping eventShoutcastMapping) : IJsonFileLoader
{
    public void LoadAll()
    {
        LoadAndMapCustomEvents();
        LoadShoutcasts();
        LoadMapping();
    }
    public void LoadShoutcasts()
    {
        var shoutJ = File.ReadAllText("shoutcast.json");
        var r = JsonNode.Parse(shoutJ);
        if (r is JsonArray j)
        {
            foreach (var shout in j)
            { 
                PluginServices.PluginLog.Verbose($"{shout}");
                var sh = shoutcastRepository.ConstructShoutcast(shout?.ToString() ?? "");
                PluginServices.PluginLog.Verbose($"Constructed {sh.Id} with {sh.SoundPath} and attr {sh.Attributes}: {shout}");
            }
        }
    }

    public void LoadAndMapCustomEvents()
    {
        var eventsJ = File.ReadAllText("event.json");
        var r = JsonNode.Parse(eventsJ);
        if (r is JsonArray j)
        {
            foreach (var customEvent in j)
            { 
                PluginServices.PluginLog.Verbose($"{customEvent}");

                if (customEvent != null)
                {
                    var name = customEvent["name"]?.GetValue<string>();
                    var id = customEvent["id"]?.GetValue<string>();
                    var eventType = customEvent["eventType"]?.GetValue<string>();
                    var actionIds = customEvent["actionIds"]?.GetValue<uint[]>();
                    var shouts = customEvent["shouts"]?.GetValue<List<string>>();
                    List<string> newShoutList = [];
                    foreach (var shout in (shouts ?? []).ToList())
                    {
                        if (shout.Equals("{{LIMIT_BREAK_LIST}}"))
                        {
                            newShoutList.AddRange(InternalConstants.LimitBreakListStr);
                        }
                        else
                        {
                            newShoutList.Add(shout);
                        }
                    }
                    
                    eventShoutcastMapping.AddShoutcasts(id ?? "UnknownEvent", newShoutList);
                    switch (eventType)
                    {
                        case "AllyActionEvent":
                        {
                            var newAllyActionEvent = new AllyActionEvent(actionIds ?? [], name ?? "Action", id ?? "UnknownEvent");
                            PluginServices.PluginLog.Verbose($"Constructed Ally Action: {newAllyActionEvent.Name} - {newAllyActionEvent.InternalName} - With id's: {actionIds}");
                            pvPEventBroker.RegisterListener(newAllyActionEvent);
                            break;
                        }
                        case "EnemyActionEvent":
                        {
                            var newEnemyActionEvent = new EnemyActionEvent(actionIds ?? [], name ?? "Action", id ?? "UnknownEvent");
                            PluginServices.PluginLog.Verbose($"Constructed Enemy Action: {newEnemyActionEvent.Name} - {newEnemyActionEvent.InternalName} - With id's: {actionIds}");
                            pvPEventBroker.RegisterListener(newEnemyActionEvent);
                            break;
                        }
                    }
                }
                //PluginServices.PluginLog.Verbose($"Constructed {sh.Id} with {sh.SoundPath} and attr {sh.Attributes}: {customEvent}");
            }
        }
    }

    public void LoadMapping()
    {
        var mappingJ = File.ReadAllText("mapping.json");
        var r = JsonNode.Parse(mappingJ);
        if (r is JsonArray j)
        {
            foreach (var mapping in j)
            {
                var eventName = mapping?["event"]?.GetValue<string>();
                var shouts = mapping?["shouts"]?.GetValue<List<string>>();
                eventShoutcastMapping.AddShoutcasts(eventName ?? "Unknown Event", shouts ?? []);
                PluginServices.PluginLog.Verbose($"Constructed Shoutcast: {eventName} with {shouts}");
            }
        }

    }
}