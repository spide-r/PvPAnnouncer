using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.ImGuiNotification;
using Newtonsoft.Json;

namespace PvPAnnouncer.Windows;

public class CustomizationWindow: Window, IDisposable
{
    public CustomizationWindow() : base(
        "PvPAnnouncer Customization Window")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        
    }
    public override void Draw()
    {
        ImGui.TextWrapped("Hey Hey! PvPAnnouncer dev here. Thanks for using the testing version! This is a BIG feature. While technically" +
                   " \"finished\", it is very rough around the edges and requires a bit of technical knowledge that I haven't really explained anywhere." +
                   "\nTutorial will be forthcoming. Please feel free to play around with it and ask ANY questions!" +
                   "\nYour questions will be instrumental in making sure that this customization is accessible and easy to use for all.");
        ImGui.Separator();
        ImGui.TextWrapped("Important gotcha. Currently there is no way to manage and delete certain voicelines. I will be adding a window in the future. For now, if you wish to modify ");
        ImGui.Separator();
        ImGui.TextWrapped("In order for this plugin to play a voiceline, it needs an audio file from the game, and a text transcription. While some voice line audio is transcribed neatly, most audio is independent from its transcription. Hence we must go through some steps to create a proper voiceline.");
        ImGui.TextWrapped("Once a voiceline is created, we must map it to an existing event.");
        if (ImGui.Button("Open Voice Line Creation Window"))
        {
            PluginServices.VoicelineCreationWindow.Toggle();
        }
        ImGui.SameLine();
        if (ImGui.Button("Open Voice Line Mapping Window"))
        {
            PluginServices.VoicelineMappingWindow.Toggle();
        }

        ImGui.Separator();
        if (ImGui.Button("Export Custom Voicelines"))
        {
            CopyValues(PluginServices.Config.CustomShoutcasts, [], []);

        }

        ImGui.SameLine();
        
        if (ImGui.Button("Export Voiceline Mapping"))
        {
            CopyValues([], PluginServices.Config.MappingOverride, []);
        }
        ImGui.SameLine();

        if (ImGui.Button("Export All"))
        {
            CopyValues(PluginServices.Config.CustomShoutcasts, PluginServices.Config.MappingOverride, []);
        }

        if (ImGui.Button("Import From Clipboard"))
        {
            var b64Clip = ImGui.GetClipboardText();
            PluginServices.PluginLog.Verbose($"Found {b64Clip}");
            try
            {
                var ser = JsonSerializer.Create();
                var decoded = new string(Encoding.UTF8.GetString(Convert.FromBase64String(b64Clip)));
                PluginServices.PluginLog.Verbose($"Decoded to: {decoded} ");
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(decoded)!;
                if (dict.TryGetValue("shoutcasts", out string? sc))
                {
                    var decodedSc =  new string(Encoding.UTF8.GetString(Convert.FromBase64String(sc)));
                    var deserializedSc = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedSc)!;
                    foreach (var keyValuePair in deserializedSc)
                    {
                        PluginServices.Config.CustomShoutcasts[keyValuePair.Key] = keyValuePair.Value;
                    }
                }
                if (dict.TryGetValue("mapping", out string? mapping))
                {
                    var decodedMap = new string(Encoding.UTF8.GetString(Convert.FromBase64String(mapping)));
                    var deserializedMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedMap)!;
                    foreach (var keyValuePair in deserializedMapping)
                    {
                        PluginServices.Config.MappingOverride[keyValuePair.Key] = keyValuePair.Value;
                    }                
                }
                
                if (dict.TryGetValue("events", out string? events))
                {
                    var decodedEvents =  new string(Encoding.UTF8.GetString(Convert.FromBase64String(events)));
                    var deserializedEvents = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedEvents)!;
                    foreach (var keyValuePair in deserializedEvents)
                    {
                        PluginServices.Config.CustomEvents[keyValuePair.Key] = keyValuePair.Value;
                    }
                    
                }
                PluginServices.Config.Save();
                PluginServices.ConfigManager.ReloadConfig();
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
        if (ImGui.CollapsingHeader("Danger Zone"))
        {
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
        ImGui.Separator();
        if (ImGui.CollapsingHeader("Event Tester###Testerheader"))
        {
            EventTester();

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

    private void EventTester()
    {
        ImGui.Text("Event Tester (Simulates these events happening in real pvp, using your configuration settings - !!!this includes announcement frequency and delay!!!)");
        var i = 1;
        foreach (var pvPEvent in PluginServices.PvPEventBroker.GetPvPEventIDs())
        {
            if (ImGui.Button(pvPEvent))
            {
                try
                {
                    var ev = PluginServices.PvPEventBroker.GetEvent(pvPEvent);
                    if (ev != null)
                    {
                        PluginServices.Announcer.ReceivePvPEvent(true, ev);
                        PluginServices.Announcer.ClearQueue();    
                    }


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