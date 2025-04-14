using System;
using Dalamud.Hooking;
using Dalamud.Utility.Signatures;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class PvPEventHooksPublisher: IPvPEventPublisher
{
    
    private unsafe delegate void ProcessPacketActionEffectDelegate(
        int sourceId, IntPtr sourceCharacter, IntPtr pos, ActionEffectHeader* effectHeader, ActionEffect* effectArray, ulong* effectTrail);

    private delegate void ProcessPacketActorControlDelegate(
        uint entityId, uint type, uint statusId, uint amount, uint a5, uint source, uint a7, uint a8, ulong a9, byte flag);
    
    
    [Signature("40 55 53 56 41 54 41 55 41 56 41 57 48 8D AC 24 60 FF FF FF 48 81 EC A0 01 00 00", DetourName = nameof(ProcessPacketActionEffectDetour))]
    private readonly Hook<ProcessPacketActionEffectDelegate> processPacketActionEffectHook = null!;

    [Signature("E8 ?? ?? ?? ?? 0F B7 0B 83 E9 64", DetourName = nameof(ProcessPacketActorControlDetour))]
    private readonly Hook<ProcessPacketActorControlDelegate> processPacketActorControlHook = null!;
    
     private unsafe void ProcessPacketActionEffectDetour(int sourceId, IntPtr sourceCharacter, IntPtr pos, ActionEffectHeader* effectHeader, 
         ActionEffect* effectArray, ulong* effectTrail) { 
         processPacketActionEffectHook.Original(sourceId, sourceCharacter, pos, effectHeader, effectArray, effectTrail);
         ActionEffectPacket actionEffect = new ActionEffectPacket(sourceId, sourceCharacter, pos, effectHeader, effectArray, effectTrail);
         EmitToBroker(actionEffect);
     }

     private void ProcessPacketActorControlDetour(uint entityId, uint type, uint statusId, uint amount, uint a5,
         uint source, uint a7, uint a8, ulong a9, byte flag)
     {
         processPacketActorControlHook.Original(entityId, type, statusId, amount, a5, source, a7, a8, a9, flag);
         ActorControlPacket actorControlPacket =
             new ActorControlPacket(entityId, type, statusId, amount, a5, source, a7, a8, a9, flag);
         EmitToBroker(actorControlPacket);
     }

    public PvPEventHooksPublisher()
    {
        PluginServices.GameInteropProvider.InitializeFromAttributes(this);
        processPacketActionEffectHook.Enable();
        processPacketActorControlHook.Enable();
    }
    
    //todo disabling
    //todo checking if pvp or not

    public void EmitToBroker(IPacket pvpEvent)
    {
        throw new System.NotImplementedException();
    }
}