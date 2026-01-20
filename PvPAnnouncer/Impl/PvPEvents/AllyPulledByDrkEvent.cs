using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyPulledByDrkEvent: PvPActionEvent
{
    public AllyPulledByDrkEvent()
    {
        Name = "Pulled By Dark Knight";
        InternalName = "AllyPulledByDrkEvent";
    }

    public override List<BattleTalk> SoundPaths()
    {
        return [SuckedIn, ThrillingBattle, Quicksand, MjRivalVying, WinThisYet, Hahahahahaha];
    }

    public override bool InvokeRule(IMessage message)
    {
        
        if (message is ActionEffectMessage pp)
        {
            if (pp.ActionId != ActionIds.SaltedEarth)
            {
                return false;
            }
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    return true;
                }
            }
            return false;
      
        }
        return false;
    }
}
