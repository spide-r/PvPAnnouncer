using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Party;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager, IPvPEventPublisher
{
    public uint Self { get; set; }
    public uint[] LightParty { get; set; } = [];
    public uint[] FullParty { get; set; } = [];

    public PvPMatchManager()
    {
        //todo: subscribe the below functions to the correct things
    }

    public bool IsMonitoredUser(int userId)
    {
        return IsMonitoredUser((uint)userId);
    }

    public bool IsMonitoredUser(uint entityId)
    {
        
        //todo: all the config stuff
        bool wantsFullParty = false;
        bool wantsLightParty = false;
        if (wantsFullParty)
        {
            return FullParty.Contains(entityId);
        }

        if (wantsLightParty)
        {
            return LightParty.Contains(entityId);
        }
        
        return entityId == PluginServices.ClientState.LocalPlayer!.EntityId;
    }

    public void MatchEntered()
    {
        List<uint> partyMembers = [];
        foreach(IPartyMember member in PluginServices.PartyList)
        {
            uint id = member.ObjectId;
            partyMembers.Add(id);
        }

        PopulateFullParty(partyMembers.ToArray());
    }

    public void MatchEntered(uint territory)
    {
        MatchEntered();
        EmitToBroker(new MatchEnteredMessage(territory));
    }

    public void MatchStarted()
    {
        EmitToBroker(new MatchStartedMessage());    }

    public void MatchEnded()
    {
        EmitToBroker(new MatchEndMessage());

    }

    public void MatchLeft()
    {
        ClearLists();
        EmitToBroker(new MatchLeftMessage());

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
        FullParty = [];
    }

    public void PopulateLightParty(uint[] party)
    {
        LightParty = party;
    }

    public void PopulateFullParty(uint[] party)
    {
        FullParty = party;
    }
    

    public void EmitToBroker(IPacket pvpEvent)
    {
        PluginServices.PvPEventBroker.IngestPacket(pvpEvent);
    }
}