using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IShoutcastRepository
{
    public Shoutcast GetShoutcast(string shoutcastId);
    
    public List<Shoutcast> GetShoutcasts();
    public void SetShoutcast(string shoutcastId, Shoutcast shoutcast);
    public bool UniqueKey(string shoutcastId);
    
    public Shoutcast ConstructShoutcast(string json);

}