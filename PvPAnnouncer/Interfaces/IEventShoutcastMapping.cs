using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public interface IEventShoutcastMapping
{
    public void AddShoutcasts(string eventId, List<string> shoutcasts);
    public void MergeShoutcast(string eventId, string shoutcast);
    public void ReplaceMapping(string eventId, List<string> shoutcast);
    public void ProcessMappingDelta(string eventId, Dictionary<string, List<string>> delta);
    public void RemoveShoutcast(string eventId, string shoutcast);
    public void PurgeMapping(string shoutcast);
    public bool ContainsKey(string shoutcast);
    public List<string> GetShoutcastList(string eventId);
    public List<string> GetAllEvents();
    public void Clear();
}