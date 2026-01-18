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
    private string nn = "";
    private string vv = "";
    private long dd = 0L;
    private bool hide = false;
    
    public static string GetPath(string announcement)
    {
        if (announcement.StartsWith("cut"))
        {
            return announcement + "_en.scd";

        }
        return "sound/voice/vo_line/" + announcement + "_en.scd";
    }
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
           play(ll);
        }

        if (ImGui.Button("Play the weeb version"))
        {
            PluginServices.SoundManager.PlaySound("sound/voice/vo_line/"+ l + "_ja.scd");

        }
        var h = hide;
        if (ImGui.Checkbox("Hide Battle Talk", ref h))
        {
            hide = h;
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
        ImGui.Separator();
        var n = nn;
        if (ImGui.InputText("Name", ref n))
        {
            nn = n;
        }
        
        var v = vv;
        if (ImGui.InputText("Voice Line", ref v))
        {
            vv = v;
        }
        var d = dd;

        if (ImGui.Button("Go Back And Play"))
        {
            ll = l - 1;
            play(ll);
        }
        ImGui.SameLine();
        if (ImGui.Button("Go Forward And Play"))
        {
            ll = l + 1;
            play(ll);
            
        }
        if (ImGui.InputScalar("Duration", ref d, 1L, 1L))
        {
            dd = d;
        }

        if (ImGui.Button("Save This"))
        {
            PluginServices.Config.Dev_VoLineList.Add($" public static readonly BattleTalk VO{ll} new BattleTalk(\"{nn.Trim()}\", \"{ll}\", {dd}, \"{vv.Trim()}\"/, GetPersonalization([Personalization.{nn}Announcer])); // {vv}");
            PluginServices.Config.Save();
            vv = "";
        }
        
    }

    private void play(long l)
    {
        PluginServices.SoundManager.PlaySound("sound/voice/vo_line/"+ l + "_en.scd");
        if (!hide)
        {
            PluginServices.Announcer.SendBattleTalk(new BattleTalk("Unknown", l.ToString(), []));
        }
    }
    
    private void SendBattleTalk(string line)
    {
        BattleTalk battleTalk = new BattleTalk("Metem", line, []);
        var name = battleTalk.Name;
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

    private void SendBattleTalk(string name, string text, float duration, byte style, uint image)
    {
      
            unsafe
            {
                UIModule.Instance()->ShowBattleTalkImage(name, text, duration, image, style);
            }
        
 
    }
    public void Dispose()
    {
    }
}