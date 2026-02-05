namespace PvPAnnouncer.Interfaces;

public interface IShoutcastRepository
{
    public IShoutcast GetShoutcast(string shoutcastId);
    public void SetShoutcast(string shoutcastId, IShoutcast shoutcast);
    public bool UniqueKey(string shoutcastId);
    
    public IShoutcast ConstructShoutcast(string json);

}