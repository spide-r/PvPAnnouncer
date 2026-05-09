using System;
using Dalamud.Game.DutyState;

namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager : IDisposable
{
    void PvPMatchEntered(uint territory);
    void DutyStarted(IDutyStateEventArgs args);
    void DutyCompleted(IDutyStateEventArgs args);
    void PvPMatchLeft();
    void MatchQueued();

    bool IsMonitoredUser(int userId);
    bool IsMonitoredUser(uint entityId);
}