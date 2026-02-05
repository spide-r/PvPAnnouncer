using Dalamud.Configuration;
using Dalamud.Game;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.InstanceContent;
using PvpAnnouncer;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Windows;

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
    internal static IShoutcastFactory ShoutcastFactory { get; private set; }
    internal static IEventShoutcastMapping EventShoutcastMapping { get; private set; } //todo make these 3 not null
    internal static IShoutcastRepository ShoutcastRepository { get; private set; }
    internal static IShoutcastBuilder ShoutcastBuilder { get; private set; }
    
    internal static VoiceLineTesterWindow voiceLineTesterWindow { get; private set; }

    internal static void Initialize(IDalamudPluginInterface pluginInterface, WindowSystem window) {
        pluginInterface.Create<PluginServices>();
        var newCfg = pluginInterface.GetPluginConfig() == null;
        Config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        PvPEventBroker = new PvPEventBroker();
        Announcer = new Announcer(EventShoutcastMapping, ShoutcastRepository);
        PvPEventHooksPublisher = new PvPEventHooksPublisher();
        SoundManager = new SoundManager();
        PlayerStateTracker = new PlayerStateTracker();
        Config.Initialize(pluginInterface, PlayerStateTracker, GameConfig, newCfg);
        PvPMatchManager = new PvPMatchManager(PlayerStateTracker);
        ShoutcastFactory = new ShoutcastFactory(DataManager);
        ScionLines.InitScionLines(ShoutcastFactory);
        AnnouncerLines.Init(ShoutcastFactory);
        
        voiceLineTesterWindow = new VoiceLineTesterWindow();
        window.AddWindow(voiceLineTesterWindow);
        ListenerLoader = new EventListenerLoader();
        ListenerLoader.LoadEventListeners();
    }
}
#pragma warning restore 8618