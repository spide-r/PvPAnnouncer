using System.Collections.Generic;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitUnderGuardEvent: PvPActorActionEvent
{

    public override List<string> SoundPaths()
    {
        return [ClearlyAnticipated, FeltThatOneStillStanding, SawThroughIt, IroncladDefense, WhatAClash, BattleElectrifying, ThrillingBattle];
    }

    public override List<string> SoundPathsMasc()
    {
        return [];
    }

    public override List<string> SoundPathsFem()
    {
        return [];
    }

    public override bool InvokeRule(IPacket arg)
    {
        if (arg is ActionEffectMessage)
        {

            ActionEffectMessage pp = (ActionEffectMessage)arg;
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
                                if (status.StatusId == BuffIds.Guard)
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
