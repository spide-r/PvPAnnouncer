using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json.Nodes;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;

namespace PvPAnnouncer.Windows;

public class VoicelineMappingWindow: Window, IDisposable
{
    public VoicelineMappingWindow() : base(
        "PvPAnnouncer Mapping Window")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        
    }
    
    private int _pvpEventSelection = 0;
    private int _currentEventShoutSelection = 0;
    private int _otherShouts = 0;
    private int _shoutcasterSelection = 0;
    public override void Draw()
    {
        var events = PluginServices.EventShoutcastMapping.GetAllEvents();
        ImGui.Text("PvP Event Selector:");
        var pvpEventSelection = _pvpEventSelection;

        if(ImGui.Combo("###PvPEvents", ref pvpEventSelection, events))
        {
            _pvpEventSelection = pvpEventSelection;
        };
        

        var currentShouts = PluginServices.EventShoutcastMapping.GetShoutcastList(events[pvpEventSelection]);

        var shoutcasters = PluginServices.CasterRepository.GetAttributeList().ToList();
        ImGui.Text("Current Voicelines:");
        var currentShoutSelection = _currentEventShoutSelection;
        if (ImGui.ListBox("###CurrentShouts", ref currentShoutSelection, currentShouts))
        {
            _currentEventShoutSelection = currentShoutSelection;
        }
        if (ImGui.Button("Test Selected Shout###CurrentShoutTest"))
        {
            try
            {
                var sc = PluginServices.ShoutcastRepository.GetShoutcast(currentShouts[currentShoutSelection]);
                PluginServices.Announcer.PlayForTesting(sc);
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }
        ImGui.SameLine();
        if (ImGui.Button("Remove Voiceline"))
        {
            try
            {
                var shoutToRemove = currentShouts[currentShoutSelection];
                var eventToRemove = events[pvpEventSelection];
                PluginServices.EventShoutcastMapping.RemoveShoutcast(eventToRemove, shoutToRemove);
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }
        ImGui.Text("Available Voicelines:");
        ImGui.Text("Filter");
        var shoutcasterSelection = _shoutcasterSelection;
        if (ImGui.Combo("###ShoutcasterFilter",  ref shoutcasterSelection, shoutcasters))
        {
            _shoutcasterSelection = shoutcasterSelection;
        }
        
        var otherShoutsList = PluginServices.ShoutcastRepository.GetShoutcasts()
            .Where(s => !currentShouts.Contains(s.Id)).Where(s => s.Shoutcaster.Equals(shoutcasters[shoutcasterSelection])).Select(e => e.Id).ToList();
        var otherShoutsSelection = _otherShouts;
        
        if (ImGui.ListBox("###OtherShouts", ref otherShoutsSelection, otherShoutsList))
        {
            _otherShouts = otherShoutsSelection;
        }
        if (ImGui.Button("Test Selected Shout###OtherShoutTest"))
        {
            try
            {
                var sc = PluginServices.ShoutcastRepository.GetShoutcast(otherShoutsList[otherShoutsSelection]);
                PluginServices.Announcer.PlayForTesting(sc);
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }
        
        ImGui.SameLine();
        if (ImGui.Button("Add Voiceline"))
        {
            try
            {
                var shoutToAdd = otherShoutsList[otherShoutsSelection];
                var eventToAdd = events[pvpEventSelection];
                PluginServices.PluginLog.Verbose($"Adding {shoutToAdd}, {eventToAdd}");
                PluginServices.EventShoutcastMapping.AddShoutcast(eventToAdd, shoutToAdd);
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }
        

        if (ImGui.Button("Save and Write to config"))
        {
            try
            {
                var eventToAdd = events[pvpEventSelection];
                var j = BuildJsonMapping(eventToAdd, PluginServices.EventShoutcastMapping.GetShoutcastList(eventToAdd));
                PluginServices.Config.AddMappingOverride(eventToAdd, j.ToJsonString());
                PluginServices.Config.Save();
                PluginServices.PluginLog.Verbose("Saved: " + j);
                PluginServices.ChatGui.Print($"Saved shoutcast mapping for {eventToAdd}!");
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }
    }
    
    private JsonObject BuildJsonMapping(string eventId, List<string> shouts)
    {
        var j = new JsonObject
        {
            ["eventId"] = eventId
        };
        var shoutsArray = new JsonArray();

        foreach (var shout in shouts.Where(shout => !shout.Equals("")))
        {
            shoutsArray.Add(shout);
        }

        if (shoutsArray.Count > 0)
        {
            j["shouts"] = shoutsArray;
        }
        return j;
    }
    public void Dispose()
    {
    }
}