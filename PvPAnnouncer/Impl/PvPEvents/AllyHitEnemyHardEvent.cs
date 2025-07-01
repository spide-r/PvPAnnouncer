using System.Collections.Generic;
using System.Linq;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;


public class AllyHitEnemyHardEvent : PvPActionEvent
{
    public AllyHitEnemyHardEvent()
    {
        Name = "Hit Enemy Hard";
    }

    public override List<string> SoundPaths()
    {
        return [StruckSquare, WhatAClash, BattleElectrifying];
    }

    public override Dictionary<uint, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<uint, List<string>>();
    }

    public override bool InvokeRule(IMessage message) 
    {
        if (message is ActionEffectMessage pp)
        {
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                // we dont want self attacks triggering this todo: code duplication
                return false;
            }
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return pp.CritsOrDirectHits() || ActionIds.IsLimitBreak(pp.ActionId) || ActionIds.IsBigHit(pp.ActionId);
            }
        }
        return false;
    }
}
