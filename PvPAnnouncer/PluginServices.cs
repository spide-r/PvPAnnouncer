using Dalamud.Configuration;
using Dalamud.Game;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.InstanceContent;
using PvpAnnouncer;
using PvPAnnouncer.Impl;
using PvPAnnouncer.Interfaces;

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
    internal static IPlayerState PlayerState { get; private set; }
    
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
    
    [PluginService]
    internal static IDutyState DutyState { get; private set; }
    
    [PluginService]
    internal static IGameConfig GameConfig { get; private set; }
    
    [PluginService]
    internal static INotificationManager NotificationManager { get; private set; }
    
    [PluginService]
    internal static IAddonLifecycle AddonLifecycle { get; private set; }

    
    
    internal static IPvPEventBroker PvPEventBroker { get; private set; }
    internal static IPvPMatchManager PvPMatchManager { get; private set; }
    internal static IAnnouncer Announcer { get; private set; }
    internal static IPvPEventPublisher PvPEventHooksPublisher { get; private set; }
    internal static ISoundManager SoundManager { get; private set; }
    internal static Configuration Config { get; private set; }
    internal static IEventListenerLoader ListenerLoader { get; private set; }
    internal static IPlayerStateTracker PlayerStateTracker { get; private set; }

    internal static void Initialize(IDalamudPluginInterface pluginInterface) {
        pluginInterface.Create<PluginServices>();
        Config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        PvPEventBroker = new PvPEventBroker();
        Announcer = new Announcer();
        PvPEventHooksPublisher = new PvPEventHooksPublisher();
        SoundManager = new SoundManager();
        PlayerStateTracker = new PlayerStateTracker();
        Config.Initialize(pluginInterface, PlayerStateTracker);
        PvPMatchManager = new PvPMatchManager(PlayerStateTracker);
        ListenerLoader = new EventListenerLoader();
        ListenerLoader.LoadEventListeners();
    }
}
#pragma warning restore 8618