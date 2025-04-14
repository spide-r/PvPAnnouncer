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
    void PopulateLightParty(ulong[] party);
    void PopulateAllianceMembers(ulong[] allianceMembers);
    void PopulateGrandCompany(ulong[] grandCompany);
    
    bool IsMonitoredUser(int userId);
    
    bool IsMonitoredUser(uint entityId);





}
