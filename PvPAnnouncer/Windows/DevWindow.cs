using System;
using System.Numerics;
using Dalamud.Interface.ImGuiNotification;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.UI;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Components;
using Dalamud.Interface.Utility;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Windows;

public class DevWindow: Window, IDisposable
{
    private string LastUsedAction = "";
    public DevWindow() : base(
        "PvPAnnouncer Dev Window", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };

    }

    private long ll = 0L;
    private string ss = "";
    public override void Draw()
    {
  
        var s = ss;
        ImGui.Text("Play A Sound path");
        if (ImGui.InputText("###SoundPath", ref s))
        {
            ss = s;

        }
        ImGui.SameLine();
        if (ImGui.Button("Play###SoundPathButton"))
        {
            PluginServices.Announcer.PlaySound(s);
        }

        var l = ll;
        ImGui.Text("Play sound/voice/vo_line/"+ l + "_en.scd");
        if(ImGui.InputScalar("###SoundLong", ref l, 1L, 1L))
        {
            ll = l;
        }
        ImGui.SameLine();
        if (ImGui.Button("Play###VoLineButton"))
        {
            PluginServices.SoundManager.PlaySound(AnnouncerLines.GetPath(l.ToString()));
            PluginServices.Announcer.SendBattleTalk(l.ToString());
        }

        ImGui.Separator();
        ImGui.Text("Last Action Used: " + PluginServices.PvPEventBroker.GetLastAction());
        if (ImGui.Button("Get Statuses"))
        {
            foreach (var status in PluginServices.ObjectTable.LocalPlayer?.StatusList)
            {
                PluginServices.PluginLog.Verbose(status.StatusId + " " + status.GameData.Value.Name.ToString());
            }
        }
        
    }
    
    
    private void SendBattleTalk(string line)
    {
        BattleTalk battleTalk = new BattleTalk(line);
        var name = "Metem";
        var text = battleTalk.Text.ToString();
        var duration = battleTalk.Duration;
        var icon = battleTalk.Icon;
        var style = battleTalk.Style;
        if (icon != 0)
        {
            unsafe
            {
                UIModule.Instance()->ShowBattleTalkImage(name, text, icon, duration, style);
            }
        }
        else
        {
            unsafe
            {
                UIModule.Instance()->ShowBattleTalk(name, text, duration, style);
            }
        }
    }

    public void Dispose()
    {
    }
}