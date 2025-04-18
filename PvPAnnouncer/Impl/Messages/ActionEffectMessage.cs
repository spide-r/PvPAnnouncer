using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Objects.Types;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using Action = Lumina.Excel.Sheets.Action;

namespace PvPAnnouncer.Impl.Messages;

public unsafe class ActionEffectMessage(
    int sourceId,
    IntPtr sourceCharacter,
    IntPtr pos,
    ActionEffectHeader* effectHeader,
    ActionEffect* effectArray,
    ulong* effectTrail)
    : IActionEffect
{
    public int SourceId { get; set; } = sourceId;
    public IntPtr SourceCharacter { get; set; } = sourceCharacter;
    public IntPtr Pos { get; set; } = pos;
    public ActionEffectHeader* EffectHeader { get; set; } = effectHeader;
    public ActionEffect* EffectArray { get; set; } = effectArray;
    public ulong* EffectTrail { get; set; } = effectTrail;
    public readonly uint Targets = effectHeader->EffectCount;
    public readonly uint ActionId = effectHeader->EffectDisplayType switch {
        ActionEffectDisplayType.MountName => 0xD000000 + effectHeader->ActionId,
        ActionEffectDisplayType.ShowItemName => 0x2000000 + effectHeader->ActionId,
        _ => effectHeader->ActionAnimationId
    };
    
    public Action? GetAction()
    {
        return PluginServices.DataManager.GetExcelSheet<Action>().GetRowOrDefault(ActionId);
    }

    public IGameObject? GetSourceGameObject()
    {
        return PluginServices.ObjectTable.SearchById((uint) SourceId);
    }

    public IGameObject? GetGameObject(uint objectId)
    {
        return PluginServices.ObjectTable.SearchById(objectId);
    }

    public string? GetSource()
    {
        return GetSourceGameObject()?.Name.TextValue;
    }

    public uint[] GetTargetIds()
    {
        uint[] targetIds = new uint[Targets];
        for (var i = 0; i < Targets; i++)
        {
            var actionTargetId = (uint) (EffectTrail[i] & uint.MaxValue);
            targetIds[i] = actionTargetId;
        }
        return targetIds;
    }
    
    public List<ActionEffectType> GetEffectTypes(uint actionTargetId)
    {
        List<ActionEffectType> types = new List<ActionEffectType>();
        for (var i = 0; i < Targets; i++)
        {
            var targetId = (uint) (EffectTrail[i] & uint.MaxValue);
            if (actionTargetId == targetId)
            {
                for (var j = 0; j < 8; j++)
                {
                    var actionEffect = EffectArray[i * 8 + j];
                    types.Add(actionEffect.EffectType);
                }
            }
        }

        return types;
    }

    public bool CritsOrDirectHits()
    {
        for (var i = 0; i < Targets; i++)
        {
                for (var j = 0; j < 8; j++)
                {
                    ref var actionEffect = ref EffectArray[i * 8 + j];
                    if((actionEffect.Param0 & 0x20) == 0x20)
                    {
                        return true;
                    }
                    
                    if((actionEffect.Param0 & 0x40) == 0x40)
                    {
                        return true;
                    }
                    
                } 
            
        }

        return false;
    }

    public List<uint> GetDamageAmount(List<uint> actionTargetIds)
    {
        List<uint> damage = new List<uint>();
        for (var i = 0; i < Targets; i++)
        {
            var targetId = (uint) (EffectTrail[i] * uint.MaxValue);
            if (actionTargetIds.Contains(targetId))
            {
                for (var j = 0; j < 8; j++)
                {
                    var actionEffect = EffectArray[i * 8 + j];
                    if (actionEffect.EffectType == ActionEffectType.Damage)
                    {
                        uint amount = actionEffect.Value;
                        if ((actionEffect.Flags2 & 0x40) == 0x40)
                            amount += (uint)actionEffect.Flags1 << 16;
                        damage.Add(amount);
                    }
                }
            }
        }
        return damage;
    }
    

}