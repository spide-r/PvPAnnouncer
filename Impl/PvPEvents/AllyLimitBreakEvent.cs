using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyLimitBreakEvent: IPvPActorActionEvent
{
    public AllyLimitBreakEvent()
    {
        InvokeRule = ShouldInvoke;
    }

    private bool ShouldInvoke(IPacket arg)
    {
        throw new NotImplementedException();
    }

    public string[]? SoundPaths { get; init; } = [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
    public Func<IPacket, bool> InvokeRule { get; init; }
}
