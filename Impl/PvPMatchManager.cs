using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Party;
using PvPAnnouncer.impl.PvPEvents;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager, IPvPEventPublisher
{
    public uint Self { get; set; }
    public uint[] LightParty { get; set; } = [];
    public uint[] AllianceMembers { get; set; } = [];
    public uint[] GrandCompanyMembers { get; set; } = [];

    public bool IsMonitoredUser(int userId)
    {
        return IsMonitoredUser((uint)userId);
    }

    public bool IsMonitoredUser(uint entityId)
    {
        
        //todo: all the config stuff
        bool wantsGC = false;
        bool wantsAlliance = false;
        bool wantsLP = false;
        if (wantsGC)
        {
            return GrandCompanyMembers.Contains(entityId);
        }

        if (wantsAlliance)
        {
            return AllianceMembers.Contains(entityId);
        }

        if (wantsLP)
        {
            return LightParty.Contains(entityId);
        }
        
        return entityId == Self;
    }

    public void MatchEntered()
    {
        throw new System.NotImplementedException();
    }

    public void MatchStarted()
    {
        //send match started
        throw new System.NotImplementedException();
    }

    public void MatchEnded()
    {
        //send match ended
        throw new System.NotImplementedException();
    }

    public void MatchLeft()
    {
        ClearLists();
    }

    public void MatchQueued()
    {
        List<uint> members = [];
        foreach(IPartyMember member in PluginServices.PartyList)
        {
            uint id = member.ObjectId;
            members.Add(id);
        }

        Self = PluginServices.ClientState.LocalPlayer!.EntityId;
        PopulateLightParty(members.ToArray());
    }

    public void ClearLists()
    {
        LightParty = [];
        AllianceMembers = [];
        GrandCompanyMembers = [];
    }

    public void PopulateLightParty(uint[] party)
    {
        LightParty = party;
    }

    public void PopulateAllianceMembers(uint[] allianceMembers)
    {
        AllianceMembers = allianceMembers;
    }

    public void PopulateGrandCompany(uint[] grandCompany)
    {
        GrandCompanyMembers = grandCompany;
    }

    public void EmitToBroker(IPacket pvpEvent)
    {
        throw new System.NotImplementedException();
    }
}