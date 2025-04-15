﻿using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Party;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Data;
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
        PluginServices.ClientState.TerritoryChanged += ClientStateOnTerritoryChanged;
        PluginServices.ClientState.CfPop += ClientStateOnCfPop;
    }

    private void ClientStateOnCfPop(ContentFinderCondition obj)
    {
        MatchQueued();
    }

    private void ClientStateOnTerritoryChanged(ushort obj)
    {
        if (InternalConstants.pvpTerritories.Contains(obj))
        {
            MatchEntered(obj);
        }
        else
        {
            MatchLeft();
        }
    }

    public bool IsMonitoredUser(int userId)
    {
        return IsMonitoredUser((uint)userId);
    }

    public bool IsMonitoredUser(uint entityId)
    {
        
        bool wantsFullParty = PluginServices.Config.WantsFullParty;
        bool wantsLightParty = PluginServices.Config.WantsLightParty;
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

    public void MatchEntered(ushort territory)
    {
        
        //todo: check to make sure the user has their voice bgm at not-zero and also not muted
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
    

    public void EmitToBroker(IMessage pvpEvent)
    {
        PluginServices.PvPEventBroker.IngestPacket(pvpEvent);
    }
}