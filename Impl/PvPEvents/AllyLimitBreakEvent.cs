using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyLimitBreakEvent: PvPActorActionEvent
{
    public override List<string> SoundPaths()
    {
        return [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IPacket packet)
    {
        if (packet is ActionEffectMessage)
        {
            ActionEffectMessage pp = (ActionEffectMessage)packet;
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return ActionIds.IsLimitBreak(pp.ActionId);
            }
        }
        return false;    
    }

}
