using Dalamud.Game.Command;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Commands;

public class MuteAnnouncer: Command
{
    public MuteAnnouncer()
    {
        Name = "muteannouncer";
        HelpText = "Toggle Mute Announcer";
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
        PluginServices.ChatGui.Print("Announcer Has been " + un + "muted!", InternalConstants.MessageTag, InternalConstants.MessageColor);
    }
}