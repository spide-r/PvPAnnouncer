using System;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitUnderGuardEvent: IPvPActorActionEvent
{
    public AllyHitUnderGuardEvent(Func<IPacket, bool> invokeRule)
    {
        InvokeRule = ShouldInvoke;
    }

    public string[]? SoundPaths { get; init; } = [ClearlyAnticipated, FeltThatOneStillStanding, SawThroughIt, IroncladDefense, WhatAClash, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; } 

    private bool ShouldInvoke(IPacket arg)
    {
        if (arg is ActionEffectPacket)
        {
            ActionEffectPacket packet = (ActionEffectPacket)arg;

            uint[] ids = packet.GetTargetIds();
            
        }

        return false;
    }
}
