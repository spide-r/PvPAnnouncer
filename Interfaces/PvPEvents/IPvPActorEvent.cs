using System;

namespace PvPAnnouncer.Interfaces.PvPEvents;

public interface IPvPActorEvent: IPvPEvent
{
    ulong PlayerId { get; init;  }
    ulong? PlayerTarget { get; init;  }

}
