using System;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyPulledDrkEvent: IPvPActorActionEvent
{
    
    public AllyPulledDrkEvent()
    {
        InvokeRule = ShouldInvoke;
    }
    public string[]? SoundPaths { get; init; } = [SuckedIn, WhatAClash, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; }
    
    private bool ShouldInvoke(IPacket packet)
    {
        if (packet is ActionEffectPacket)
        {
            ActionEffectPacket aa = (ActionEffectPacket) packet;

            ulong actionId = aa.ActionId;
            return actionId == 0; //todo drk suck id
      
        }
        return false;
    }
}
