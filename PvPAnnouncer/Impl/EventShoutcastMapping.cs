using System.Collections.Generic;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class EventShoutcastMapping: IEventShoutcastMapping
{
    private readonly Dictionary<string, List<string>> _map = new();
    public void AddShoutcasts(string eventStr, List<string> shoutcasts)
    {
        if (_map.TryGetValue(eventStr, out var shoutList))
        {
            shoutList.AddRange(shoutcasts);
        }
        else
        {
            _map.Add(eventStr, [..shoutcasts]); //todo does this work???
        }
    }

    public void AddShoutcast(string eventStr, string shoutcast)
    {
        if (_map.TryGetValue(eventStr, out var shoutList))
        {
            shoutList.Add(shoutcast);
        }
        else
        {
            _map.Add(eventStr, [shoutcast]); //todo does this work???
        }
    }

    public void RemoveShoutcast(string eventStr, string shoutcast)
    {
        if (_map.TryGetValue(eventStr, out var shoutList))
        {
            shoutList.Remove(shoutcast);
        }
    }

    public List<string> GetShoutcastList(string eventStr)
    {
        return _map.TryGetValue(eventStr, out var shoutList) ? shoutList : [];
    }
}