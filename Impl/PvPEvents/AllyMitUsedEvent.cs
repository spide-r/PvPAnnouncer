using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyMitUsedEvent: PvPActorActionEvent
{
    public AllyMitUsedEvent()
    {
        Name = "Mitigation used";
    }

    public override List<string> SoundPaths()
    {
        return  [IroncladDefense, WhatAClash, ThrillingBattle];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
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
