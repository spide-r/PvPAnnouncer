using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IShoutcastRepository
{
    public Shoutcast? GetShoutcast(string shoutcastId);
    public void DeleteShoutcast(string shoutcastId);

    public List<Shoutcast> GetShoutcasts();
    public void SetShoutcast(string shoutcastId, Shoutcast shoutcast);
    public bool ContainsKey(string shoutcastId);
    public List<string> GetShoutcasters();
    public List<string> GetAttributes();
    public void Clear();
}