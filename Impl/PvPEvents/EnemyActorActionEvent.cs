using System;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyActorActionEvent : IPvPActorActionEvent
{
    private static uint _actionId;

    public EnemyActorActionEvent(uint actionId, string[] soundPaths)
    {
        _actionId = actionId;
        SoundPaths = soundPaths;
        InvokeRule = ShouldEmit;
    }

    public string[]? SoundPaths { get; init; }
    public Func<IPacket, bool> InvokeRule { get; init; } 
    
    private static bool ShouldEmit(IPacket arg)
    {
        if (arg is ActionEffectPacket)
        {
            ActionEffectPacket packet = (ActionEffectPacket)arg;
            if (!PvPAnnouncerPlugin.PvPMatchManager!.IsMonitoredUser(packet.SourceId))
            {
                return packet.ActionId == _actionId;
            }
        }
        return false;
    }
}
