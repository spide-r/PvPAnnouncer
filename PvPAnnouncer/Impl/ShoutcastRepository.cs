using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class ShoutcastRepository(IShoutcastBuilder builder) : IShoutcastRepository
{
    private readonly Dictionary<string, Shoutcast> _shoutcasts = new();
    public Shoutcast GetShoutcast(string shoutcastId)
    {
        return _shoutcasts[shoutcastId];
    }

    public void SetShoutcast(string shoutcastId, Shoutcast shoutcast)
    {
        _shoutcasts[shoutcastId] = shoutcast;
    }

    public bool UniqueKey(string shoutcastId)
    {
        return !_shoutcasts.ContainsKey(shoutcastId);
    }

    public Shoutcast ConstructShoutcast(string json)
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
            builder.WithIcon(j["icon"]!.GetValue<uint>());
        }
        
        if (j["transcription"] != null)
        {
            var dict =  j["transcription"]!.Deserialize<Dictionary<string, string>>();
            builder.WithTranscription(dict ?? []);
        }
        
        if (j["duration"] != null)
        {
            builder.WithDuration(j["duration"]!.GetValue<byte>());
        }
                
        if (j["style"] != null)
        {
            builder.WithStyle(j["style"]!.GetValue<byte>());
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
            builder.WithContentDirectorBattleTalkVo(j["contentDirectorBattleTalkVo"]!.GetValue<uint>());
        }
        
        if (j["npcYell"] != null)
        {
            builder.WithNpcYell(j["npcYell"]!.GetValue<uint>());
        }
        
        if (j["instanceContentTextDataRow"] != null)
        {
            builder.WithInstanceContentTextDataRow(j["instanceContentTextDataRow"]!.GetValue<uint>());
        }

        
        return builder.Build();
        
    }
}