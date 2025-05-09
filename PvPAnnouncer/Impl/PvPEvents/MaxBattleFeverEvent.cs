using System.Collections.Generic;
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
    }

    public override List<string> SoundPaths()
    {
        return [WhatPower];
    }

    public override List<string> SoundPathsMasc()
    {
        return [AssaultedRefMasc, RoofCavedSuchDevastation];
    }

    public override List<string> SoundPathsFem()
    {
        return [HerCharmsNotDeniedFem, GatheringAetherFem];
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