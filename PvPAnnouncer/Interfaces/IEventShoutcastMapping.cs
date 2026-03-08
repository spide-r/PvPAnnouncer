using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public interface IEventShoutcastMapping
{
    public void AddShoutcasts(string eventId, List<string> shoutcasts);
    public void MergeShoutcast(string eventId, string shoutcast);
    public void ReplaceMapping(string eventId, List<string> shoutcast);
    public void MergeMapping(string eventId, List<string> shoutcast);
    public void RemoveShoutcast(string eventId, string shoutcast);
    public void PurgeMapping(string shoutcast);
    public bool ContainsKey(string shoutcast);
    public List<string> GetShoutcastList(string eventId);
    public List<string> GetAllEvents();
}