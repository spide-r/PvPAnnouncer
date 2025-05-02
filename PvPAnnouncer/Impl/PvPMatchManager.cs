using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Client.Game;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager, IPvPEventPublisher
{
    private readonly HashSet<uint> _deadMembers = [];

    public PvPMatchManager()
    {
        PluginServices.ClientState.TerritoryChanged += ClientStateOnTerritoryChanged;
        PluginServices.ClientState.EnterPvP += EnterPvP;
        PluginServices.ClientState.CfPop += ClientStateOnCfPop;
        PluginServices.DutyState.DutyStarted += MatchStarted;
        PluginServices.DutyState.DutyCompleted += MatchEnded;
    }

    private void EnterPvP()
    {
        PluginServices.PluginLog.Verbose("EnterPvP");
        PluginServices.PlayerStateTracker.CheckSoundState(); // sloppy - need to make this class not rely on PlayerStateTracker being loaded
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
        return PluginServices.ClientState.LocalPlayer != null && entityId == PluginServices.ClientState.LocalPlayer.EntityId;
    }

    public void RegisterDeath(uint userId)
    {
        _deadMembers.Add(userId);
    }

    public void UnregisterDeath(uint userId)
    {
        _deadMembers.Remove(userId);
    }

    public bool IsDead(uint userId)
    {
        return _deadMembers.Contains(userId);
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
    }
}