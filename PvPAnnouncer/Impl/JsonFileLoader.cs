using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class JsonFileLoader(IShoutcastBuilder shoutcastBuilder, IShoutcastRepository shoutcastRepository, IEventShoutcastMapping eventShoutcastMapping) : IJsonFileLoader
{
    public void LoadEvents()
    {
        var eventsJ = File.ReadAllText("event.json");
        var r = JsonNode.Parse(eventsJ);
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

    public void LoadShoutcasts()
    {
        var shout = File.ReadAllText("shoutcast.json");
    }

    public void LoadMapping()
    {
        var mapping = File.ReadAllText("eventShoutcastMapping.json");
    }
}