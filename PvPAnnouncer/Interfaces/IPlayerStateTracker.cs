namespace PvPAnnouncer.Interfaces;

public interface IPlayerStateTracker : IEventPublisher
{
    bool IsPvP();
    void CheckSoundState();
    bool CheckCNClient();
    bool CheckKRClient();
}