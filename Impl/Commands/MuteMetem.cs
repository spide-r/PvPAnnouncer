using Dalamud.Game.Command;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Commands;

public class MuteMetem: Command
{
    public MuteMetem()
    {
        Name = "mutemetem";
        HelpText = "Toggle Mute metem";
        Info = new CommandInfo(OnCommand)
        {
            HelpMessage = HelpText
        };
    }
    public override void OnCommand(string command, string args)
    {
        PluginServices.SoundManager.ToggleMute();
        string un = "";
        if (!PluginServices.Config.Muted)
        {
            un = "un-";
        }
        PluginServices.ChatGui.Print("Metem Has been " + un + "muted!", InternalConstants.MessageTag, InternalConstants.MessageColor);
    }
}