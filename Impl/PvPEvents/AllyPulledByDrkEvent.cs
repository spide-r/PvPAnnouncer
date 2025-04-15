using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyPulledByDrkEvent: PvPActorActionEvent
{
    

    public override List<string> SoundPaths()
    {
        return [SuckedIn, BattleElectrifying, ThrillingBattle];
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
        if (packet is ActionEffectPacket)
        {
            ActionEffectPacket aa = (ActionEffectPacket) packet;

            ulong actionId = aa.ActionId;
            return actionId == ActionIds.SaltedEarth;
      
        }
        return false;
    }
}
