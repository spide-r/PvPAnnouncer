using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public abstract class PvPEvent(string name = "PvP Event")
{
    public abstract List<BattleTalk> SoundPaths();

    public abstract Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths();
    public abstract bool InvokeRule(IMessage message);
    public string Name = name;
    public string InternalName = "InternalName";
}
