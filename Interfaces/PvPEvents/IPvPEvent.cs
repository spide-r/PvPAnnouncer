using System;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public interface IPvPEvent
{
    string[]? SoundPaths { get; init; }
    Func<IPacket, bool> InvokeRule { get; init; }
}
