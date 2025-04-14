using Dalamud.Game.Command;
using PvPAnnouncer.Data;
using ICommand = PvPAnnouncer.Interfaces.ICommand;

namespace PvPAnnouncer.Impl.Commands;

public class PlaySound: ICommand
{
    public string Name { get; init; }
    public string HelpText  { get; init; }
    public CommandInfo CommandInfo { get; init; }

    public PlaySound()
    {
        Name = "/playsound";
        HelpText = "Plays a sound.";
        CommandInfo = new CommandInfo(OnCommand)
        {
            HelpMessage = HelpText
        };
    }

    public void OnCommand(string command, string args)
    {
        PvPAnnouncerPlugin.SoundManager?.PlaySound(AnnouncerLines.GetPath(AnnouncerLines.IroncladDefense));

    }
    

}