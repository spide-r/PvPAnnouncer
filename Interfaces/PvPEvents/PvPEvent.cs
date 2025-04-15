using System;
using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPEvent(string name = "PvP Event")
{
    public abstract List<string> SoundPaths();
    public abstract List<string> SoundPathsMasc();
    public abstract List<string> SoundPathsFem();
    public abstract bool InvokeRule(IPacket packet);
    public string Name = name;
}
