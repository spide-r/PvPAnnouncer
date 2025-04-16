using System;
using System.Collections;
using Lumina.Data.Files;

namespace PvPAnnouncer.Interfaces;

public interface ISoundManager
{    
    void PlaySound(String path);
    void ToggleMute();
    void SetMute(bool mute);
    
}
