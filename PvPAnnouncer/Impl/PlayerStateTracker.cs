using Dalamud.Game.ClientState.Conditions;
using Dalamud.Plugin.Services;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class PlayerStateTracker: IPlayerStateTracker
{
    private float PrevPos { get; set; }
    private float PrevVel { get; set; }
    private float DistJump { get; set; }
    private bool WasFalling { get; set; }
    public PlayerStateTracker()
    {
     PluginServices.Framework.Update += OnUpdate;   
    }

    
    // Attributed to Oof Plugin
    private void OnUpdate(IFramework framework)
    {
        if (PluginServices.ClientState.LocalPlayer == null)
        {
            return;
        }
        
        if (PluginServices.Condition[ConditionFlag.BetweenAreas] ||
            PluginServices.Condition[ConditionFlag.BetweenAreas51] ||
            PluginServices.Condition[ConditionFlag.BeingMoved])
        {
            return;
        }

        if (PluginServices.Condition[ConditionFlag.WatchingCutscene] ||
            PluginServices.Condition[ConditionFlag.WatchingCutscene78])
        {
            return;
        }
        
        var isFalling = PluginServices.Condition[ConditionFlag.Jumping];
        var pos = PluginServices.ClientState.LocalPlayer.Position.Y;
        var velocity = PrevPos - pos;

        if (isFalling && !WasFalling)
        {
            if (PrevVel < 0.17)
            {
                DistJump = pos;
            } 
        }
        else if (WasFalling && !isFalling)
        {
            if (DistJump - pos > 9.60)
            {
                EmitToBroker(new UserZoneOutMessage());
            }
        }
        PrevPos = pos;
        PrevVel = velocity;
        WasFalling = isFalling;
        

    }

    public bool IsPvP()
    {
        if (PluginServices.Config.WolvesDen)
        {
            return PluginServices.ClientState.IsPvP;
        }
        return PluginServices.ClientState.IsPvPExcludingDen;

    }

    public void EmitToBroker(IMessage pvpEvent)
    {
        PluginServices.PvPEventBroker.IngestMessage(pvpEvent);
    }
}