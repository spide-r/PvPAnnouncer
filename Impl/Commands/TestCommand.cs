using Dalamud.Game.Command;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Commands;

public class TestCommand: Command
{
    public string Name { get; init; }
    public string HelpText { get; init; }
    public CommandInfo CommandInfo { get; init; }

    public TestCommand()
    {
        Name = "testcommand";
        HelpText = "Test command";
        CommandInfo = new CommandInfo(OnCommand)
        {
            HelpMessage = HelpText
        };
    }
    
    public new void OnCommand(string command, string args)
    {
        PluginServices.PluginLog.Info("Hello World!");
    }
}