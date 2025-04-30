using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Party;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Info;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PvPMatchManager: IPvPMatchManager, IPvPEventPublisher
{
    public uint Self { get; set; }
    
    private readonly HashSet<uint> _deadMembers = [];

    public PvPMatchManager()
    {
        PluginServices.ClientState.TerritoryChanged += ClientStateOnTerritoryChanged;
        PluginServices.ClientState.CfPop += ClientStateOnCfPop;
        PluginServices.ClientState.Login += OnLogin;
        PluginServices.DutyState.DutyStarted += MatchStarted;
        PluginServices.DutyState.DutyCompleted += MatchEnded;
    }

    private void OnLogin()
    {
        //todo: check for bgm volume here?
    }

    private void ClientStateOnCfPop(ContentFinderCondition obj)
    {
        PluginServices.PluginLog.Verbose("OnCfPop");
        MatchQueued();
    }

    private void ClientStateOnTerritoryChanged(ushort territory)
    {
        var t = PluginServices.DataManager.GetExcelSheet<TerritoryType>().GetRow(territory);
        if (t.IsPvpZone && PluginServices.PvPMatchManager.IsInPvP())
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

    public bool IsInPvP()
    {
        if (PluginServices.Config.WolvesDen)
        {
            return PluginServices.ClientState.IsPvP;
        }
        return PluginServices.ClientState.IsPvPExcludingDen;

    }

    public void MatchEntered(ushort territory)
    {
        
        //todo: check to make sure the user has their voice bgm at not-zero and also not muted
        if (PluginServices.PvPMatchManager.IsInPvP())
        {
            EmitToBroker(new MatchEnteredMessage(territory));
            unsafe
            {
                var weatherId = WeatherManager.Instance()->GetCurrentWeather();
                PluginServices.PluginLog.Verbose($"Match Weather: {weatherId}");
                EmitToBroker(new MatchWeatherMessage(weatherId));
            }
        }
    }

    public void MatchStarted(object? sender, ushort @ushort)
    {
        if (PluginServices.PvPMatchManager.IsInPvP())
        {
            EmitToBroker(new MatchStartedMessage());    
        }
    }

    public void MatchEnded(object? sender, ushort @ushort)
    {
        if (PluginServices.PvPMatchManager.IsInPvP())
        {
            EmitToBroker(new MatchEndMessage());
        }
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