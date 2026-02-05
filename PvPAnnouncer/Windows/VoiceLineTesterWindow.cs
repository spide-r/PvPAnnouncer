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
    private List<Shoutcast> _allBattleTalks;
    public VoiceLineTesterWindow() : base(
        "Voice Line Tester window", ImGuiWindowFlags.AlwaysVerticalScrollbar)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        _allBattleTalks = new List<Shoutcast>(InternalConstants.GetBattleTalkList());

    }

    public void Dispose()
    {
        
    }

    private int _filterIndex = 0;
    private string[] toFilter = {"All", "Zenos", "Alphinaud", "Alisaie", "Thancred", "Urianger", "Y'shtola", 
        "Estinien", "G'raha Tia", "Krile", "Wuk Lamat", "Koana", "Bakool Ja Ja", "Erenville", "Metem", "Referee"};
    public override void Draw()
    {
        ImGui.Text("How did we get here?");
        ImGui.Separator();

        if (ImGui.BeginCombo("Announcer Filter", toFilter[_filterIndex]))
        {
            for (var i = 0; i < toFilter.Length; i++)
            {
                bool selected = (_filterIndex == i);
                if (ImGui.Selectable(toFilter[i], selected))
                {
                    _filterIndex = i;
                }
            }   
            ImGui.EndCombo();
        }
     
        
        // id(voLine/path/)- name - text - button
        if (ImGui.BeginTable("Voicelines", 4,
                ImGuiTableFlags.Borders | ImGuiTableFlags.Resizable | ImGuiTableFlags.Reorderable))
        {
            ImGui.TableSetupColumn("Voiceline Path");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Text");
            ImGui.TableSetupColumn("Button");
            ImGui.TableHeadersRow();
            foreach (var bt in _allBattleTalks)
            {
                if (_filterIndex != 0)
                {
                    if (!bt.ShouterName.Equals(toFilter[_filterIndex]))
                    {
                        continue;
                    }
                }
                ImGui.TableNextRow();
                ImGui.TableNextColumn();
                ImGui.Text(bt.Path);
                ImGui.TableNextColumn();
                ImGui.Text(bt.ShouterName);
                ImGui.TableNextColumn();
                ImGui.Text(bt.Text);
                ImGui.TableNextColumn();
            
                if (ImGui.Button("Play###" + bt.Path))
                {
                    PluginServices.Announcer.SendBattleTalk(bt);
                    PluginServices.Announcer.PlaySound(bt.Path + "_" + PluginServices.Config.Language +".scd");
                }
            }
            ImGui.EndTable();
        }

    }
}