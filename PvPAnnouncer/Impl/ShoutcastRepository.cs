using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class ShoutcastRepository : IShoutcastRepository
{
    private readonly Dictionary<string, Shoutcast> _shoutcasts = new();

    public Shoutcast? GetShoutcast(string shoutcastId)
    {
        if (_shoutcasts.ContainsKey(shoutcastId))
        {
            return _shoutcasts[shoutcastId];
        }

        return null;
    }

    public void DeleteShoutcast(string shoutcastId)
    {
        _shoutcasts.Remove(shoutcastId);
    }

    public List<Shoutcast> GetShoutcasts()
    {
        return _shoutcasts.Values.ToList();
    }

    public void SetShoutcast(string shoutcastId, Shoutcast shoutcast)
    {
        _shoutcasts[shoutcastId] = shoutcast;
    }

    public bool ContainsKey(string shoutcastId)
    {
        return _shoutcasts.ContainsKey(shoutcastId);
    }

    public List<string> GetShoutcasters()
    {
        return _shoutcasts.Values.Select(sc => sc.Shoutcaster).Distinct().ToList();
    }

    public List<string> GetAttributes()
    {
        return _shoutcasts.Values.SelectMany(sc => sc.Attributes).Distinct().ToList();
    }
}