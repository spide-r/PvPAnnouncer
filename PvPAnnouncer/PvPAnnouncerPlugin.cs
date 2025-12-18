using System.Collections.Generic;
using System.Threading.Tasks;
using Dalamud.Game.Command;
using Dalamud.Interface.Style;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Impl.Commands;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Windows;

namespace PvPAnnouncer
{
    public sealed class PvPAnnouncerPlugin: IDalamudPlugin
    {
        private WindowSystem WindowSystem = new("PvPAnnouncer");
        private ConfigWindow ConfigWindow { get; init; }
        private MainWindow MainWindow { get; init; }

        private readonly Command[] _commands = [ new MuteAnnouncer()];

        public PvPAnnouncerPlugin(IDalamudPluginInterface pluginInterface)
        {
#if DEBUG
            _commands = [new PlaySound(), new MuteAnnouncer()];
#endif
            PluginServices.Initialize(pluginInterface);
            LoadCommands();
            ConfigWindow = new ConfigWindow();
            MainWindow = new MainWindow();
            WindowSystem.AddWindow(ConfigWindow);
            WindowSystem.AddWindow(MainWindow);
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
                    "Welcome back! PvPAnnouncer has been updated. Metem's 7.4 voicelines have been added but are disabled by default. " +
                    "Check the settings to enable them. New Events with the new Arcadion fighters " +
                    "will be added once the socially acceptable embargo of 2 weeks has elapsed.", "PvPAnnouncer", 15);
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

        private void ToggleConfigWindow()
        {
            ConfigWindow.Toggle();
        }
        
        private void ToggleMainUI()
        {
            PluginUpdateMessage();
            MainWindow.Toggle();
        }

        private void LoadCommands()
        {
            Config c = new Config();
            PluginServices.CommandManager.AddHandler(c.GetFullCommandName(), new CommandInfo(OnCommand)
            {
                HelpMessage = "Open the Config Window",
            });
            foreach (var command in _commands)
            {
                PluginServices.CommandManager.AddHandler(command.GetFullCommandName(), command.Info!);
            }
        }

        private void UnloadCommands()
        {
            Config c = new Config();
            PluginServices.CommandManager.RemoveHandler(c.GetFullCommandName());
            foreach (var command in _commands)
            {
                PluginServices.CommandManager.RemoveHandler(command.GetFullCommandName());

            }
            
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

