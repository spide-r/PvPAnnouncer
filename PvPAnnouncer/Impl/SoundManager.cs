using System;
using System.Runtime.InteropServices;
using System.Text;
using Dalamud.Hooking;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class SoundManager: ISoundManager
{
    // Attributed to VFXEditor: https://github.com/0ceal0t/Dalamud-VFXEditor/blob/main/VFXEditor/Interop/ResourceLoader.Sound.cs

    private const string PlaySoundSig = "E8 ?? ?? ?? ?? E9 ?? ?? ?? ?? FE C2";
    public const string InitSoundSig = "E8 ?? ?? ?? ?? 8B 5D 77";

    private delegate IntPtr PlaySoundDelegate( IntPtr path, byte play );

    private readonly PlaySoundDelegate _playSoundPath;
    
    private delegate IntPtr InitSoundPrototype( IntPtr a1, IntPtr path, float volume, int idx, int a5, uint a6, uint a7 );

    //private readonly InitSoundPrototype _initSoundHook;

    private bool _muted;

    public SoundManager()
    {
        PluginServices.GameInteropProvider.InitializeFromAttributes(this);
        _playSoundPath = Marshal.GetDelegateForFunctionPointer<PlaySoundDelegate>( PluginServices.SigScanner.ScanText(PlaySoundSig ) );
      //  _initSoundHook = Marshal.GetDelegateForFunctionPointer<InitSoundPrototype>( PluginServices.SigScanner.ScanText(InitSoundSig ) );
        SetMute(PluginServices.Config.Muted);
        PluginServices.PluginLog.Verbose("Initializing Sound Manager");
    }
    
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
    


    /*
    private IntPtr InitSoundDetour( IntPtr a1, IntPtr path, float volume, int idx, int a5, uint a6, uint a7 ) {
        PluginServices.PluginLog.Verbose($"a1: {a1}, path: {path}, volume: {volume}, idx: {idx}, a5: {a5}, a7: {a7}");

        if( path != IntPtr.Zero) {
            _initSoundHook.Original()
            return _initSoundHook.Original( a1, path, volume, idx, a5, a6, a7 );
        }
        return _initSoundHook.Original( a1, path, volume, idx, a5, a6, a7 );
    }
    */

    public void ToggleMute()
    {
        _muted = !_muted;
        PluginServices.Config.Muted = _muted;
    }

    public void SetMute(bool mute)
    {
        _muted = mute;
        PluginServices.Config.Muted = _muted;

    }
}