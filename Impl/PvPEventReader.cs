using System;
using Dalamud.Hooking;
using Dalamud.Utility.Signatures;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class PvPEventReader: IPvPEventReader
{
    [Signature("40 55 53 56 41 54 41 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 70",
        DetourName = nameof(ReceiveAbilityEffectDetour))]
    private readonly Hook<ReceiveAbilityDelegate> _receiveAbilityEffectHook = null!;
    
    private unsafe delegate void ReceiveAbilityDelegate(int sourceId, IntPtr sourceCharacter, IntPtr pos, ActionEffectHeader* effectHeader,
        ActionEffect* effectArray, ulong* effectTrail);
    
     private unsafe void ReceiveAbilityEffectDetour(int sourceId, IntPtr sourceCharacter, IntPtr pos, ActionEffectHeader* effectHeader, 
         ActionEffect* effectArray, ulong* effectTrail) { 
         _receiveAbilityEffectHook.Original(sourceId, sourceCharacter, pos, effectHeader, effectArray, effectTrail);
         
    }

    public PvPEventReader()
    {
        PluginServices.GameInteropProvider.InitializeFromAttributes(this);
    }
    public void ProcessActorControl()
    {
        throw new System.NotImplementedException();
    }

    public void ProcessAction()
    {
        throw new System.NotImplementedException();
    }

    public void ReadToast()
    {
        throw new System.NotImplementedException();
    }

    public void Emit(IPvPEvent pvpEvent)
    {
        throw new System.NotImplementedException();
    }
}