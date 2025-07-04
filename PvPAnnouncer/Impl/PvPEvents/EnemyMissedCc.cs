﻿using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyMissedCc: PvPActionEvent
{
    public EnemyMissedCc()
    {
        Name = "Cleansing & Dodging CC";
        InternalName = "EnemyMissedCcEvent";
    }

    public override List<string> SoundPaths()
    {
        return
        [
            BeautifullyDodged, SawThroughIt, EffortlesslyDodged, ClearlyAnticipated, StylishEvasion, AvoidedWithEase,
            DodgedEverything, BattleElectrifying, ThrillingBattle
        ];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>();
    }
    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    if (pp.GetEffectTypes(target).Contains(ActionEffectType.StatusNoEffect)) // whats the difference between StatusNoEffect and NoEffectText 
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
