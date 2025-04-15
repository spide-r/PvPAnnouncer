using System;
using Dalamud.Game.Command;

namespace PvPAnnouncer.Interfaces;

public abstract class Command
{ 
    string Name { get; init; }
    string HelpText { get; init; }

    public void OnCommand(string command, string args)
    {
        
    }

    public string GetFullCommandName()
    {
        return "/" + Name;
    }

    CommandInfo CommandInfo { get; init; }
    
    
}