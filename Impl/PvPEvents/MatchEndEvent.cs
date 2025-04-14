﻿using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchEndEvent: IPvPMatchEvent
{
    public string[]? SoundPaths { get; init; } = [AllOverUntilNextTime, BattleElectrifying];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
}
