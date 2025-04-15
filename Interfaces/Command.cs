using System;
using Dalamud.Game.Command;

namespace PvPAnnouncer.Interfaces;

public abstract class Command
{

    public string Name { get; init; }
    public string HelpText { get; init; }

    public abstract void OnCommand(string command, string args);

    public string GetFullCommandName()
    {
        return "/" + Name;
    }

    public CommandInfo Info { get; init; }
    
    
}