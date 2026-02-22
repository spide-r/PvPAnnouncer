using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using Dalamud.Configuration;
using Dalamud.Game;
using Dalamud.Interface.Components;
using Dalamud.Interface.ImGuiNotification;
using Dalamud.Interface.Utility;
using Dalamud.Utility;
using Lumina.Data;
using PvPAnnouncer;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration _configuration;
    private readonly IShoutcastRepository _shoutcastRepository;
    private readonly IStringRepository _casterRepository;
    private readonly IStringRepository _attributeRepository;

    public ConfigWindow(IShoutcastRepository shoutcastRepository, Configuration pluginConfiguration,
        IEventShoutcastMapping eventShoutcastMapping, IStringRepository casterRepository,
        IStringRepository attributeRepository) : base(
        "PvPAnnouncer Configuration")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(500, 425),
            MaximumSize = new Vector2(800, 1000)
        };

        SizeCondition = ImGuiCond.Always;

        _configuration = pluginConfiguration;
        _shoutcastRepository = shoutcastRepository;
        _casterRepository = casterRepository;
        _attributeRepository = attributeRepository;
    }

    public void Dispose()
    {
    }

    private void DoAttribute(string attr)
    {
        var attVar = _configuration.WantsAttribute(attr);
        if (ImGui.Checkbox(attr, ref attVar))
        {
            if (attVar)
            {
                _configuration.SetAttribute(attr);
            }
            else
            {
                _configuration.RemoveAttribute(attr);
            }

            _configuration.Save();
        }
    }

    private int _activeEventsSelectedItem;
    private int _disabledEventsSelectedItem = 0;
    private string[] _activeEventsArr = [];
    private string[] _activeEventsArrInternal = [];
    private string[] _disabledEventsArr = [];
    private string[] _disabledEventsArrInternal = [];

    public override void Draw()
    {
        var disabled = _configuration.Disabled;
        var muted = _configuration.Muted;
        var hideBattleText = _configuration.HideBattleText;
        var blEvents = _configuration.BlacklistedEvents;
        var cooldown = _configuration.CooldownSeconds;
        var percent = _configuration.Percent;
        var repeatVoiceLine = _configuration.RepeatVoiceLineQueue;
        var repeatEventCommentary = _configuration.RepeatEventCommentaryQueue;
        var animationDelayFactor = _configuration.AnimationDelayFactor;
        var wolvesDen = _configuration.WolvesDen;
        var notify = _configuration.Notify;
        var icon = _configuration.WantsIcon;


        if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
        {
            ImGui.Separator();
            ImGui.TextWrapped(
                "Dawntrail is not installed! This plugin needs the expansion installed in order to work!");
            ImGui.Separator();
        }

        ImGui.TextWrapped("I love your feedback! It helps me make the plugin better! " +
                          "If you have any issues at all, suggestions/questions etc. I would love to hear them no matter how small! " +
                          "Use the plugin feedback button or contact .spider in the Dalamud Discord.");
        ImGui.Separator();

        if (ImGui.Button("Test The Announcer"))
        {
            PluginServices.PlayerStateTracker.CheckSoundState();
            Shoutcast[] bt = _shoutcastRepository.GetShoutcasts()
                .Where(bt => PluginServices.Config.WantsAllAttributes(bt.Attributes)).ToArray();
            if (bt.Length != 0) // no announcers selected
            {
                var e = bt[Random.Shared.Next(bt.Length)];
                PluginServices.Announcer.PlayForTesting(e);
                PluginServices.ChatGui.Print($"Playing Voiceline for {e.Shoutcaster}", InternalConstants.MessageTag);

                if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
                {
                    Notification n = new Notification();
                    n.Title = "Dawntrail Not installed!";
                    n.Type = NotificationType.Error;
                    n.Minimized = false;
                    n.MinimizedText = "Dawntrail is not installed!";
                    n.Content = "You must install Dawntrail for this plugin to work!";
                    PluginServices.NotificationManager.AddNotification(n);
                }
            }
            else
            {
                var dict = new Dictionary<string, string>
                {
                    ["en"] = "You don't have any announcers selected!"
                };
                var s = PluginServices.ShoutcastBuilder.WithSoundPath(InternalConstants.DefaultSoundPath)
                    .WithId("OopsAnnouncerDev").WithShoutcaster(InternalConstants.PvPAnnouncerDevName)
                    .WithIcon(InternalConstants.PvPAnnouncerDevIcon)
                    .WithTranscription(dict).Build();
                PluginServices.Announcer.SendBattleTalk(s);
            }
        }

        if (ImGui.Checkbox("Disabled", ref disabled))
        {
            _configuration.Disabled = disabled;
            _configuration.Save();
        }

        ImGui.Separator();
        ImGui.TextWrapped("Customization:");
        if (ImGui.Button("Show All Possible Voicelines"))
        {
            //todo eventually move this to the customization window
            PluginServices.VoiceLineTesterWindow.Toggle();
        }

        ImGui.SameLine();
        ImGuiComponents.HelpMarker("This is mostly for testing/debugging purposes. Enjoy!");
        if (ImGui.Button("Open the Event Customization & Voiceline creation window!"))
        {
            PluginServices.CustomizationWindow.Toggle();
        }

        ImGuiComponents.HelpMarker("Soft-launched feature for real pvp enjoyers. ");
        ImGui.Separator();


        ImGui.Text("Announcers: ");

        var c = 0;
        foreach (var caster in _casterRepository.GetAttributeList())
        {
            DoAttribute(caster);
            c++;
            if (c % 4 != 0)
            {
                ImGui.SameLine();
            }
        }

        ImGui.NewLine();

        ImGui.Separator();

        ImGui.TextWrapped("Use Voice Lines mentioning: ");
        ImGui.SameLine();
        ImGuiComponents.HelpMarker(
            "These two values allow announcers to use voice lines usually reserved for specific people. For example, \nMetem may say \"The Honey B. Lovely show has begun!\" if Honey B. Lovely is enabled.");
        var a = 0;
        foreach (var se in _attributeRepository.GetAttributeList())
        {
            DoAttribute(se);
            a++;
            if (a % 4 != 0)
            {
                ImGui.SameLine();
            }
        }

        ImGui.NewLine();
        ImGui.Separator();
        if (ImGui.Checkbox("Use Voice Lines in the Wolves Den", ref wolvesDen))
        {
            _configuration.WolvesDen = wolvesDen;
            _configuration.Save();
        }

        if (ImGui.Checkbox("Show Announcer Portrait", ref icon))
        {
            _configuration.WantsIcon = icon;
            _configuration.Save();
        }

        if (ImGui.Checkbox("Notify when Voice Volume is muted", ref notify))
        {
            _configuration.Notify = notify;
            _configuration.Save();
        }

        ImGui.Separator();
        ImGui.TextWrapped("Minimum delay between announcements");
        ImGui.Indent();
        if (ImGui.SliderInt("###SliderCooldown", ref cooldown, 1, 120, "%ds", ImGuiSliderFlags.AlwaysClamp))
        {
            _configuration.CooldownSeconds = cooldown;
            _configuration.Save();
        }

        ImGui.Unindent();


        ImGui.TextWrapped("Announcement Frequency");
        ImGuiComponents.HelpMarker("This controls the chance of announcing any given event.");
        ImGui.Indent();

        if (ImGui.SliderInt("###SliderPercent", ref percent, 1, 100, "%d%%", ImGuiSliderFlags.AlwaysClamp))
        {
            _configuration.Percent = percent;
            _configuration.Save();
        }

        ImGui.Unindent();

        ImGui.TextWrapped("Announcement Delay");
        ImGuiComponents.HelpMarker(
            "Sometimes this plugin announces a split-second too early. This setting adds a very minor delay which should prevent announcements before an action finishes.");
        ImGui.Indent();

        if (ImGui.SliderInt("###SliderAnimationFactor", ref animationDelayFactor, 250, 2000, "%dms",
                ImGuiSliderFlags.AlwaysClamp))
        {
            _configuration.AnimationDelayFactor = animationDelayFactor;
            _configuration.Save();
        }

        ImGui.Unindent();


        ImGui.TextWrapped("Minimum unique voice lines to play before a repeat is allowed.");
        ImGui.Indent();
        if (ImGui.SliderInt("##SliderVoicelines", ref repeatVoiceLine, 1, 25))
        {
            _configuration.RepeatVoiceLineQueue = repeatVoiceLine;
            _configuration.Save();
        }

        ImGui.Unindent();


        ImGui.TextWrapped("Minimum number of events to announce before a repeat is allowed.");
        ImGui.Indent();
        if (ImGui.SliderInt("###SliderEvents", ref repeatEventCommentary, 1, 10))
        {
            _configuration.RepeatEventCommentaryQueue = repeatEventCommentary;
            _configuration.Save();
        }

        ImGui.Unindent();
        ImGui.Separator();
        ImGui.Text("Announcer Spoken Language:");
        DoLanguageVoiceSelection();

        ImGui.Text("Announcer Written Language:");
        ImGuiComponents.HelpMarker(
            "Due to how the plugin works, some voice lines do not have text equivalents in game. (specifically Mahjong Lines and Encrypted Voicelines from M12S). They have been manually transcribed to English. If you wish to help translate them to different languages, please contact the Plugin Developer.");

        DoLanguageTextSelection();
        ImGui.Separator();


        if (ImGui.Checkbox("Mute Announcer", ref muted))
        {
            _configuration.Muted = muted;
            _configuration.Save();
        }

        ImGui.SameLine();
        if (ImGui.Checkbox("Hide Battle Text", ref hideBattleText))
        {
            _configuration.HideBattleText = hideBattleText;
            _configuration.Save();
        }

        ImGui.Separator();

        List<string> activeEvents = new List<string>();
        List<string> activeEventsInternal = new List<string>();
        foreach (var e in PluginServices.PvPEventBroker.GetPvPEvents())
        {
            var eventId = e.Id;
            if (!blEvents.Contains(eventId))
            {
                activeEvents.Add(e.Name);
                activeEventsInternal.Add(eventId);
            }
        }


        List<string> listDisabledInternal = [];
        List<string> listDisabledPublic = [];
        foreach (string internalName in blEvents)
        {
            var e = PluginServices.PvPEventBroker.GetEvent(internalName);
            if (e == null)
            {
                continue;
            }

            listDisabledInternal.Add(internalName);
            listDisabledPublic.Add(e.Name);
        }

        _activeEventsArr = activeEvents.ToArray();
        _activeEventsArrInternal = activeEventsInternal.ToArray();
        ImGui.Text("Enabled Events:");
        ImGui.ListBox("###EnabledEvents", ref _activeEventsSelectedItem, _activeEventsArr);
        if (ImGui.Button("Disable"))
        {
            if (_activeEventsSelectedItem < _activeEventsArrInternal.Length)
            {
                _configuration.BlacklistedEvents.Add(_activeEventsArrInternal[_activeEventsSelectedItem]);
                _configuration.Save();
            }
        }

        _disabledEventsArrInternal = listDisabledInternal.ToArray();
        _disabledEventsArr = listDisabledPublic.ToArray();
        ImGui.Text("Disabled Events:");
        ImGui.ListBox("###DisabledEvents", ref _disabledEventsSelectedItem, _disabledEventsArr);
        if (ImGui.Button("Enable"))
        {
            if (_disabledEventsSelectedItem < _disabledEventsArrInternal.Length)
            {
                _configuration.BlacklistedEvents.Remove(_disabledEventsArrInternal[_disabledEventsSelectedItem]);
                _configuration.Save();
            }
        }
    }

    private void DoLanguageVoiceSelection()
    {
        foreach (var keyValuePair in LanguageUtil.LanguageMap)
        {
            var k = keyValuePair.Key;
            var v = keyValuePair.Value;
            if (k == 0)
            {
                continue;
            }

            var keyName = Enum.GetName(k) ?? "Unknown Language";
            if (!PluginServices.DataManager.FileExists($"sound/voice/vo_line/8205353_{v}.scd"))
            {
                continue;
            }

            var lang = _configuration.Language;
            if (ImGui.RadioButton(keyName + "###" + "LanguageVoiceSelection" + v, lang.Equals(v)))
            {
                _configuration.Language = v;
                _configuration.Save();
            }

            ImGui.SameLine();
        }

        ImGui.NewLine();
    }

    private void DoLanguageTextSelection()
    {
        var langs = Enum.GetValues<ClientLanguage>().Cast<ClientLanguage>();

        foreach (var clientLangEnum in langs)
        {
            var enumCodeString = clientLangEnum.ToCode();

            var userFacingLang = Enum.GetName(clientLangEnum) ?? "Unknown Language";

            var configuredTextLang = _configuration.TextLanguage;

            if (ImGui.RadioButton(userFacingLang + "###" + "LanguageTextSelection" + enumCodeString,
                    configuredTextLang.Equals(enumCodeString)))
            {
                _configuration.TextLanguage = enumCodeString;
                _configuration.Save();
            }

            ImGui.SameLine();
        }

        ImGui.NewLine();
    }
}