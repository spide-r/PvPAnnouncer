namespace PvPAnnouncer.Interfaces;

public interface IPlayerStateTracker: IPvPEventPublisher
{
    bool IsPvP();
    void CheckSoundState();
    bool IsDawntrailInstalled();
}