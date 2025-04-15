using System;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;


public class AllyHitEnemyHardEvent : IPvPActorActionEvent
{
    public AllyHitEnemyHardEvent()
    {
        InvokeRule = InvokeRuleFunc;
    }

    private bool InvokeRuleFunc(IPacket packet) 
    {
        if (packet is ActionEffectPacket)
        {
            ActionEffectPacket pp = (ActionEffectPacket)packet;
            if (PvPAnnouncerPlugin.PvPMatchManager!.IsMonitoredUser(pp.SourceId))
            {
                return pp.CritsOrDirectHits() || ActionIds.IsLimitBreak(pp.ActionId) || ActionIds.IsBigHit(pp.ActionId);
            }
        }
        return false;
    }

    public string[]? SoundPaths { get; init; } = [StruckSquare, WhatAClash, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; }

}
