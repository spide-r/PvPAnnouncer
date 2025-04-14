namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager
{
    ulong Self {get; set;}
    ulong[] LightParty { get; set; }
    ulong[] AllianceMembers { get; set; }
    ulong[] GrandCompanyMembers { get; set; }

    void MatchEntered();
    void MatchStarted();
    void MatchEnded();
    void MatchLeft();
    void MatchQueued();

    void ClearLists();
    void PopulateLightParty(ulong[] party);
    void PopulateAllianceMembers(ulong[] allianceMembers);
    void PopulateGrandCompany(ulong[] grandCompany);





}
