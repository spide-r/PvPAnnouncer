using System;
using System.Runtime.InteropServices;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.System.Resource;
using FFXIVClientStructs.FFXIV.Client.System.Resource.Handle;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl;
using PvPAnnouncer.Impl.Commands;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Windows;

namespace PvPAnnouncer
{
    public sealed class PvPAnnouncerPlugin: IDalamudPlugin
    {
        public WindowSystem WindowSystem = new("PvPAnnouncer");
        public ConfigWindow ConfigWindow { get; init; }

        private readonly Command[] _commands = [new PlaySound(), new MuteMetem(), new TestCommand()];

        public PvPAnnouncerPlugin(IDalamudPluginInterface pluginInterface)
        {
            PluginServices.Initialize(pluginInterface);
            LoadCommands();
            ConfigWindow = new ConfigWindow();
            WindowSystem.AddWindow(ConfigWindow);
            
          
        }
        private void OnCommand(string command, string args)
        {
            ToggleConfigWindow();
        }

        private void ToggleConfigWindow()
        {
            ConfigWindow.Toggle();
        }

        private void LoadCommands()
        {
            Config c = new Config();
            PluginServices.CommandManager.AddHandler(c.GetFullCommandName(), new CommandInfo(OnCommand)
            {
                HelpMessage = "Open the Config Window",
            });
            foreach (var command in _commands)
            {
                PluginServices.CommandManager.AddHandler(command.GetFullCommandName(), command.Info!);

            }
        }

        private void UnloadCommands()
        {
            Config c = new Config();
            PluginServices.CommandManager.RemoveHandler(c.GetFullCommandName());
            foreach (var command in _commands)
            {
                PluginServices.CommandManager.RemoveHandler(command.GetFullCommandName());

            }
            
        }
        public void Dispose()
        {
            UnloadCommands();
        }
    }
}

