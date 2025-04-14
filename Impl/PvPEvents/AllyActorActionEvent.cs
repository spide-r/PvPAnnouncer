using System;
using PvPAnnouncer.Impl;
using PvPAnnouncer.Impl.Packets;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyActionEvent : IPvPActorActionEvent
{
    //generic event for specific action and sound pairings
    public AllyActionEvent(uint actionId, string[] soundPaths)
    {
        SoundPaths = soundPaths;
        ActionId = actionId;
        InvokeRule = ShouldEmit;
    }

    public string[]? SoundPaths { get; init; }
    public Func<IPacket, bool> InvokeRule { get; init; }
    public uint ActionId { get; init; }
    
    private bool ShouldEmit(IPacket arg)
    {
        if (arg is ActionEffectPacket)
        {
            ActionEffectPacket packet = (ActionEffectPacket)arg;
            if (PvPAnnouncerPlugin.PvPMatchManager!.IsMonitoredUser(packet.SourceId))
            {
                return packet.ActionId == ActionId;

            }
        }
        return false;
    }
}
