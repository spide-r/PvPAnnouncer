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
        Name = "Battle High V / Flying High Gained";
        InternalName = "MaxBattleFeverEvent";
    }

    public override List<string> SoundPaths()
    {
        return [WhatPower];
    }
    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>
        {
            {Personalization.MascPronouns, [RoofCavedSuchDevastation]}, 
            { Personalization.FemPronouns, [HerCharmsNotDeniedFem, GatheringAetherFem, UnusedOhMercyFem]},
            {Personalization.DancingGreen, [DGFeverPich]}
        };
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is BattleHighMessage bhm)
        {
            return bhm.Level == 5;
        } 
        return message is SoaringMessage;
    }
}