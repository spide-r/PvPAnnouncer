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
            Service.Initialize(pluginInterface);
            PluginInterface = pluginInterface;
            Service.CommandManager.AddHandler(MainCommand, new CommandInfo(OnCommand)
            {
                HelpMessage = "To Be Replaced!"
            });


        }
        public void Dispose()
        {
            Service.CommandManager.RemoveHandler(MainCommand);
        }
        private void OnCommand(string command, string args)
        {
            Service.PluginLog.Info("Hello World!");
        }
    }
}

