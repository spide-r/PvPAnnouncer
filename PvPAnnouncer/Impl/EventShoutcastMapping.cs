using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class EventShoutcastMapping: IEventShoutcastMapping
{
    private readonly Dictionary<string, List<string>> _map = new();
    public void AddShoutcasts(string eventId, List<string> shoutcasts)
    {
        if (_map.TryGetValue(eventId, out var shoutList))
        {
            shoutList.AddRange(shoutcasts);
        }
        else
        {
            _map.Add(eventId, [..shoutcasts]); //todo does this work???
        }
    }

    public void AddShoutcast(string eventId, string shoutcast)
    {
        if (_map.TryGetValue(eventId, out var shoutList))
        {
            shoutList.Add(shoutcast);
        }
        else
        {
            _map.Add(eventId, [shoutcast]); //todo does this work???
        }
    }

    public void RemoveShoutcast(string eventId, string shoutcast)
    {
        if (_map.TryGetValue(eventId, out var shoutList))
        {
            shoutList.Remove(shoutcast);
        }
    }

    public List<string> GetShoutcastList(string eventId)
    {
        return _map.TryGetValue(eventId, out var shoutList) ? shoutList : [];
    }
}