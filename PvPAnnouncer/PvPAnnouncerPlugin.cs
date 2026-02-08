using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using PvPAnnouncer.Data;
using PvPAnnouncer.Windows;

namespace PvPAnnouncer
{
    //todo option to hide the flag upon victory/loss so that the blorbo can be seen 
    //todo pull the old todo's from scionlines when you want to add more voicelines
    
    //todo there is so much differing naming for everything, id/internalname shout/annnounce fix it!!!
    public sealed class PvPAnnouncerPlugin: IDalamudPlugin
    {
        private WindowSystem WindowSystem = new("PvPAnnouncer");
        private ConfigWindow ConfigWindow { get; init; }
        private MainWindow MainWindow { get; init; }
        private DevWindow DevWindow { get; init; }
        private VoiceLineTesterWindow VoiceLineTesterWindow { get; init; }


        public PvPAnnouncerPlugin(IDalamudPluginInterface pluginInterface)
        {
            PluginServices.Initialize(pluginInterface, WindowSystem);
            LoadCommands();
            ConfigWindow = new ConfigWindow(PluginServices.ShoutcastRepository, PluginServices.Config, PluginServices.EventShoutcastMapping);
            MainWindow = new MainWindow();
            DevWindow = new DevWindow();
            VoiceLineTesterWindow = new VoiceLineTesterWindow(PluginServices.ShoutcastRepository);
            WindowSystem.AddWindow(ConfigWindow);
            WindowSystem.AddWindow(MainWindow);
            WindowSystem.AddWindow(DevWindow);
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
                    "PvPAnnouncer now has every scion as an announcer! Yes! You read that right! ALL THE SCIONS and MORE!\n" +
                    "Tell your local scion enjoyer that their fav is now in PvP ;D \n" +
                    "You can also enable the character portrait when an announcer speaks.", "PvPAnnouncer", 15);
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
            DevWindow.Toggle();
            #endif
            ConfigWindow.Toggle();
        }
        
        private void ToggleMainUI()
        {
            PluginUpdateMessage();
            MainWindow.Toggle();
        }
        
        private void ToggleTesterWindow()
        {
            VoiceLineTesterWindow.Toggle();
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

