using System;
using System.Linq;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitUnderGuardEvent: IPvPActorActionEvent
{
    public AllyHitUnderGuardEvent(Func<IPacket, bool> invokeRule)
    {
        InvokeRule = ShouldInvoke;
    }

    public string[]? SoundPaths { get; init; } = [ClearlyAnticipated, FeltThatOneStillStanding, SawThroughIt, IroncladDefense, WhatAClash, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; } 

    private bool ShouldInvoke(IPacket arg)
    {
        if (arg is ActionEffectPacket)
        {

            ActionEffectPacket pp = (ActionEffectPacket)arg;
            foreach (var target in pp.GetTargetIds())
            {
                if (PvPAnnouncerPlugin.PvPMatchManager.IsMonitoredUser(target))
                {
                    IGameObject? obj = pp.GetGameObject(target);
                    if (obj is IPlayerCharacter)
                    {
                        IPlayerCharacter? player = obj as IPlayerCharacter;
                        if (false) //todo pull status list and check for guard
                        {
                            return true;
                        }
                    }
                }
            }
            foreach (var allianceMember in PvPAnnouncerPlugin.PvPMatchManager!.AllianceMembers)
            {
                if (pp.GetTargetIds().Contains(allianceMember))
                {
                    return pp.CritsOrDirectHits();
                }
            }
            
        }

        return false;
    }
}
