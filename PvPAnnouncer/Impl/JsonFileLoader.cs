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
using PvPAnnouncer.Impl.PvPEvents;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class JsonFileLoader(IShoutcastBuilder builder, IStringRepository attributeRepository, IStringRepository casterRepository, IPvPEventBroker pvPEventBroker, IShoutcastRepository shoutcastRepository, IEventShoutcastMapping eventShoutcastMapping) : IJsonFileLoader
{
    public void LoadAll()
    {
        LoadAndMapCustomEvents();
        LoadShoutcasts();
        LoadMapping();
    }

    private static string ReadFile(string jsonFile)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        var resourceName = "PvPAnnouncer.json." + jsonFile;

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return "";
        }
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
    public void LoadShoutcasts()
    {
        var shoutJ = ReadFile("shoutcast.json");
        var r = JsonNode.Parse(shoutJ);
        if (r is JsonArray j)
        {
            foreach (var shout in j)
            { 
                var sh = ConstructShoutcast(shout?.ToString() ?? "");
                PluginServices.PluginLog.Verbose($"Constructed {sh.Id} with {sh.SoundPath}");
                casterRepository.RegisterAttribute(sh.Shoutcaster);
                shoutcastRepository.SetShoutcast(sh.Id, sh);
                foreach (var shAttribute in sh.Attributes)
                {
                    attributeRepository.RegisterAttribute(shAttribute);

                }
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

    private void MapEvent(string id, List<string> shouts)
    {
        List<string> newShoutList = [];
        foreach (var shout in shouts)
        {
            if (shout.Equals("LIMIT_BREAK_LIST"))
            {
                foreach (var se in InternalConstants.LimitBreakListStr)
                {
                    if (shoutcastRepository.ContainsKey(se))
                    {
                        newShoutList.Add(se);
                    }
                    else
                    {
                        PluginServices.PluginLog.Warning($"{se} not found in shoutcast repository!");
                    }
                }

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

    private Shoutcast ConstructShoutcast(string json)
    {
        var j = JsonNode.Parse(json);
        if (j == null)
        {
            return builder.Build();
        }
        if (j["id"] != null)
        {
            builder.WithId(j["id"]!.GetValue<string>());
        }
        
        if (j["icon"] != null)
        {
            builder.WithIcon(Convert.ToUInt32(j["icon"]!.GetValue<string>()));
        }
        
        if (j["transcription"] != null)
        {
            var dict =  j["transcription"][0].Deserialize<Dictionary<string, string>>();
            builder.WithTranscription(dict ?? []);
        }
        
        if (j["duration"] != null)
        {
            builder.WithDuration(Convert.ToByte(j["duration"]!.GetValue<string>()));
        }
                
        if (j["style"] != null)
        {
            builder.WithStyle(Convert.ToByte(j["style"]!.GetValue<string>()));
        }
        if (j["shoutcaster"] != null)
        {
            builder.WithShoutcaster(j["shoutcaster"]!.GetValue<string>());
        }
        
        if (j["attributes"] != null)
        {
            var att =  j["attributes"]!.Deserialize<List<string>>();
            builder.WithAttributes(att ?? []);
        }
        
        if (j["soundPath"] != null)
        {
            builder.WithSoundPath(j["soundPath"]!.GetValue<string>());
        }
        
        if (j["cutsceneLine"] != null)
        {
            builder.WithCutsceneLine(j["cutsceneLine"]!.GetValue<string>());
        }
        
        if (j["contentDirectorBattleTalkVo"] != null)
        {
            builder.WithContentDirectorBattleTalkVo(Convert.ToUInt32(j["contentDirectorBattleTalkVo"]!.GetValue<string>()));
        }
        
        if (j["npcYell"] != null)
        {
            builder.WithNpcYell(Convert.ToUInt32(j["npcYell"]!.GetValue<string>()));
        }
        
        if (j["instanceContentTextDataRow"] != null)
        {
            builder.WithInstanceContentTextDataRow(Convert.ToUInt32(j["instanceContentTextDataRow"]!.GetValue<string>()));
        }

        
        return builder.Build();
    }
}