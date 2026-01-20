using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyMitUsedEvent: PvPActionEvent
{
    public AllyMitUsedEvent()
    {
        Name = "Mitigation";
        InternalName = "AllyMitUsedEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return  [IroncladDefense, WhatAClash, ThrillingBattle, MjDontStandAChance, BestDefenceIsGoodDefence, 
            DoYourWorst, KeepYourGuardUp, SoundDecision, SmallMerciesYshtola, MakeReady, UseConfidenceAgainstThem,
            WontFallHereKrile, HaveFaithAllWell, MustStandStrongKrile, KeepYourGuardUpWuk];
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return ActionIds.IsMitigation(pp.ActionId);
            }
        }
        return false;    
    }

}
