using System.Collections.Generic;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;

namespace PvPAnnouncer.impl.PvPEvents;

public class BattleHighEvent: PvPEvent
{
    public BattleHighEvent()
    {
        Name = "Battle High Gained (Not Implemented)";
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
        return message is BattleHighMessage;
    }
}