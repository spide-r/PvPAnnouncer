using Dalamud.Game.Command;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Commands;

public class PlaySound: Command
{
    public PlaySound()
    {
        Name = "playsound";
        HelpText = "Plays a sound. Remove me!";
        Info = new CommandInfo(OnCommand)
        {
            HelpMessage = HelpText
        };
    }

    public override void OnCommand(string command, string args)
    {
        PluginServices.Announcer.PlaySound(AnnouncerLines.GetPath(args));
        PluginServices.Announcer.SendBattleTalk(args);

    }
    

}