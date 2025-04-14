using System;
using Dalamud.Game.Command;

namespace PvPAnnouncer.Interfaces;

public interface ICommand
{ 
    String Name { get; init; }
    String HelpText { get; init; }

    public void OnCommand(string command, string args);

    CommandInfo CommandInfo { get; init; }
    
    
}