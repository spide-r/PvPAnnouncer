namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager
{
    ulong[] LIGHT_PARTY { get; set; }
    ulong[] ALLIANCE_MEMBERS { get; set; }
    ulong[] GRAND_COMPANY_MEMBERS { get; set; }

    void MatchEntered();
    void MatchStarted();
    void MatchEnded();
    void MatchLeft();
    void MatchQueued();

    void ClearLists();
    void PopulateLightParty(ulong[] party);
    void PopulateAllianceMembers(ulong[] allianceMembers);
    void PopulateGrandCompany(ulong[] grandCompany);
    
    bool IsInLightParty();
    bool IsInAlliance();
    bool IsInGrandCompany();





}
