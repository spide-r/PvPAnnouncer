using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;


public class AllyHitEnemyHardEvent : PvPActionEvent
{
    public AllyHitEnemyHardEvent()
    {
        Name = "Hit an Enemy Hard";
        InternalName = "AllyHitEnemyHardEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [StruckSquare, WhatAClash, BattleElectrifying, ViciousBlow, RainOfDeath, FeverPitch, 
            HitThemWhereItHurts, WinThisYet, JustALittleMore, Fall, DefeatIsNotAnOption, TurnaboutsFairPlay, 
            ThatWasUnbecoming, DragoonImpressed, LeaveAMark, AmidstGreatChaos, GiveItEverythingAlisaie, 
            ShowThemWhatYoureMadeOf, GiveItEverything, FightHardNotHardEnough, GotThemNowWuk, CantPassChanceWuk, 
            WillStopThemWuk, NoHoldingBackWukMahjong, NoHoldingBackWuk, Impressive, Kill, Rend, More];
    }

    public override bool InvokeRule(IMessage message) 
    {
        if (message is ActionEffectMessage pp)
        {
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                return false;
            }
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return pp.CritsOrDirectHits() || ActionIds.IsLimitBreakAttack(pp.ActionId) || ActionIds.IsBurst(pp.ActionId);
            }
        }
        return false;
    }
}
