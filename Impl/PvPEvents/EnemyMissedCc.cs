using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyMissedCc: PvPActorActionEvent
{
    public EnemyMissedCc()
    {
        Name = "Enemies Fail to hit CC";
    }

    public override List<string> SoundPaths()
    {
        return
        [
            BeautifullyDodged, SawThroughIt, EffortlesslyDodged, ClearlyAnticipated, StylishEvasion, AvoidedWithEase,
            DodgedEverything, BattleElectrifying, ThrillingBattle
        ];
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
        if (packet is ActionEffectMessage pp)
        {
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    if (pp.GetEffectTypes(pp.GetTargetIds()).Contains(ActionEffectType.NoEffectText)) 
                        //todo: this is nasty double looping, also No Effect might not be the same as a miss - need to check
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
