using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent: PvPActorEvent
{
    public AllyResurrectEvent()
    {
        Name = "Resurrection";
        InternalName = "AllyResurrectEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [BackUpGrit, BackOnFeet, RisesAgain, WhatFightingSpirit, BackInAction, BattleNotOverDontDespair];
    }

    public override Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<BattleTalk>>
        {
            {Personalization.President, [ItsAliveLindwurm, ContinuesToMultiply, LWCompletelyHealed, SomethingRevolting]}
        };    
    }

    public override bool InvokeRule(IMessage m)
    {
        return m is UserResurrectedMessage;
    }

}
