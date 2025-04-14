using System;
using System.Runtime.InteropServices;
using System.Text;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public unsafe class SoundManager: ISoundManager
{
    public const string PlaySoundSig = "E8 ?? ?? ?? ?? E9 ?? ?? ?? ?? FE C2";
    public delegate IntPtr PlaySoundDelegate( IntPtr path, byte play );

    private readonly PlaySoundDelegate PlaySoundPath;

    private bool muted = false;

    public SoundManager()
    {
        PluginServices.GameInteropProvider.InitializeFromAttributes(this);
        PlaySoundPath = Marshal.GetDelegateForFunctionPointer<PlaySoundDelegate>( PluginServices.SigScanner.ScanText(PlaySoundSig ) );

        PluginServices.PluginLog.Info("Initializing Sound Manager");
    }
    
    // Attributed to VFXEditor: https://github.com/0ceal0t/Dalamud-VFXEditor/blob/main/VFXEditor/Interop/ResourceLoader.Sound.cs
    public void PlaySound(string path)
    {
        if (muted)
        {
            return;
        }
        var bytes = Encoding.ASCII.GetBytes( path );
        var ptr = Marshal.AllocHGlobal( bytes.Length + 1 );
        Marshal.Copy( bytes, 0, ptr, bytes.Length );
        Marshal.WriteByte( ptr + bytes.Length, 0 );
        PlaySoundPath( ptr, 1);
        Marshal.FreeHGlobal( ptr );

    }
    

    public void ToggleMute()
    {
        muted = !muted;
    }

    public void SetMute(bool mute)
    {
        muted = mute;
    }
}