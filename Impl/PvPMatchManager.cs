using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager
{
    public ulong Self { get; set; }
    public ulong[] LightParty { get; set; } = [];
    public ulong[] AllianceMembers { get; set; } = [];
    public ulong[] GrandCompanyMembers { get; set; } = [];

    public bool IsMonitoredUser(uint userId)
    {
        throw new System.NotImplementedException();
    }
    
    public void MatchEntered()
    {
        throw new System.NotImplementedException();
    }

    public void MatchStarted()
    {
        throw new System.NotImplementedException();
    }

    public void MatchEnded()
    {
        throw new System.NotImplementedException();
    }

    public void MatchLeft()
    {
        throw new System.NotImplementedException();
    }

    public void MatchQueued()
    {
        throw new System.NotImplementedException();
    }

    public void ClearLists()
    {
        throw new System.NotImplementedException();
    }

    public void PopulateLightParty(ulong[] party)
    {
        throw new System.NotImplementedException();
    }

    public void PopulateAllianceMembers(ulong[] allianceMembers)
    {
        throw new System.NotImplementedException();
    }

    public void PopulateGrandCompany(ulong[] grandCompany)
    {
        throw new System.NotImplementedException();
    }
}