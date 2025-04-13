using System;
using System.Collections;
using Lumina.Data.Files;

namespace PvPAnnouncer.Interfaces;

public interface ISoundManager
{    
    Hashtable AudioFiles { get; init; } //key will be path, value will be the audio data needed to play

    void PlaySound(ScdFile.Sound sound);
    void PlaySound(byte[] audioData);
    void PlaySound(String path);
    
    void StopCurrentSound();
    
    void SetVolume(float volume);
    void ToggleMute();
    void SetMute(bool mute);
    
}
