namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager
{
    uint Self {get; set;}
    uint[] LightParty { get; set; }
    uint[] AllianceMembers { get; set; }
    uint[] GrandCompanyMembers { get; set; }

    void MatchEntered();
    void MatchStarted();
    void MatchEnded();
    void MatchLeft();
    void MatchQueued();

    void ClearLists();
    void PopulateLightParty(uint[] party);
    void PopulateAllianceMembers(uint[] allianceMembers);
    void PopulateGrandCompany(uint[] grandCompany);
    
    bool IsMonitoredUser(int userId);
    
    bool IsMonitoredUser(uint entityId);





}
