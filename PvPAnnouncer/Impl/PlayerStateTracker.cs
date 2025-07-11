﻿using System;
using System.Linq;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.Config;
using Dalamud.Interface.ImGuiNotification;
using Dalamud.Plugin.Services;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.impl.PvPEvents;
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
     PluginServices.Condition.ConditionChange += OnConditionChange;
    }

    private void OnConditionChange(ConditionFlag flag, bool value)
    {
        if (flag == ConditionFlag.Unconscious)
        {
            if (value)
            {
                EmitToBroker(new UserDiedMessage());
            }
            else
            {
                if (PluginServices.ClientState.LocalPlayer != null)
                {
                    EmitToBroker(new UserResurrectedMessage());
                }
            }
        }

        if (flag == ConditionFlag.Transformed && value)
        {
            PluginServices.PluginLog.Verbose("Transformed");
            EmitToBroker(new UserEnteredMechMessage());
        }
    }


    private void OnUpdate(IFramework framework)
    {
        if (PluginServices.ClientState.LocalPlayer == null)
        {
            return;
        }

        if (!PluginServices.ClientState.IsPvP)
        {
            return;
        }
        
        // Attributed to Oof Plugin
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

    public void CheckSoundState()
    {
        if (!PluginServices.Config.Notify)
        {
            return;
        }
        uint voiceVolume = PluginServices.GameConfig.System.GetUInt(SystemConfigOption.SoundVoice.ToString());   
        uint voiceMuted = PluginServices.GameConfig.System.GetUInt(SystemConfigOption.IsSoundVoiceAlways.ToString());
        uint sndVoice = PluginServices.GameConfig.System.GetUInt(SystemConfigOption.IsSndVoice.ToString());
        PluginServices.PluginLog.Verbose($"VoiceVolume: {voiceVolume}, VoiceMuted: {voiceMuted}, SndVoice {sndVoice}");
        if (sndVoice == 1 || voiceVolume < 1)
        {
            Notification n = new Notification();
            n.Title = "Voice Volume Error!";
            n.Type = NotificationType.Error;
            n.Minimized = false;
            n.MinimizedText = "Voice Volume is muted!";
            n.Content = "Your voice volume is either muted or set to zero! You will be unable to hear the announcer until this is fixed!";
            PluginServices.NotificationManager.AddNotification(n);
        }
    }

    public bool IsDawntrailInstalled()
    {
        return PluginServices.DataManager.FileExists(AnnouncerLines.GetPath(AnnouncerLines.Fallen));
    }

    public void EmitToBroker(IMessage pvpEvent)
    {
        PluginServices.PvPEventBroker.IngestMessage(pvpEvent);
    }

    public void Dispose()
    {
        PluginServices.Framework.Update -= OnUpdate;   
        PluginServices.Condition.ConditionChange -= OnConditionChange;

    }
}