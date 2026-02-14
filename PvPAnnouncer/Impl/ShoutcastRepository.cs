using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class ShoutcastRepository : IShoutcastRepository
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
        bool added = _shoutcasts.TryAdd(shoutcastId,  shoutcast);
        if (!added)
        {
            PluginServices.PluginLog.Verbose($"Unable to add {shoutcastId}");
        }
    }

    public bool ContainsKey(string shoutcastId)
    {
        return _shoutcasts.ContainsKey(shoutcastId);
    }


}