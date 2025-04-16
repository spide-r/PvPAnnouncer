using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using PvpAnnouncer;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration _configuration;
    public ConfigWindow() : base(
        "PvPAnnouncer Configuration")
    {

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(500, 425),
            MaximumSize = new Vector2(800, 1000)
        };

        SizeCondition = ImGuiCond.Always;

        _configuration = PluginServices.Config;
        _allEvents = PluginServices.ListenerLoader.GetPvPEvents();
    }

    public void Dispose() { }

    private int _activeEventsSelectedItem;
    private int _disabledEventsSelectedItem = 0;
    private String[] _activeEventsArr;
    private String[] _disabledEventsArr;
    private readonly PvPEvent[] _allEvents;
    public override void Draw()
    {
        var disabled = _configuration.Disabled;
        var muted = _configuration.Muted;
        var lang = _configuration.Language;
        var blEvents = _configuration.BlacklistedEvents;
        var cooldown = _configuration.CooldownSeconds;
        var percent = _configuration.Percent;
        var fem = _configuration.WantsFem;
        var masc = _configuration.WantsMasc;
        var fullParty = _configuration.WantsFullParty;
        var lightParty = _configuration.WantsLightParty;
        var repeatVoiceLine = _configuration.RepeatVoiceLineQueue;
        var repeatEventCommentary = _configuration.RepeatEventCommentaryQueue;
        
        if (ImGui.Checkbox("Disabled", ref disabled))
        {
            _configuration.Disabled = disabled;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Announcer Muted", ref muted))
        {
            _configuration.Muted = muted;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Use Voice Lines with Feminine Pronouns", ref fem))
        {
            _configuration.WantsFem = fem;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Use Voice Lines with Masculine Pronouns", ref masc))
        {
            _configuration.WantsMasc = masc;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("The Announcer should comment on people in my light party", ref lightParty))
        {
            _configuration.WantsLightParty = lightParty;
            _configuration.Save();
        }
        
        /*
        if (ImGui.Checkbox("The Announcer should comment on people in my Full party", ref fullParty))
        {
            _configuration.WantsFullParty = fullParty;
            _configuration.Save();
        }*/
        
        
        ImGui.TextWrapped("What is the minimum amount of seconds to wait between announcements?");
        if (ImGui.SliderInt("(S)", ref cooldown, 1, 30))
        {
            _configuration.CooldownSeconds = cooldown;
            _configuration.Save();
        }
        
        
        ImGui.TextWrapped("What percent of events should have an announcement?");
        if (ImGui.SliderInt("%", ref percent, 1, 100))
        {
            _configuration.Percent = percent;
            _configuration.Save();
        }
        
        
        ImGui.TextWrapped("How Many Unique voice lines should be said before a potential repeat?");
        if (ImGui.SliderInt("# Of Voice lines", ref repeatVoiceLine, 1, 25))
        {
            _configuration.RepeatVoiceLineQueue = repeatVoiceLine;
            _configuration.Save();
        }
        

        ImGui.TextWrapped("How Many Unique events should be commented on before a duplicate happens?");
        if (ImGui.SliderInt("# Of Events", ref repeatEventCommentary, 1, 10))
        {
            _configuration.RepeatEventCommentaryQueue = repeatEventCommentary;
            _configuration.Save();
        }

        ImGui.Text("Announcer Language:");
        if (ImGui.RadioButton("English", lang.Equals("en")))
        {
            _configuration.Language = "en";
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.RadioButton("German", lang.Equals("de")))
        {
            _configuration.Language = "de";
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.RadioButton("French", lang.Equals("fr")))
        {
            _configuration.Language = "fr";
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.RadioButton("Japanese", lang.Equals("ja")))
        {
            _configuration.Language = "ja";
            _configuration.Save();
        }
        
        
        List<String> list = new List<string>();
        foreach (var ee in _allEvents)
        {
            string name = ee.Name;
            if (!blEvents.Contains(name) && !name.Contains("Not Implemented"))
            {
                list.Add(ee.Name);

            }
        }
        _activeEventsArr = list.ToArray();
        ImGui.ListBox("Enabled Events", ref _activeEventsSelectedItem, _activeEventsArr, _activeEventsArr.Length);
        if (ImGui.Button("Disable"))
        {
            _configuration.BlacklistedEvents.Add(_activeEventsArr[_activeEventsSelectedItem]);
            _configuration.Save();
        }
        
        List<String> listDisabled = new List<string>();
        foreach (String ee in blEvents)
        {
            listDisabled.Add(ee);
        }

        foreach (var ee in _allEvents)
        {
            string name = ee.Name;
            if (name.Contains("Not Implemented"))
            {
                listDisabled.Add(ee.Name);

            }
        }
        _disabledEventsArr = listDisabled.ToArray();
        ImGui.ListBox("Disabled Events", ref _disabledEventsSelectedItem, _disabledEventsArr, _disabledEventsArr.Length);
        if (ImGui.Button("Enable"))
        {
            if (!_disabledEventsArr[_disabledEventsSelectedItem].Contains("Not Implemented"))
            {
                _configuration.BlacklistedEvents.Remove(_disabledEventsArr[_disabledEventsSelectedItem]);
                _configuration.Save();
            }
            
        }
        
        
        
       
        
        
        
    }
}
