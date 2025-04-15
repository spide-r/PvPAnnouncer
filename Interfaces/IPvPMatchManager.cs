namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager
{
    uint Self {get; set;}
    uint[] LightParty { get; set; }
    uint[] FullParty { get; set; }

    void MatchEntered(ushort territory);
    void MatchStarted();
    void MatchEnded();
    void MatchLeft();
    void MatchQueued();

    void ClearLists();
    void PopulateLightParty(uint[] party);
    void PopulateFullParty(uint[] party);
    
    bool IsMonitoredUser(int userId);
    
    bool IsMonitoredUser(uint entityId);





}
