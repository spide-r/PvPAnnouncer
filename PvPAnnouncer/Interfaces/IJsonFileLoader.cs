namespace PvPAnnouncer.Interfaces;

public interface IJsonFileLoader
{
    public void LoadEvents();
    public void LoadShoutcasts();
    public void LoadMapping();
}