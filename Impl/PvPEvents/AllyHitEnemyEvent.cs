using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;


public class AllyHitEnemyEvent : IPvPActorActionEvent
{
    //this is too generic
    public AllyHitEnemyEvent(ulong playerId, ulong playerTarget, uint actionId)
    {
        PlayerId = playerId;
        PlayerTarget = playerTarget;
        ActionId = actionId;
        InvokeRule = InvokeRuleFunc;
    }

    private bool InvokeRuleFunc(IPacket arg) 
    {
        return false;
    }

    public string[]? SoundPaths { get; init; } = [StruckSquare, WhatAClash, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; }
    public ulong PlayerId { get; init; }
    public ulong? PlayerTarget { get; init; }
    public uint ActionId { get; init; }
}
