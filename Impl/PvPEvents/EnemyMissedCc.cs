using System;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnemyMissedCc: IPvPActorActionEvent
{
    public string[]? SoundPaths { get; init; } = [BeautifullyDodged, SawThroughIt, EffortlesslyDodged, ClearlyAnticipated, StylishEvasion, AvoidedWithEase, DodgedEverything, BattleElectrifying, ThrillingBattle];
    public Func<IPacket, bool> InvokeRule { get; init; } = _ => false;
}
