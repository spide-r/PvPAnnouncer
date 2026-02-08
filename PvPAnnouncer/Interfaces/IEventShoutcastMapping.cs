using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public interface IEventShoutcastMapping
{
    public void AddShoutcasts(string eventId, List<string> shoutcasts);
    public void AddShoutcast(string eventId, string shoutcast);
    public void RemoveShoutcast(string eventId, string shoutcast);
    public List<string> GetShoutcastList(string eventId);
}