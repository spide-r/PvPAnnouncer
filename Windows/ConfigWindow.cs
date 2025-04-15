using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using PvpAnnouncer;

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
    }

    public void Dispose() { }

    private int _activeEventsSelectedItem;
    private int _disabledEventsSelectedItem = 0;
    private String[] _activeEventsArr;
    private String[] _disabledEventsArr;
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
        
        if (ImGui.Checkbox("Disabled", ref disabled))
        {
            _configuration.Disabled = disabled;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Metem Muted", ref muted))
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
        
        if (ImGui.Checkbox("The Announcer should comment on people in my Full party", ref fullParty))
        {
            _configuration.WantsFullParty = fullParty;
            _configuration.Save();
        }
        
        if (ImGui.SliderInt("What is the minimum amount of seconds to wait between announcements?", ref cooldown, 6, 30))
        {
            _configuration.CooldownSeconds = cooldown;
            _configuration.Save();
        }
        
        if (ImGui.SliderInt("What percent of events should have an announcement?", ref percent, 1, 100))
        {
            _configuration.Percent = percent;
            _configuration.Save();
        }
        
        //todo: lang
        List<String> list = new List<string>();
        foreach (var ee in PluginServices.ListenerLoader.GetPvPEvents())
        {
            string name = ee.Name;
            if (!blEvents.Contains(name))
            {
                list.Add(ee.Name);

            }
        }
        _activeEventsArr = list.ToArray();
        ImGui.ListBox("Active Events", ref _activeEventsSelectedItem, _activeEventsArr, _activeEventsArr.Length);
        if (ImGui.Button("Prevent Event from Being Announced"))
        {
            _configuration.BlacklistedEvents.Add(_activeEventsArr[_activeEventsSelectedItem]);
            _configuration.Save();
        }
        
        List<String> listDisabled = new List<string>();
        foreach (String ee in blEvents)
        {
            listDisabled.Add(ee);
        }
        _disabledEventsArr = listDisabled.ToArray();
        ImGui.ListBox("Disabled Events", ref _disabledEventsSelectedItem, _disabledEventsArr, _disabledEventsArr.Length);
        if (ImGui.Button("Allow Event to be Announced"))
        {
            _configuration.BlacklistedEvents.Remove(_disabledEventsArr[_disabledEventsSelectedItem]);
            _configuration.Save();
        }
        
        
        
       
        
        
        
    }
}
