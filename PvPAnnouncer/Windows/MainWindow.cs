using System;
using System.Numerics;
using Dalamud.Interface.ImGuiNotification;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.UI;
using Dalamud.Bindings.ImGui;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Windows;

public class MainWindow: Window, IDisposable
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
        if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
        {
            ImGui.Separator();
            ImGui.TextWrapped("Dawntrail is not installed! This plugin needs the expansion installed in order to work!");
            ImGui.Separator();
        }
        ImGui.TextWrapped("Welcome to PvP Announcer! This plugin will take Metem from the Arcadion and put him into your PvP match! " +
                          "\nPlease contact .spider in the Dalamud Discord for feedback/suggestions!" +
                          "\nView the config with /pvpannouncer");
        ImGui.Spacing();
        ImGui.Text("Attributions");
        ImGui.BulletText("DeathRecap, VFXEditor, OofPlugin");
        ImGui.BulletText("Mutant Standard for the plugin icon (CC BY-NC-SA) - https://mutant.tech");
        ImGui.Spacing();
        if (ImGui.Button("Test The Announcer"))
        {
            PluginServices.ChatGui.Print("Playing Voiceline!", InternalConstants.MessageTag);
            var line = AnnouncerLines.GetRandomAnnouncement();
            PluginServices.SoundManager.PlaySound(AnnouncerLines.GetPath(line));
            PluginServices.PlayerStateTracker.CheckSoundState();
            SendBattleTalk(line);
            if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
            {
                Notification n = new Notification();
                n.Title = "Dawntrail Not installed!";
                n.Type = NotificationType.Error;
                n.Minimized = false;
                n.MinimizedText = "Dawntrail is not installed!";
                n.Content = "You must install Dawntrail for this plugin to work!";
                PluginServices.NotificationManager.AddNotification(n);
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