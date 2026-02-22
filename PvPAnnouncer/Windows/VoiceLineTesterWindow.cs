using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Windows;

public class VoiceLineTesterWindow : Window, IDisposable
{
    private readonly List<Shoutcast> _allBattleTalks;
    private List<string> toFilter = ["All"];

    public VoiceLineTesterWindow(IShoutcastRepository shoutcastRepository) : base(
        "Voice Line Tester window", ImGuiWindowFlags.AlwaysVerticalScrollbar)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        foreach (var s in PluginServices.CasterRepository.GetAttributeList()) toFilter.Add(s);


        _allBattleTalks = new List<Shoutcast>(shoutcastRepository.GetShoutcasts());
    }

    public void Dispose()
    {
    }

    private int _filterIndex = 0;

    public override void Draw()
    {
        ImGui.Text("How did we get here?");
        ImGui.Separator();

        if (ImGui.BeginCombo("Announcer Filter", toFilter[_filterIndex]))
        {
            for (var i = 0; i < toFilter.Count; i++)
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
                    if (!bt.Shoutcaster.Equals(toFilter[_filterIndex]))
                    {
                        continue;
                    }
                }

                ImGui.TableNextRow();
                ImGui.TableNextColumn();
                ImGui.Text(bt.SoundPath);
                ImGui.TableNextColumn();
                ImGui.Text(bt.Shoutcaster);
                ImGui.TableNextColumn();
                var text = bt.GetTranscriptionWithGender(PluginServices.Config.Language,
                    PluginServices.Config.WantsAttribute("Feminine Pronouns"), PluginServices.SeStringEvaluator);
                if (text.Equals(""))
                {
                    text = "Untranslated Text! Contact the PvPAnnouncer developer if you wish to contribute!";
                }

                ImGui.Text(text);
                ImGui.TableNextColumn();

                if (ImGui.Button("Play###" + bt.SoundPath))
                {
                    PluginServices.Announcer.SendBattleTalk(bt);
                    PluginServices.Announcer.PlaySound(bt.GetShoutcastSoundPathWithGenderAndLang(
                        PluginServices.Config.Language, PluginServices.Config.WantsAttribute("Feminine Pronouns")));
                }
            }

            ImGui.EndTable();
        }
    }
}