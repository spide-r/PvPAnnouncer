using Dalamud.Game.Command;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Commands;

public class Config: Command
{
    public Config()
    {
        Name = "pvpannouncer";
        HelpText = "Open The announcer config.";
        Info = new CommandInfo(OnCommand)
        {
            HelpMessage = HelpText
        };
    }


    public override void OnCommand(string command, string args)
    {
        PluginServices.PluginLog.Info("Opening config!");
        //todo: maybe make the command stuff clearer
    }
}