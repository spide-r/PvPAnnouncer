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
        InternalName = "AllyLimitBreakEvent";
    }

    public override List<string> SoundPaths()
    {
        return [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>{
            {Personalization.MascPronouns, [BoundingFromWallToWallMasc, AssaultedRefMasc]}, 
            {Personalization.BlackCat, [FeralOnslaught, FelineFerocity, LitheAndLethal]}, 
            {Personalization.HoneyBLovely, [ChangedRoutine]},
            {Personalization.BruteBomber, [BBEmbiggening, KaboomBBSpecial, UnusedBombarianPress, BBDesprate]},
            {Personalization.WickedThunder, [DischargeAether]},
            {Personalization.DancingGreen, [DGSteps]},
            {Personalization.SugarRiot, [SRBringsWorkToLife]},
            {Personalization.BruteAbominator, [ChimericalFoe, FeralPowersWeapon, PunishingAttackFusion]},
            {Personalization.VampFatale, [VFWickedWeapon, DrainingAudienceFem, VFUnleashedAether, EvenMoreAether, DrainingCrowd, LightheadedFem]},
            {Personalization.Tyrant, [TyrannyTimeMasc, TTAlteredFormMasc, UnleashedFullMightMasc]},
            {Personalization.President, [ArmLifeOwnMasc, ArmSlitheringOutDisgustingMasc, LWScatteredGore, CruelCoil]},
            {Personalization.Spoilers, [NeitherSideHoldingBack, SuchFerocity, AbsoluteBrutality]},
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
