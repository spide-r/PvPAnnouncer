using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MechKilledEvent: IPvPEvent
{
    public string[]? SoundPaths { get; init; } = [ColossalThingSwordMasc];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
}
