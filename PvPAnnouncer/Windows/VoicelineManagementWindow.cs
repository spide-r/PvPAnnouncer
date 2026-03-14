using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Windows;

public class VoicelineManagementWindow : Window, IDisposable
{
    //option to delete if custom voiceline, can mute/unmute here
    private int _currentEventShoutSelection;
    private int _otherShouts;
    private int _shoutcasterSelection;


    public VoicelineManagementWindow() : base(
        "Voiceline Management")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225)
        };
    }

    public void Dispose()
    {
    }


    public override void Draw()
    {
        ImGui.Text("Voiceline Selector:");
        ImGui.Text("Character Filter");
        var shoutcasterSelection = _shoutcasterSelection;
        var shoutcasters = PluginServices.ShoutcastRepository.GetShoutcasters().ToList();

        if (ImGui.Combo("###ShoutcasterFilter", ref shoutcasterSelection, shoutcasters))
            _shoutcasterSelection = shoutcasterSelection;

        ImGui.Separator();

        ImGui.TextWrapped(
            "This Page will let you mute voicelines that you simply do not wish to hear at all. If a voiceline custom one, you have the option to delete it as well.");


        var currentShouts = new List<string>();
        var mutedShouts = new List<string>();

        var shoutcaster = "";
        try
        {
            shoutcaster = shoutcasters[shoutcasterSelection];
        }
        catch (ArgumentOutOfRangeException)
        {
        }

        foreach (var shoutcast in PluginServices.ShoutcastRepository.GetShoutcasts())
            if (shoutcast.Shoutcaster.Equals(shoutcaster))
            {
                if (PluginServices.Config.MutedShouts.Contains(shoutcast.Id))
                    mutedShouts.Add(shoutcast.Id);
                else
                    currentShouts.Add(shoutcast.Id);
            }

        ImGui.TextWrapped("Current Voicelines:");

        var currentShoutSelection = _currentEventShoutSelection;
        if (ImGui.ListBox("###CurrentShoutsNotMuted", ref currentShoutSelection, currentShouts))
            _currentEventShoutSelection = currentShoutSelection;

        var selection = "";
        try
        {
            selection = currentShouts[currentShoutSelection];
        }
        catch (ArgumentOutOfRangeException)
        {
        }

        if (ImGui.Button("Test Selected Shout###CurrentShoutTest"))
            try
            {
                var sc = PluginServices.ShoutcastRepository.GetShoutcast(selection);
                if (sc == null)
                {
                    PluginServices.PluginLog.Warning($"{selection} not found");
                    return;
                }

                PluginServices.Announcer.PlayAndSendBattleTalkForTesting(sc);
            }
            catch (ArgumentOutOfRangeException)
            {
            }

        ImGui.SameLine();
        if (ImGui.Button("Mute Voiceline"))
            try
            {
                var shoutToMute = selection;
                if (!shoutToMute.IsNullOrEmpty())
                    if (!PluginServices.Config.MutedShouts.Contains(shoutToMute))
                    {
                        PluginServices.Config.MutedShouts.Add(shoutToMute);
                        PluginServices.Config.Save();
                    }
            }
            catch (ArgumentOutOfRangeException)
            {
            }

        if (PluginServices.Config.CustomShoutcasts.ContainsKey(selection))
        {
            if (ImGui.Button("Edit Voiceline"))
            {
                var sc = PluginServices.ShoutcastRepository.GetShoutcast(selection);
                if (sc != null)
                {
                    PluginServices.VoicelineCreationWindow.Edit(sc);
                    PluginServices.VoicelineCreationWindow.IsOpen = true;
                }
                else
                {
                    PluginServices.PluginLog.Error("Invalid sc id of " + selection);
                }
            }

            if (ImguiTools.CtrlShiftButton("Delete Custom Voiceline"))
                PluginServices.ConfigManager.DeleteAndDeregisterShoutcast(selection);
        }


        ImGui.Text("Muted Voicelines:");

        var mutedSelection = _otherShouts;

        if (ImGui.ListBox("###OtherShouts", ref mutedSelection, mutedShouts)) _otherShouts = mutedSelection;

        if (ImGui.Button("Test Selected Shout###OtherShoutTest"))
            try
            {
                var sc = PluginServices.ShoutcastRepository.GetShoutcast(mutedShouts[mutedSelection]);
                if (sc == null)
                {
                    PluginServices.PluginLog.Warning($"{mutedShouts[mutedSelection]} not found");
                    return;
                }

                PluginServices.Announcer.PlayAndSendBattleTalkForTesting(sc);
            }
            catch (ArgumentOutOfRangeException)
            {
            }

        ImGui.SameLine();
        if (ImGui.Button("Unmute Voiceline"))
            try
            {
                var shout = mutedShouts[mutedSelection];
                PluginServices.Config.MutedShouts.Remove(shout);
                PluginServices.Config.Save();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
    }
}