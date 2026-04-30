using System;
using Dalamud.Game.DutyState;

namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager : IDisposable
{
    void MatchEntered(uint territory);
    void MatchStarted(IDutyStateEventArgs args);
    void MatchEnded(IDutyStateEventArgs args);
    void MatchLeft();
    void MatchQueued();

    bool IsMonitoredUser(int userId);
    bool IsMonitoredUser(uint entityId);
}