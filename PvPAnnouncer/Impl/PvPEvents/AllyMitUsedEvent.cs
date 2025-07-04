using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyMitUsedEvent: PvPActionEvent
{
    public AllyMitUsedEvent()
    {
        Name = "Mitigation";
        InternalName = "AllyMitUsedEvent";
    }

    public override List<string> SoundPaths()
    {
        return  [IroncladDefense, WhatAClash, ThrillingBattle];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>();
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
