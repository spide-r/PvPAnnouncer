﻿using System;
using System.Runtime.InteropServices;
using Dalamud.Game.Command;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.System.Resource;
using FFXIVClientStructs.FFXIV.Client.System.Resource.Handle;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl;
using PvPAnnouncer.Impl.Commands;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer
{
    public sealed class PvPAnnouncerPlugin: IDalamudPlugin
    {
        private const string MainCommand = "/ppa";
        private IDalamudPluginInterface PluginInterface { get; init; }
        
        public static ISoundManager? SoundManager { get; private set; }
        public static IAnnouncer? PvPAnnouncer { get; private set; }
        public static IPvPMatchManager? PvPMatchManager { get; private set; }
        

        public PvPAnnouncerPlugin(IDalamudPluginInterface pluginInterface)
        {
            PluginServices.Initialize(pluginInterface);
            PluginInterface = pluginInterface;
            SoundManager = new SoundManager();
            PvPAnnouncer = new Impl.Announcer();
            PvPMatchManager = new PvPMatchManager();

            PluginServices.CommandManager.AddHandler("/playsound", new PlaySound().CommandInfo);

        }

        private void LoadCommands()
        {
            //todo 
        }

        private void UnloadCommands()
        {
            //todo
        }
        public void Dispose()
        {
            PluginServices.CommandManager.RemoveHandler("/playsound");
        }
    }
}

