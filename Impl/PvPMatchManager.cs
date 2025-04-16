using System.Collections.Generic;
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
        PluginServices.ClientState.TerritoryChanged += ClientStateOnTerritoryChanged;
        PluginServices.ClientState.CfPop += ClientStateOnCfPop;
        PluginServices.ClientState.Login += OnLogin;
        
    }

    private void OnLogin()
    {
        
    }

    private void ClientStateOnCfPop(ContentFinderCondition obj)
    {
        PluginServices.PluginLog.Information("OnCfPop");
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
        if (PluginServices.ClientState.LocalPlayer != null && entityId == PluginServices.ClientState.LocalPlayer.EntityId)
        {
            return true;
        }
        bool wantsFullParty = PluginServices.Config.WantsFullParty;
        bool wantsLightParty = PluginServices.Config.WantsLightParty;
        if (wantsLightParty)
        {
            return LightParty.Contains(entityId);
        }
        if (wantsFullParty)
        {
            return FullParty.Contains(entityId);
        }

       
        
        return false;
    }

    public void MatchEntered()
    {
        List<uint> partyMembers = [];
        foreach(IPartyMember member in PluginServices.PartyList)
        {
            PluginServices.PluginLog.Verbose($"Registering {member.ObjectId} to the full party");
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
            PluginServices.PluginLog.Verbose($"Registering {member.ObjectId} to the light party");
            uint id = member.ObjectId;
            if (id != 0)
            {
                members.Add(id);
            }
        }

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