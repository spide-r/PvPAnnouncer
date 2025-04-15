﻿using Dalamud.Game.Command;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl.Commands;

public class TestCommand: Command
{

    public TestCommand()
    {
        Name = "testcommand";
        HelpText = "Test command";
        Info = new CommandInfo(OnCommand)
        {
            HelpMessage = HelpText
        };
    }
    
    public override void OnCommand(string command, string args)
    {
        PluginServices.PluginLog.Info("Hello World!");
    }
}