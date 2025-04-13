using Dalamud.Game.Command;
using Dalamud.Plugin;

namespace PvPAnnouncer
{
    public sealed class PvPAnnouncer: IDalamudPlugin
    {
        private const string MainCommand = "/ppa";
        private IDalamudPluginInterface PluginInterface { get; init; }
        

        public PvPAnnouncer(IDalamudPluginInterface pluginInterface)
        {
            PluginServices.Initialize(pluginInterface);
            PluginInterface = pluginInterface;
            PluginServices.CommandManager.AddHandler(MainCommand, new CommandInfo(OnCommand)
            {
                HelpMessage = "To Be Replaced!"
            });


        }
        public void Dispose()
        {
            PluginServices.CommandManager.RemoveHandler(MainCommand);
        }
        private void OnCommand(string command, string args)
        {
            PluginServices.PluginLog.Info("Hello World!");
        }
    }
}

