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
        PluginServices.ClientState.CfPop += ClientStateOnCfPop;
        PluginServices.DutyState.DutyStarted += MatchStarted;
        PluginServices.DutyState.DutyCompleted += MatchEnded;
    }

    private void ClientStateOnCfPop(ContentFinderCondition obj)
    {
        PluginServices.PluginLog.Verbose("OnCfPop");
        MatchQueued();
    }

    private void ClientStateOnTerritoryChanged(ushort territory)
    {
        var t = PluginServices.DataManager.GetExcelSheet<TerritoryType>().GetRow(territory);
        if (t.IsPvpZone && PluginServices.PlayerStateTracker.IsPvP())
        {
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
        PluginServices.PlayerStateTracker.CheckSoundState(); // sloppy - need to make this class not rely on PlayerStateTracker being loaded
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