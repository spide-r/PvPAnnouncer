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

    private delegate void ProcessPacketActorControlDelegate(uint entityId, uint type, uint statusId, uint amount, 
        uint a5, uint source, uint a7, uint a8, uint param7New, uint param8New, ulong targetId, byte flag);
    //uint category, uint eventId, uint param1, uint param2, uint param3, uint param4, uint param5, uint param6, uint param7, uint param8, ulong targetId, byte param9
    
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

     //GOTCHA: some of these are labeled incorrectly but we only care about entityid and type 
     //todo Actor Control is much much more complicated than previously thought, apparently theres 3 different ways each of these args can be used - yikes!
     private void ProcessPacketActorControlDetour(uint entityId, uint category, uint arg1, uint arg2, uint arg3, uint arg4, uint arg5, uint arg6, uint arg7, uint arg8, ulong targetId, byte isRecorded)
     {
         processPacketActorControlHook.Original(entityId, category, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, targetId, isRecorded);
         try
         {
             var actorControlMessage =
                 new ActorControlMessage(entityId, category);

             if (PluginServices.PvPMatchManager.IsMonitoredUser(entityId))
             {
                 PluginServices.PluginLog.Verbose($"Processing Actor Control entityId {entityId},  category {(ActorControlCategory) category}, arg1 {arg1}, " +
                                                  $"a2 {arg2}, a3 {arg3}, a4 {arg4}, a5 {arg5}, a6 {arg6}, a7 {arg7}, a8 {arg8}, targetId {targetId}, flag {isRecorded}");

             }
         
             if (actorControlMessage.GetCategory() == ActorControlCategory.GainEffect)
             {
               
                 if (!PluginServices.PvPMatchManager.IsMonitoredUser(entityId))
                 {
                     return;
                 }

                 if (arg1 == StatusIds.BH5)
                 {
                     EmitToBroker(new BattleHighMessage(5));
                 } else if (arg1 == StatusIds.BH4)
                 {
                     EmitToBroker(new BattleHighMessage(4));
                 } 
                 else if (arg1 == StatusIds.BH3)
                 {
                     EmitToBroker(new BattleHighMessage(3));
                 } else if (arg1 == StatusIds.BH2)
                 {
                     EmitToBroker(new BattleHighMessage(2));
                 }else if (arg1 == StatusIds.BH1)
                 {
                     EmitToBroker(new BattleHighMessage(1));
                 } else if (arg1 == StatusIds.FlyingHigh)
                 {
                     EmitToBroker(new FlyingHighMessage());
                 } else if (arg1 == StatusIds.Soaring)
                 {
                     EmitToBroker(new SoaringMessage((int) arg2));
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