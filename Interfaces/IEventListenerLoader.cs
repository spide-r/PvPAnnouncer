using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IEventListenerLoader
{
    public void LoadEventListeners();
    public PvPEvent[] GetPvPEvents();
}