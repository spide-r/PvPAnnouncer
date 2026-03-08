using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class EventShoutcastMapping : IEventShoutcastMapping
{
    private readonly Dictionary<string, List<string>> _map = new();

    public void AddShoutcasts(string eventId, List<string> shoutcasts)
    {
        if (_map.TryGetValue(eventId, out var shoutList))
        {
            foreach (var shoutcast in shoutcasts.Where(shoutcast => !shoutList.Contains(shoutcast)))
            {
                shoutList.Add(shoutcast);
            }
        }
        else
        {
            _map.Add(eventId, [..shoutcasts]);
        }
    }


    public void MergeShoutcast(string eventId, string shoutcast)
    {
        if (_map.TryGetValue(eventId, out var shoutList))
        {
            if (!shoutList.Contains(shoutcast))
            {
                shoutList.Add(shoutcast);
            }
        }
        else
        {
            _map[eventId] = [shoutcast];
        }
    }

    public void ReplaceMapping(string eventId, List<string> shoutcast)
    {
        _map[eventId] = shoutcast;
    }

    public void MergeMapping(string eventId, List<string> shoutcast)
    {
        var list = _map.GetValueOrDefault(eventId, []);
        list.AddRange(shoutcast);
        list = list.Distinct().ToList();
        _map[eventId] = list;
    }

    public void RemoveShoutcast(string eventId, string shoutcast)
    {
        if (_map.TryGetValue(eventId, out var shoutList))
        {
            shoutList.Remove(shoutcast);
        }
    }

    public void PurgeMapping(string shoutcast)
    {
        foreach (var keyValuePair in _map) keyValuePair.Value.Remove(shoutcast);
    }

    public bool ContainsKey(string shoutcast)
    {
        return _map.ContainsKey(shoutcast);
    }

    public List<string> GetShoutcastList(string eventId)
    {
        return _map.TryGetValue(eventId, out var shoutList) ? shoutList : [];
    }

    public List<string> GetAllEvents()
    {
        return _map.Keys.ToList();
    }
}