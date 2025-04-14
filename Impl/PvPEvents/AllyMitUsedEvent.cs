using System;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyMitUsedEvent: IPvPActorActionEvent
{
    public AllyMitUsedEvent()
    {
        InvokeRule = ShouldInvoke;
    }

    public string[]? SoundPaths { get; init; } = [IroncladDefense, WhatAClash, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; }
    public ulong? PlayerTarget { get; init; }

    private bool ShouldInvoke(IPacket packet)
    {
        if (packet is ActionEffectPacket)
        {
            ActionEffectPacket aa = (ActionEffectPacket) packet;

            ulong actionId = aa.ActionId;
            //todo add mits
        }
        return false;
    }
}
