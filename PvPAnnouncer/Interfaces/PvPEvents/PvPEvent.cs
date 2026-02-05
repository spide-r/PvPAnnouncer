using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPEvent(string name = "PvP Event")
{
    [Obsolete]
    public abstract List<Shoutcast> SoundPaths();

    public abstract bool InvokeRule(IMessage message);
    public string Name = name;
    public string InternalName = "InternalName";
}
