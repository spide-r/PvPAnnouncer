using System;
using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPEvent(string name = "PvP Event")
{
    public abstract List<string> SoundPaths();

    public abstract Dictionary<uint, List<string>> PersonalizedSoundPaths();
    public abstract bool InvokeRule(IMessage message);
    public string Name = name;
}
