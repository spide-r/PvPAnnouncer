using System;
using Dalamud.Game.DutyState;

namespace PvPAnnouncer.Interfaces;

public interface IDutyManager : IDisposable
{
    void PvPMatchEntered(uint territory);
    void DutyStarted(IDutyStateEventArgs args);
    void DutyCompleted(IDutyStateEventArgs args);
    void PvPMatchLeft();
    bool IsMonitoredUser(int userId);
    bool IsMonitoredUser(uint entityId);
}