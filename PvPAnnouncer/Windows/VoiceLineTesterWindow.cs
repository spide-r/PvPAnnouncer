using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Windows;

public class VoiceLineTesterWindow: Window, IDisposable
{
    //todo create
    private List<BattleTalk> _allBattleTalks;
    public VoiceLineTesterWindow() : base(
        "Voice Line Tester window", ImGuiWindowFlags.AlwaysVerticalScrollbar)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        _allBattleTalks = new List<BattleTalk>(InternalConstants.GetBattleTalkList());

    }

    public void Dispose()
    {
        
    }

    public override void Draw()
    {
        ImGui.Text("How did we get here?");
        ImGui.Separator();
        //todo sort by name
        // id(voLine/path/)- name - text - button
        ImGui.BeginTable("Voicelines", 4, ImGuiTableFlags.Borders | ImGuiTableFlags.Resizable | ImGuiTableFlags.Reorderable);
        ImGui.TableSetupColumn("Voiceline Path");
        ImGui.TableSetupColumn("Name");
        ImGui.TableSetupColumn("Text");
        ImGui.TableSetupColumn("Button");
        ImGui.TableHeadersRow();
        foreach (var bt in _allBattleTalks)
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.Text(bt.Path);
            ImGui.TableNextColumn();
            ImGui.Text(bt.Name);
            ImGui.TableNextColumn();
            ImGui.Text(bt.Text);
            ImGui.TableNextColumn();
            
            if (ImGui.Button("Play###" + bt.Path))
            {
                PluginServices.Announcer.SendBattleTalk(bt);
                PluginServices.Announcer.PlaySound(bt.Path + PluginServices.Config.Language +".scd");
            }
        }
        ImGui.EndTable();
    }
}