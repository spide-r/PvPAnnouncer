using System;
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
        
        private readonly Command[] _commands = [new PlaySound(), new Config(), new MuteMetem(), new TestCommand()];

        public PvPAnnouncerPlugin(IDalamudPluginInterface pluginInterface)
        {
            PluginServices.Initialize(pluginInterface);
            LoadCommands();

        }

        private void LoadCommands()
        {
            foreach (var command in _commands)
            {
                PluginServices.CommandManager.AddHandler(command.Name, command.Info!);

            }
        }

        private void UnloadCommands()
        {
            foreach (var command in _commands)
            {
                PluginServices.CommandManager.RemoveHandler(command.Name);

            }
        }
        public void Dispose()
        {
            UnloadCommands();
            PluginServices.CommandManager.RemoveHandler("/playsound");
        }
    }
}

