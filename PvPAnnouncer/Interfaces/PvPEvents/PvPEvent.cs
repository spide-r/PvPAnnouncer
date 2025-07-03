using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPEvent(string name = "PvP Event")
{
    public abstract List<string> SoundPaths();

    public abstract Dictionary<Personalization, List<string>> PersonalizedSoundPaths();
    public abstract bool InvokeRule(IMessage message);
    public string Name = name;
    public string InternalName = "InternalName";
}
