using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Lumina.Excel.Sheets;
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

    private string ReadFile(string jsonFile)
    {
        var assembly = Assembly.GetExecutingAssembly();
    
        // Format: "YourProjectNamespace.json.event.json"
        // Replace 'YourProjectNamespace' with your actual root namespace
        string resourceName = $"PvPAnnouncer.json." + jsonFile;

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null) return null;
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
    public void LoadShoutcasts()
    {
        var shoutJ = ReadFile("shoutcast.json");
        var r = JsonNode.Parse(shoutJ);
        if (r is JsonArray j)
        {
            foreach (var shout in j)
            { 
                var sh = shoutcastRepository.ConstructShoutcast(shout?.ToString() ?? "");
                PluginServices.PluginLog.Verbose($"Constructed {sh.Id} with {sh.SoundPath}");
                shoutcastRepository.SetShoutcast(sh.Id, sh);
            }
        }
    }

    public void LoadAndMapCustomEvents()
    {
        var eventsJ = ReadFile("event.json");
        var r = JsonNode.Parse(eventsJ);
        if (r is JsonArray j)
        {
            foreach (var customEvent in j)
            { 
                if (customEvent != null)
                {
                    var name = customEvent["name"]?.GetValue<string>();
                    var id = customEvent["id"]?.GetValue<string>();
                    var eventType = customEvent["eventType"]?.GetValue<string>();
                    var actionIds = customEvent["actionIds"]?.AsArray().Select(x => (uint)x).ToArray();
                    var shouts = customEvent["shouts"]?.Deserialize<List<string>>() ?? [];
                    if (id == null)
                    {
                        continue;
                    }
                    MapEvent(id, shouts);
                    switch (eventType)
                    {
                        case "AllyActionEvent":
                        {
                            var newAllyActionEvent = new AllyActionEvent(actionIds ?? [], name ?? "Action", id ?? "UnknownEvent");
                            pvPEventBroker.RegisterListener(newAllyActionEvent);
                            break;
                        }
                        case "EnemyActionEvent":
                        {
                            var newEnemyActionEvent = new EnemyActionEvent(actionIds ?? [], name ?? "Action", id ?? "UnknownEvent");
                            pvPEventBroker.RegisterListener(newEnemyActionEvent);
                            break;
                        }
                    }
                }
                //PluginServices.PluginLog.Verbose($"Constructed {sh.Id} with {sh.SoundPath} and attr {sh.Attributes}: {customEvent}");
            }
        }
    }

    public void MapEvent(string id, List<string> shouts)
    {
        List<string> newShoutList = [];
        foreach (var shout in shouts)
        {
            if (shout.Equals("LIMIT_BREAK_LIST"))
            {
                newShoutList.AddRange(InternalConstants.LimitBreakListStr);
            }
            else
            {
                PluginServices.PluginLog.Verbose($"Shout {shout} for {id} added.");
                newShoutList.Add(shout);
            }
        }
                    
        eventShoutcastMapping.AddShoutcasts(id, newShoutList);
    }

    public void LoadMapping()
    {
        var mappingJ = ReadFile("mapping.json");
        var r = JsonNode.Parse(mappingJ);
        if (r is JsonArray j)
        {
            foreach (var mapping in j)
            {
                var id = mapping?["eventId"]?.GetValue<string>();
                var shouts = mapping?["shouts"]?.Deserialize<List<string>>();
                if (id == null || shouts == null)
                {
                    continue;
                }
                MapEvent(id, shouts);
            }
        }

    }
}