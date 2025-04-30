using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Client.Game;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager, IPvPEventPublisher
{
    //todo: bug where the territory + pvp status + match weather isnt updating as I would expect

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
        
        if (PluginServices.PlayerStateTracker.IsPvP())
        {
            EnterPvP();
            MatchEntered(territory);
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
            var weatherId = WeatherManager.Instance()->GetCurrentWeather();
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
}