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
        "PvPAnnouncer Dev Window")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };

    }

    private long ll = 0L;
    private string icon = "";
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
        var l = ll;

        var ic = icon;
        if(ImGui.InputText("Icon###Icon", ref ic))
        {
            icon = ic;
        }

        if (ImGui.Button("Test Icon"))
        {
            PluginServices.Announcer.SendBattleTalk(new BattleTalk("Unknown", Convert.ToUInt32(icon), "8291265", 2, "Asdf", []));

        }
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
        
        ImGui.Text("Events: ");
        var i = 1;
        foreach (var pvPEvent in PluginServices.ListenerLoader.GetPvPEvents())
        {
            if (ImGui.Button(pvPEvent.Name))
            {
                PluginServices.Announcer.ReceivePvPEvent(true, pvPEvent);
                PluginServices.Announcer.ClearQueue();
            }

            if (i % 4 != 0)
            {
                ImGui.SameLine();
            }

            i++;
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
    
    public void Dispose()
    {
    }
}