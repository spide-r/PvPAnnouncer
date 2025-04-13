using System;
using Lumina.Data.Files;

namespace PvPAnnouncer.Interfaces;

public interface ISoundManager
{
    void PlaySound(ScdFile.Sound sound);
    void PlaySound(byte[] audioData);
    void PlaySound(String path);
    
    void StopCurrentSound();
    
    void SetVolume(float volume);
    void ToggleMute();
    void SetMute(bool mute);
    
}
