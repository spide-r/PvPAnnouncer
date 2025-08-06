using System;
using Dalamud.Hooking;
using Dalamud.Utility.Signatures;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Impl;

public class PvPEventHooksPublisher: IPvPEventPublisher, IDisposable
{
    
    private unsafe delegate void ProcessPacketActionEffectDelegate(
        int sourceId, IntPtr sourceCharacter, IntPtr pos, ActionEffectHeader* effectHeader, ActionEffect* effectArray, ulong* effectTrail);

    private delegate void ProcessPacketActorControlDelegate(
        uint entityId, uint type, uint statusId, uint amount, uint a5, uint source, uint a7, uint a8, ulong a9, byte flag);
    
    [Signature("E8 ?? ?? ?? ?? 48 8B 8D ?? ?? ?? ?? 48 33 CC E8 ?? ?? ?? ?? 48 81 C4 00 05 00 00", DetourName = nameof(ProcessPacketActionEffectDetour))]
    private readonly Hook<ProcessPacketActionEffectDelegate> processPacketActionEffectHook = null!;

    [Signature("E8 ?? ?? ?? ?? 0F B7 0B 83 E9 64", DetourName = nameof(ProcessPacketActorControlDetour))]
    private readonly Hook<ProcessPacketActorControlDelegate> processPacketActorControlHook = null!;
    
     private unsafe void ProcessPacketActionEffectDetour(int sourceId, IntPtr sourceCharacter, IntPtr pos, ActionEffectHeader* effectHeader, 
         ActionEffect* effectArray, ulong* effectTrail) { 
         processPacketActionEffectHook.Original(sourceId, sourceCharacter, pos, effectHeader, effectArray, effectTrail);
         try
         {
             ActionEffectMessage actionEffect = new ActionEffectMessage(sourceId, sourceCharacter, pos, effectHeader,
                 effectArray, effectTrail);
             EmitToBroker(actionEffect);
         }
         catch (Exception e)
         {
             PluginServices.PluginLog.Error(e, "Problem when Processing Action Effect");
 
         }
     }

     private void ProcessPacketActorControlDetour(uint entityId, uint type, uint statusId, uint amount, uint a5,
         uint source, uint a7, uint a8, ulong a9, byte flag)
     {
         processPacketActorControlHook.Original(entityId, type, statusId, amount, a5, source, a7, a8, a9, flag);
         try
         {
             ActorControlMessage actorControlMessage =
                 new ActorControlMessage(entityId, type, statusId, amount, a5, source, a7, a8, a9, flag);
             if (actorControlMessage.GetCategory() == ActorControlCategory.GainEffect)
             {
                 if (!PluginServices.PvPMatchManager.IsMonitoredUser(entityId))
                 {
                     return;
                 }
                 if (statusId == StatusIds.BH5)
                 {
                     EmitToBroker(new BattleHighMessage(5));
                 } else if (statusId == StatusIds.BH4)
                 {
                     EmitToBroker(new BattleHighMessage(4));
                 } 
                 else if (statusId == StatusIds.BH3)
                 {
                     EmitToBroker(new BattleHighMessage(3));
                 } else if (statusId == StatusIds.BH2)
                 {
                     EmitToBroker(new BattleHighMessage(2));
                 }else if (statusId == StatusIds.BH1)
                 {
                     EmitToBroker(new BattleHighMessage(1));
                 } else if (statusId == StatusIds.FlyingHigh)
                 {
                     EmitToBroker(new FlyingHighMessage());
                 } else if (statusId == StatusIds.Soaring)
                 {
                     EmitToBroker(new SoaringMessage((int) amount));
                 }
             }
             else
             {
                 EmitToBroker(actorControlMessage);
             }
         }
         catch (Exception e)
         {
             PluginServices.PluginLog.Error(e, "Problem when Processing Actor Control");
         }
     }

    public PvPEventHooksPublisher()
    {
        PluginServices.GameInteropProvider.InitializeFromAttributes(this);
        processPacketActionEffectHook.Enable();
        processPacketActorControlHook.Enable();
    }
    public void EmitToBroker(IMessage pvpEvent)
    {
        PluginServices.PvPEventBroker.IngestMessage(pvpEvent);
    }

    public void Dispose()
    {
        processPacketActionEffectHook.Disable();
        processPacketActorControlHook.Disable();
        
        processPacketActionEffectHook.Dispose();
        processPacketActorControlHook.Dispose();
    }
}