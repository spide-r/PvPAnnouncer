using System;
using System.Runtime.InteropServices;
using System.Text;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class SoundManager: ISoundManager
{
    private const string PlaySoundSig = "E8 ?? ?? ?? ?? E9 ?? ?? ?? ?? FE C2";

    private delegate IntPtr PlaySoundDelegate( IntPtr path, byte play );

    private readonly PlaySoundDelegate _playSoundPath;

    private bool _muted;

    public SoundManager()
    {
        PluginServices.GameInteropProvider.InitializeFromAttributes(this);
        _playSoundPath = Marshal.GetDelegateForFunctionPointer<PlaySoundDelegate>( PluginServices.SigScanner.ScanText(PlaySoundSig ) );
        //todo: determine mute status from config
        SetMute(false);
        PluginServices.PluginLog.Info("Initializing Sound Manager");
    }
    
    // Attributed to VFXEditor: https://github.com/0ceal0t/Dalamud-VFXEditor/blob/main/VFXEditor/Interop/ResourceLoader.Sound.cs
    public void PlaySound(string path)
    {
        if (_muted)
        {
            return;
        }
        var bytes = Encoding.ASCII.GetBytes( path );
        var ptr = Marshal.AllocHGlobal( bytes.Length + 1 );
        Marshal.Copy( bytes, 0, ptr, bytes.Length );
        Marshal.WriteByte( ptr + bytes.Length, 0 );
        _playSoundPath( ptr, 1);
        Marshal.FreeHGlobal( ptr );

    }
    

    public void ToggleMute()
    {
        _muted = !_muted;
    }

    public void SetMute(bool mute)
    {
        _muted = mute;
    }
}