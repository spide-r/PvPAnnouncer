using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
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

        private readonly Command[] _commands = [/*new PlaySound(),*/ new MuteAnnouncer()/*, new TestCommand()*/];

        public PvPAnnouncerPlugin(IDalamudPluginInterface pluginInterface)
        {
            PluginServices.Initialize(pluginInterface);
            LoadCommands();
            ConfigWindow = new ConfigWindow();
            MainWindow = new MainWindow();
            WindowSystem.AddWindow(ConfigWindow);
            WindowSystem.AddWindow(MainWindow);
            pluginInterface.UiBuilder.Draw += DrawUi;
            pluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
            pluginInterface.UiBuilder.OpenConfigUi += ToggleConfigWindow;
            
          
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
            UnloadCommands();
        }
    }
}

