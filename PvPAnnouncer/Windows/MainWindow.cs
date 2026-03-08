using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;

namespace PvPAnnouncer.Windows;

public class MainWindow : Window, IDisposable
{
    public MainWindow() : base(
        "PvPAnnouncer", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
            MaximumSize = new Vector2(450, 225)
        };
    }

    public override void Draw()
    {
        if (ImGui.Button("Open Plugin Configuration"))
        {
            PluginServices.ConfigWindow.Toggle();
        }

        ImGui.TextWrapped(
            "Welcome to PvP Announcer! This plugin will take many NPC's and put them into your PvP match! " +
            "\nPlease contact .spider in the Dalamud Discord for feedback/suggestions!" +
            "\nView the config with /pvpannouncer");
        ImGui.Spacing();
        ImGui.Text("Attributions");
        ImGui.BulletText("DeathRecap, VFXEditor, OofPlugin");
        ImGui.BulletText("Mutant Standard for the plugin icon (CC BY-NC-SA) - https://mutant.tech");
        ImGui.Spacing();
    }

    public void Dispose()
    {
    }
}