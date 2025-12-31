using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitUnderGuardEvent: PvPActionEvent
{
    public AllyHitUnderGuardEvent()
    {
        Name = "Hit while under Guard";
        InternalName = "AllyHitUnderGuardEvent";
    }

    public override List<string> SoundPaths()
    {
        return [ClearlyAnticipated, FeltThatOneStillStanding, SawThroughIt, IroncladDefense, 
            WhatAClash, DirectHitStillStanding, NotFastEnough, MjHeatingUp, MjStillStandingGentle];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>
        {
        };
        
    }

    public override bool InvokeRule(IMessage arg)
    {
        if (arg is ActionEffectMessage pp)
        {
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                return false;
            }
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    IGameObject? obj = pp.GetGameObject(target);
                    if (obj is IPlayerCharacter)
                    {
                        IPlayerCharacter? player = obj as IPlayerCharacter;
                        var list = player?.StatusList;
                        if (list != null)
                            foreach (var status in list)
                            {
                                if (status.StatusId == StatusIds.Guard || status.StatusId == StatusIds.HardenedScales)
                                {
                                    return true;
                                }
                            }
                    }
                }
            }
        }

        return false;
    }
}
