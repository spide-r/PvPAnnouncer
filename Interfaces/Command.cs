using System;
using Dalamud.Game.Command;
using PvPAnnouncer.impl.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public abstract class Command
{

    public string Name { get; init; } = "default";
    protected string HelpText { get; init; } = "Default Help";

    public abstract void OnCommand(string command, string args);

    public string GetFullCommandName()
    {
        return "/" + Name;
    }

    public CommandInfo? Info { get; protected init; }
    
    
}