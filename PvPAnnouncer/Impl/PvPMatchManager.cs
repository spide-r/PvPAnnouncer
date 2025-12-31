using System;
using System.Collections.Generic;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager, IPvPEventPublisher
{
    private int _leftPoints = 0;
    private int _rightPoints = 0;
    private int _ourPoints = 0;
    public PvPMatchManager()
    {
        PluginServices.AddonLifecycle.RegisterListener(AddonEvent.PreDraw, "PvPFrontlineHeader", HandleHeaderPreDraw);
        PluginServices.ClientState.TerritoryChanged += ClientStateOnTerritoryChanged;
        PluginServices.ClientState.EnterPvP += EnterPvP;
        PluginServices.ClientState.CfPop += ClientStateOnCfPop;
        PluginServices.DutyState.DutyStarted += MatchStarted;
        PluginServices.DutyState.DutyCompleted += MatchEnded;
    }

    private void HandleHeaderPreDraw(AddonEvent type, AddonArgs args)
    {
        unsafe
        {
            var addon = (AtkUnitBase*)args.Addon.Address;
            
            _ourPoints = GetScore(9, addon);
            _rightPoints = GetScore(13, addon);
            _leftPoints = GetScore(12, addon);
        }
    }

    private unsafe int GetScore(uint id, AtkUnitBase* addon)
    {
        var alliance = addon->GetComponentNodeById(id)->GetComponent();
        var scoreNode = alliance->GetComponentById(8);
        var currentScoreNode = scoreNode->GetTextNodeById(2)->GetText().ToString();
        return Convert.ToInt32(currentScoreNode);
    }

    private void EnterPvP()
    {
        PluginServices.PluginLog.Verbose("EnterPvP");
        PluginServices.PlayerStateTracker.CheckSoundState(); //todo sloppy - need to make this class not rely on PlayerStateTracker being loaded
    }

    private void ClientStateOnCfPop(ContentFinderCondition obj)
    {
        PluginServices.PluginLog.Verbose("OnCfPop");
        MatchQueued();
    }

    private void ClientStateOnTerritoryChanged(ushort territory)
    {
        var t = PluginServices.DataManager.GetExcelSheet<TerritoryType>().GetRow(territory);
        PluginServices.PluginLog.Verbose("OnTerritoryChanged " + territory);
        if (t.IsPvpZone) 
        {
            if (territory == 250) // entering the wolves den
            { 
                if (PluginServices.PlayerStateTracker.IsPvP()) // Went from pvp area to wolves den 
                {
                    PluginServices.PluginLog.Verbose("Territory Change: PvP -> WD");
                    MatchLeft();
                }
            }
            else
            {
                PluginServices.PluginLog.Verbose("Territory Change: NoPvP->PvP");
                MatchEntered(territory); // entered a pvp zone that isnt the wolves den
                
            }
        }
        else
        {
            if (PluginServices.PlayerStateTracker.IsPvP()) // was in pvp zone before warp and then warped to a non-pvp zone
            {
                PluginServices.PluginLog.Verbose("Territory Change: PvP -> NoPvP");
                MatchLeft();
            }
        }
    }

    public bool IsMonitoredUser(int userId)
    {
        return IsMonitoredUser((uint)userId);
    }

    public bool IsMonitoredUser(uint entityId)
    {
        return PluginServices.ObjectTable.LocalPlayer != null && entityId == PluginServices.PlayerState.EntityId;
    }

    private bool Winning()
    {
        return _ourPoints >= _leftPoints && _ourPoints >= _rightPoints;
    }
    

    public void MatchEntered(ushort territory)
    {
        PluginServices.PluginLog.Verbose($"OnMatchEntered {territory}");
        EmitToBroker(new MatchEnteredMessage(territory));
        unsafe
        {
            var weatherId = WeatherManager.Instance()->GetWeatherForDaytime(territory, 0);
            PluginServices.PluginLog.Verbose($"Match Weather: {weatherId}");
            EmitToBroker(new MatchWeatherMessage(weatherId));
        }
        
    }

    public void MatchStarted(object? sender, ushort @ushort)
    {
        
        EmitToBroker(new MatchStartedMessage());    
        
    }

    public void MatchEnded(object? sender, ushort @ushort)
    {

        if (Winning())
        {
            EmitToBroker(new MatchVictoryMessage());
        }
        else
        {
            EmitToBroker(new MatchLossMessage());
        }
        
        EmitToBroker(new MatchEndMessage());
        
    }

    public void MatchLeft()
    {
        EmitToBroker(new MatchLeftMessage());

    }

    public void MatchQueued()
    {

    }

    public void EmitToBroker(IMessage pvpEvent)
    {
        PluginServices.PvPEventBroker.IngestMessage(pvpEvent);
    }

    public void Dispose()
    {
        PluginServices.ClientState.TerritoryChanged -= ClientStateOnTerritoryChanged;
        PluginServices.ClientState.EnterPvP -= EnterPvP;
        PluginServices.ClientState.CfPop -= ClientStateOnCfPop;
        PluginServices.DutyState.DutyStarted -= MatchStarted;
        PluginServices.DutyState.DutyCompleted -= MatchEnded;
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, "PvPFrontlineHeader", HandleHeaderPreDraw);

    }
}