using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchStartEvent: IPvPMatchEvent
{
    public string[]? SoundPaths { get; init; } = [SendingCameras, UpstartBegins];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
}
