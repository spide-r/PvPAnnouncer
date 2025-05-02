using System;

namespace PvPAnnouncer.Interfaces;

public interface IPvPMatchManager: IDisposable
{
    void MatchEntered(ushort territory);
    void MatchStarted(object? sender, ushort @ushort);
    void MatchEnded(object? sender, ushort @ushort);
    void MatchLeft();
    void MatchQueued();
    
    bool IsMonitoredUser(int userId);
    
    bool IsMonitoredUser(uint entityId);

    void RegisterDeath(uint userId);
    
    void UnregisterDeath(uint userId);
    bool IsDead(uint userId);




}
