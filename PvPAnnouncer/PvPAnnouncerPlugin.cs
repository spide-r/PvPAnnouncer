using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using PvPAnnouncer.Data;

namespace PvPAnnouncer
{
    //there is so much differing naming for everything, id/internalname/actionid shout/annnounce fix it!!!

    //todo make sure that all voicelines are transcribed w/ npcyell or whatever - the only lines that you should have an english-only thing should be the mahjong stuff + m12 encrypted
    //todo vuln stack event


    public sealed class PvPAnnouncerPlugin : IDalamudPlugin
    {
        private WindowSystem WindowSystem = new("PvPAnnouncer");

        public PvPAnnouncerPlugin(IDalamudPluginInterface pluginInterface)
        {
            PluginServices.Initialize(pluginInterface, WindowSystem);
            LoadCommands();

            pluginInterface.UiBuilder.Draw += DrawUi;
            pluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
            pluginInterface.UiBuilder.OpenConfigUi += ToggleConfigWindow;
            PluginServices.ClientState.Login += PluginUpdateMessage;
            PluginUpdateMessage();
        }

        public static void PluginUpdateMessage()
        {
            if (PluginServices.Config.ShowNotification)
            {
                if (PluginServices.ClientState.IsLoggedIn == false)
                {
                    return;
                }

                PluginServices.ChatGui.Print(
                    "Happy 7.5! Zero's Mahjong voicelines (plus some extras) have been added Enable them in the plugin config!",
                    "PvPAnnouncer", 15);
                PluginServices.Config.ShowNotification = false;
                PluginServices.Config.Save();
            }
        }

        private void DrawUi()
        {
            WindowSystem.Draw();
        }

        private void OnCommand(string command, string args)
        {
            ToggleConfigWindow();
        }

        private void OnToggleMuteCommand(string command, string args)
        {
            PluginServices.SoundManager.ToggleMute();
            string un = "";
            if (!PluginServices.Config.Muted)
            {
                un = "un-";
            }

            PluginServices.ChatGui.Print("Announcer Has been " + un + "muted!", InternalConstants.MessageTag);
        }

        private void ToggleConfigWindow()
        {
#if DEBUG
            PluginServices.DevWindow.Toggle();
#endif
            PluginServices.ConfigWindow.Toggle();
        }

        private void ToggleMainUI()
        {
            PluginUpdateMessage();
            PluginServices.MainWindow.Toggle();
        }

        private void LoadCommands()
        {
            PluginServices.CommandManager.AddHandler("/pvpannouncer", new CommandInfo(OnCommand)
            {
                HelpMessage = "Open the Config Window",
            });
            PluginServices.CommandManager.AddHandler("/muteannouncer", new CommandInfo(OnToggleMuteCommand)
            {
                HelpMessage = "Toggle Mute Announcer"
            });
        }

        private void UnloadCommands()
        {
            PluginServices.CommandManager.RemoveHandler("/pvpannouncer");
            PluginServices.CommandManager.RemoveHandler("/muteannouncer");
        }

        public void Dispose()
        {
            PluginServices.ClientState.Login -= PluginUpdateMessage;
            PluginServices.DalamudPluginInterface.UiBuilder.Draw -= DrawUi;
            PluginServices.DalamudPluginInterface.UiBuilder.OpenMainUi -= ToggleMainUI;
            PluginServices.DalamudPluginInterface.UiBuilder.OpenConfigUi -= ToggleConfigWindow;
            PluginServices.PvPEventHooksPublisher.Dispose();
            PluginServices.PlayerStateTracker.Dispose();
            PluginServices.PvPMatchManager.Dispose();
            UnloadCommands();
        }
    }
}