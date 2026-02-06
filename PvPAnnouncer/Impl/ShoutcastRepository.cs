using System;
using System.Collections.Generic;
using System.Linq;
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

    public List<Shoutcast> GetShoutcasts()
    {
        return _shoutcasts.Values.ToList();
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
            builder.WithIcon(Convert.ToUInt32(j["icon"]!.GetValue<string>()));
        }
        
        if (j["transcription"] != null)
        {
            var tranNode = j["transcription"];
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