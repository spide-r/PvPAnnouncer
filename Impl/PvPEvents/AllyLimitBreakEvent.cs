using System;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
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

    private bool ShouldInvoke(IPacket packet)
    {
        if (packet is ActionEffectPacket)
        {
            ActionEffectPacket pp = (ActionEffectPacket)packet;
            if (PvPAnnouncerPlugin.PvPMatchManager!.IsMonitoredUser(pp.SourceId))
            {
                return ActionIds.IsLimitBreak(pp.ActionId);
            }
        }
        return false;    
    }

    public string[]? SoundPaths { get; init; } = [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
    public Func<IPacket, bool> InvokeRule { get; init; }
}
