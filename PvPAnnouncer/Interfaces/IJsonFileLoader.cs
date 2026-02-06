namespace PvPAnnouncer.Interfaces;

public interface IJsonFileLoader
{
    public void LoadAll();
    public void LoadAndMapCustomEvents();
    public void LoadShoutcasts();
    public void LoadMapping();
}