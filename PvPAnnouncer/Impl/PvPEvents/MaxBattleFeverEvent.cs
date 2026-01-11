using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class MaxBattleFeverEvent: PvPEvent
{
    public MaxBattleFeverEvent()
    {
        Name = "Gaining Battle High V / Flying High";
        InternalName = "MaxBattleFeverEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [WhatPower, RainOfDeath];
    }
    public override Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<BattleTalk>>
        {
            {Personalization.MascPronouns, [RoofCavedSuchDevastation]}, 
            { Personalization.FemPronouns, [HerCharmsNotDeniedFem, GatheringAetherFem, UnusedOhMercyFem]},
            {Personalization.DancingGreen, [DGFeverPich]},
        };
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is BattleHighMessage bhm)
        {
            return bhm.Level == 5;
        } 
        return message is FlyingHighMessage;
    }
}