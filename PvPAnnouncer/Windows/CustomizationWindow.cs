using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Components;
using Dalamud.Interface.ImGuiNotification;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using Newtonsoft.Json;

namespace PvPAnnouncer.Windows;

public class CustomizationWindow : Window, IDisposable
{
    //todo shill/ask players nicely to share their voicelines w/ people
    //todo only export 1 character at a time option pre-checked

    private readonly Configuration _configuration;

    public CustomizationWindow(Configuration pluginConfiguration) : base(
        "Character, Event, and Voiceline Management")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        _configuration = pluginConfiguration;
    }

    private int _activeEventsSelectedItem;
    private int _disabledEventsSelectedItem;
    private string[] _activeEventsArr = [];
    private string[] _activeEventsArrInternal = [];
    private string[] _disabledEventsArr = [];
    private string[] _disabledEventsArrInternal = [];
    private int _selection;
    private string _shoutcaster = "";

    public override void Draw()
    {
        var blEvents = _configuration.BlacklistedEvents;
        var single = _configuration.ExportSingleCharacter;

        //todo make this intro look nice
        ImGui.TextWrapped(
            "Hey Hey! PvPAnnouncer dev here. Thanks for using the testing version! This is a BIG feature. Please feel free to play around with it and ask ANY questions!" +
            "\nYour questions will be instrumental in making sure that this customization is accessible and easy to use for all.");
        ImGui.Separator();
        ImGui.TextWrapped(
            "In order for this plugin to play a voiceline, it needs an audio file from the game, and a text transcription. While some voice line audio is transcribed neatly, most audio is independent from its transcription. Hence we must go through some steps to create a proper voiceline.");
        ImGui.TextWrapped("Once a voiceline is created, we must map it to an existing event.");

        //todo make the button layout look nice
        if (ImGui.Button("Open Voice Line Creation Window"))
        {
            PluginServices.VoicelineCreationWindow.Toggle();
        }


        if (ImGui.Button("Add or Remove voicelines for each event"))
        {
            PluginServices.VoicelineMappingWindow.Toggle();
        }

        if (ImGui.Button("Mute & Delete Voicelines")) PluginServices.VoicelineManagementWindow.Toggle();

        if (ImGui.Button("Open Full Voiceline List")) PluginServices.LoadedVoicelineWindow.Toggle();

        if (ImGui.Button("Export Custom Voicelines"))
        {
            CopyValues(
                single && !_shoutcaster.IsNullOrEmpty()
                    ? GetShoutsForShoutcaster(_shoutcaster)
                    : PluginServices.Config.CustomShoutcasts, [], []);
        }

        ImGui.SameLine();

        if (ImGui.Button("Export Voiceline Mapping"))
        {
            CopyValues([],
                single && !_shoutcaster.IsNullOrEmpty()
                    ? GetCustomMappingForShoutcaster(_shoutcaster)
                    : PluginServices.Config.MappingOverride, []);
        }

        ImGui.SameLine();

        if (ImGui.Button("Export Both"))
            CopyValues(
                single && !_shoutcaster.IsNullOrEmpty()
                    ? GetShoutsForShoutcaster(_shoutcaster)
                    : PluginServices.Config.CustomShoutcasts,
                single && !_shoutcaster.IsNullOrEmpty()
                    ? GetCustomMappingForShoutcaster(_shoutcaster)
                    : PluginServices.Config.MappingOverride, []);

        if (ImGui.Checkbox("Export Custom Values for a Single Character", ref single))
        {
            _configuration.ExportSingleCharacter = single;
            _configuration.Save();
        }

        var selection = _selection;
        if (single)
        {
            var scList = PluginServices.ShoutcastRepository.GetShoutcasters().Where(CasterHasChanges).ToList();
            ImGui.TextWrapped("Select The Character to Export:");
            if (ImGui.ListBox("###Chars", ref selection,
                    scList)) //slow and sloppy, but due to low list count it doesnt matter
            {
                _selection = selection;
                _shoutcaster = scList[selection];
            }
        }

        ImGui.Separator();
        if (ImGui.Button("Import From Clipboard"))
        {
            var b64Clip = ImGui.GetClipboardText();
            PluginServices.PluginLog.Verbose($"Found {b64Clip}");
            var impVl = 0;
            var impMap = 0;
            try
            {
                var decoded = new string(Encoding.UTF8.GetString(Convert.FromBase64String(b64Clip)));
                PluginServices.PluginLog.Verbose($"Decoded to: {decoded} ");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(decoded)!;
                if (dict.TryGetValue("shoutcasts", out string? sc))
                {
                    var decodedSc = new string(Encoding.UTF8.GetString(Convert.FromBase64String(sc)));
                    var deserializedSc = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedSc)!;
                    foreach (var keyValuePair in deserializedSc)
                    {
                        if (_configuration.DupeVoicelineChoice == 1 &&
                            PluginServices.ShoutcastRepository.ContainsKey(keyValuePair.Key)) // skip
                            continue;

                        PluginServices.Config.CustomShoutcasts[keyValuePair.Key] = keyValuePair.Value;
                        impVl++;
                    }
                }

                if (dict.TryGetValue("mapping", out string? mapping))
                {
                    var decodedMap = new string(Encoding.UTF8.GetString(Convert.FromBase64String(mapping)));
                    var deserializedMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedMap)!;
                    foreach (var keyValuePair in deserializedMapping)
                    {
                        switch (_configuration.DupeMappingChoice)
                        {
                            // skip stuff already overriden 
                            case 1 when PluginServices.Config.MappingOverride.ContainsKey(keyValuePair.Key):
                                continue;
                            case 1:
                                PluginServices.Config.MappingOverride[keyValuePair.Key] =
                                    keyValuePair.Value; //no overriden values, add freely
                                impMap++;
                                break;
                            // override - we dont care whats in our config
                            case 2:
                                PluginServices.Config.MappingOverride[keyValuePair.Key] = keyValuePair.Value;
                                impMap++;
                                break;
                            // either no dupe or we want to merge 
                            case 0:
                            {
                                var current =
                                    PluginServices.JsonLoader.ConstructMappingFromJson(
                                        PluginServices.Config.MappingOverride.GetValueOrDefault(keyValuePair.Key,
                                            "{}"));
                                var toMerge = PluginServices.JsonLoader.ConstructMappingFromJson(
                                    keyValuePair.Value);
                                current.AddRange(toMerge);
                                current = current.Distinct().ToList();
                                PluginServices.Config.MappingOverride[keyValuePair.Key] = PluginServices.JsonLoader
                                    .BuildJsonMapping(keyValuePair.Key, current).ToJsonString();
                                impMap++;
                                break;
                            }
                        }
                    }
                }


                PluginServices.Config.Save();
                PluginServices.ConfigManager.ReloadConfig();
                PluginServices.ChatGui.Print(
                    $"Successfully Imported {impVl} voicelines and mapping for {impMap} events!");
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "Oops!");
                PluginServices.NotificationManager.AddNotification(new Notification()
                {
                    Content = "Error: " + e.Message,
                    Type = NotificationType.Error,
                    Title = "Error Importing Clipboard Data!"
                });
            }
        }

        var overrideVl = _configuration.DupeVoicelineChoice == 0;
        var skipVl = _configuration.DupeVoicelineChoice == 1;
        var mergeMap = _configuration.DupeMappingChoice == 0;
        var skipMap = _configuration.DupeMappingChoice == 1;
        var overrideMap = _configuration.DupeMappingChoice == 2;

        ImGui.Text("Voiceline Import Options:");
        if (ImGui.RadioButton("Import and Override Duplicate Voicelines (Default)", overrideVl))
        {
            _configuration.DupeVoicelineChoice = 0;
            _configuration.Save();
        }

        if (ImGui.RadioButton("Don't import Duplicate Voicelines", skipVl))
        {
            _configuration.DupeVoicelineChoice = 1;
            _configuration.Save();
        }

        ImGui.Text("Event Mapping Import Options:");
        if (ImGui.RadioButton("Combine Event Mapping (Default)", mergeMap))
        {
            _configuration.DupeMappingChoice = 0;
            _configuration.Save();
        }

        if (ImGui.RadioButton("Preserve Existing Event Mapping", skipMap))
        {
            _configuration.DupeMappingChoice = 1;
            _configuration.Save();
        }

        ImGuiComponents.HelpMarker(
            "If you have made modifications to an event, this option will ensure that the import does not touch those modifications.");

        if (ImGui.RadioButton("Override Existing Event Mapping", overrideMap))
        {
            _configuration.DupeMappingChoice = 2;
            _configuration.Save();
        }


        if (ImGui.CollapsingHeader("Enable & Disable Events"))
        {
            var activeEvents = new List<string>();
            var activeEventsInternal = new List<string>();
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
            foreach (var internalName in blEvents)
            {
                var e = PluginServices.PvPEventBroker.GetEvent(internalName);
                if (e == null) continue;

                listDisabledInternal.Add(internalName);
                listDisabledPublic.Add(e.Name);
            }

            _activeEventsArr = activeEvents.ToArray();
            _activeEventsArrInternal = activeEventsInternal.ToArray();
            ImGui.Text("Enabled Events:");
            ImGui.ListBox("###EnabledEvents", ref _activeEventsSelectedItem, _activeEventsArr);
            if (ImGui.Button("Disable"))
                if (_activeEventsSelectedItem < _activeEventsArrInternal.Length)
                {
                    _configuration.BlacklistedEvents.Add(_activeEventsArrInternal[_activeEventsSelectedItem]);
                    _configuration.Save();
                }

            _disabledEventsArrInternal = listDisabledInternal.ToArray();
            _disabledEventsArr = listDisabledPublic.ToArray();
            ImGui.Text("Disabled Events:");
            ImGui.ListBox("###DisabledEvents", ref _disabledEventsSelectedItem, _disabledEventsArr);
            if (ImGui.Button("Enable"))
                if (_disabledEventsSelectedItem < _disabledEventsArrInternal.Length)
                {
                    _configuration.BlacklistedEvents.Remove(_disabledEventsArrInternal[_disabledEventsSelectedItem]);
                    _configuration.Save();
                }
        }

        if (ImGui.CollapsingHeader("Event Tester###Testerheader")) EventTester();

        ImGui.Separator();

        if (ImGui.CollapsingHeader("Danger Zone"))
        {
            //todo confirm box
            if (ImGui.Button("Reset Custom Voicelines"))
            {
                PluginServices.Config.CustomShoutcasts.Clear();
                PluginServices.Config.Save();
                PluginServices.ConfigManager.ReloadConfig();
                PluginServices.ChatGui.Print("Reset Custom Voicelines!");
            }

            ImGui.SameLine();
            if (ImGui.Button("Reset Custom Mapping"))
            {
                PluginServices.Config.MappingOverride.Clear();
                PluginServices.Config.Save();
                PluginServices.ConfigManager.ReloadConfig();
                ImGui.OpenPopup("ConfirmWipeMapping");
                PluginServices.ChatGui.Print("Reset Custom Mapping!");
            }
        }
    }

    private void CopyValues(Dictionary<string, string> customVoicelines, Dictionary<string, string> customMappings,
        Dictionary<string, string> customEvents)
    {
        var customStuff = new Dictionary<string, string>
        {
            {"shoutcasts", GetB64(customVoicelines)},
            {"mapping", GetB64(customMappings)},
            {"events", GetB64(customEvents)}
        };
        var b64 = GetB64(customStuff);
        PluginServices.PluginLog.Verbose($"Output: {b64}");
        ImGui.SetClipboardText(b64);
        PluginServices.NotificationManager.AddNotification(new Notification()
        {
            Title = "Copied!",
            Content = "Successfully copied to the clipboard!"
        });
    }

    private string GetB64(object? obj)
    {
        var ser = JsonSerializer.Create();
        var writer = new StringWriter();
        ser.Serialize(writer, obj);
        var str = writer.ToString();
        var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(str));

        return b64;
    }

    private Dictionary<string, string> GetShoutsForShoutcaster(string caster)
    {
        var customShouts = PluginServices.Config.CustomShoutcasts.Where(sh =>
        {
            var sc = PluginServices.ShoutcastRepository.GetShoutcast(sh.Key);
            return sc != null && sc.Shoutcaster.Equals(caster);
        });
        return customShouts.ToDictionary(sh => sh.Key, sh => sh.Value);
    }

    private bool CasterHasChanges(string caster)
    {
        return GetCustomMappingForShoutcaster(caster).Count > 0 || GetShoutsForShoutcaster(caster).Count > 0;
    }

    private Dictionary<string, string> GetCustomMappingForShoutcaster(string caster)
    {
        var dict = new Dictionary<string, string>();
        foreach (var (eventId, mapJson) in PluginServices.Config.MappingOverride)
        {
            var mapList = PluginServices.JsonLoader.ConstructMappingFromJson(mapJson).Where(sh =>
            {
                var sc = PluginServices.ShoutcastRepository.GetShoutcast(sh);
                return sc != null && sc.Shoutcaster.Equals(caster);
            }).ToList();
            if (mapList.Count == 0) continue;

            var toExport = PluginServices.JsonLoader.BuildJsonMapping(eventId, mapList).ToJsonString();
            dict.Add(eventId, toExport);
        }

        return dict;
    }


    private void EventTester()
    {
        ImGui.Text("Event Tester");
        ImGui.TextWrapped(
            "This Simulates these events happening in real pvp, using your configuration settings including announcement frequency and delay. If delay is long or frequency is low, you may not hear voice lines when activating these buttons.");
        var i = 1;
        foreach (var ev in PluginServices.PvPEventBroker.GetPvPEvents())
        {
            if (ImGui.Button(ev.Name))
            {
                try
                {
                    PluginServices.Announcer.ReceivePvPEvent(true, ev);
                    PluginServices.Announcer.ClearQueue();
                }
                catch (Exception e)
                {
                    PluginServices.PluginLog.Error(e, "Issue sending custom event!!!");
                }
            }

            if (i % 4 != 0)
            {
                ImGui.SameLine();
            }

            i++;
        }
    }

    public void Dispose()
    {
    }
}