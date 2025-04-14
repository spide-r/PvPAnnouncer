﻿using Dalamud.Game;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace PvPAnnouncer;

#pragma warning disable 8618
internal class PluginServices {

    [PluginService]
    internal static IDataManager DataManager { get; private set; }
    
    [PluginService]
    internal static ISigScanner SigScanner { get; private set; }
    
    [PluginService]
    internal static ICommandManager CommandManager { get; private set; }

    [PluginService]
    internal static IChatGui ChatGui { get; private set; }

    [PluginService]
    internal static IObjectTable ObjectTable { get; private set; }

    [PluginService]
    internal static IPartyList PartyList { get; private set; }

    [PluginService]
    internal static IClientState ClientState { get; private set; }
    
    [PluginService]
    internal static IDalamudPluginInterface DalamudPluginInterface { get; private set; }
    
    [PluginService]
    internal static IFramework Framework { get; private set; }
    
    [PluginService]
    internal static ICondition Condition { get; private set; }

    
    [PluginService]
    internal static IGameInteropProvider GameInteropProvider { get; private set; }
    
    [PluginService]
    internal static IPluginLog PluginLog { get; private set; }

    internal static void Initialize(IDalamudPluginInterface pluginInterface) {
        pluginInterface.Create<PluginServices>();
    }
}
#pragma warning restore 8618