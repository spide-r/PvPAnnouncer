using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace PvPAnnouncer.Windows;

public class MainWindow: Window, IDisposable
{
    public MainWindow() : base(
        "PvPAnnouncer", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 200),
            MaximumSize = new Vector2(450, 200)
        };

    }

    public override void Draw()
    {
        ImGui.TextWrapped("Welcome to PvP Announcer! This plugin will take Metem from the Arcadion and put him into your PvP match! " +
                          "\nPlease provide lots of feedback. " +
                          "\nYou can use the feedback button or contact .spider in the Dalamud Discord! View the config with /pvpannouncer");
        ImGui.Spacing();
        ImGui.Text("Attributions");
        ImGui.BulletText("DeathRecap, VFXEditor");
        ImGui.BulletText("Mutant Standard for the plugin icon (CC BY-NC-SA) - https://mutant.tech");
    }

    public void Dispose()
    {
    }
}