namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager
{
    //todo: should I even bother listening to the actions of teammates? - I'll need to check after more people have finished the alpha 
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

    void RegisterDeath(uint userId); //todo: current implementation only works with the self-user, not teammates
    
    void UnregisterDeath(uint userId);
    bool IsDead(uint userId);





}
