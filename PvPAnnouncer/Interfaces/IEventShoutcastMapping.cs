using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public interface IEventShoutcastMapping
{
    public void AddShoutcasts(string eventStr, List<string> shoutcasts);
    public void AddShoutcast(string eventStr, string shoutcast);
    public void RemoveShoutcast(string eventStr, string shoutcast);
    public List<string> GetShoutcastList(string eventStr);
}