using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager
{
    public uint Self { get; set; }
    public uint[] LightParty { get; set; } = [];
    public uint[] AllianceMembers { get; set; } = [];
    public uint[] GrandCompanyMembers { get; set; } = [];

    public bool IsMonitoredUser(int userId)
    {
        throw new System.NotImplementedException();
    }

    public bool IsMonitoredUser(uint entityId)
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