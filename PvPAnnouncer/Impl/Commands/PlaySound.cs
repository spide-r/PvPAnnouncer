using System;
using Dalamud.Game.Command;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Commands;

public class PlaySound: Command
{
    public PlaySound()
    {
        Name = "ps";
        HelpText = "Plays a sound. Remove me!";
        Info = new CommandInfo(OnCommand)
        {
            HelpMessage = HelpText
        };
    }

    public override void OnCommand(string command, string args)
    {
        try
        {
            PluginServices.Announcer.PlaySound(args);
            PluginServices.Announcer.SendBattleTalk(args);
        }
        catch (Exception e)
        {
            PluginServices.PluginLog.Error("oops!", e);
        }


    }
    

}