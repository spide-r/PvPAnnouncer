using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public interface IShoutcastEventMapping
{
    public void AddShoutcast(string eventStr, string shoutcast);
    public void RemoveShoutcast(string eventStr, string shoutcast);
    public List<string> GetShoutcastList(string eventStr);
}