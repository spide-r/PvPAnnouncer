using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyLimitBreakEvent: PvPActionEvent
{
    public AllyLimitBreakEvent()
    {
        Name = "Limit Breaks";
    }

    public override List<string> SoundPaths()
    {
        return [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>{
            {Personalization.MascPronouns, [BoundingFromWallToWallMasc]}, 
            {Personalization.BlackCat, [FeralOnslaught, FelineFerocity, LitheAndLethal]}, 
            {Personalization.HoneyBLovely, [ChangedRoutine, VenomStrikeFem]},
            {Personalization.BruteBomber, [BBEmbiggening, KaboomBBSpecial, UnusedBombarianPress]},
            {Personalization.WickedThunder, [DischargeAether]},
            {Personalization.DancingGreen, [DGSteps]},
            {Personalization.SugarRiot, [SRBringsWorkToLife]},
            {Personalization.BruteAbominator, [ChimericalFoe, FeralPowersWeapon, PunishingAttackFusion]},
        };
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is ActionEffectMessage pp)
        {
            if (PluginServices.PvPMatchManager.IsMonitoredUser(pp.SourceId))
            {
                return ActionIds.IsLimitBreak(pp.ActionId);
            }
        }
        return false;    
    }

}
