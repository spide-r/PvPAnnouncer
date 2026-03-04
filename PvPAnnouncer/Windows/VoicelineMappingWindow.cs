using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json.Nodes;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;

namespace PvPAnnouncer.Windows;

public class VoicelineMappingWindow : Window, IDisposable
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
        //todo option to filter mapping by specific character?
        var eventsUserFacingName = new List<string>();
        var eventsInternal = new List<string>();
        foreach (var e in PluginServices.PvPEventBroker.GetPvPEvents())
        {
            var eventId = e.Id;

            eventsUserFacingName.Add(e.Name);
            eventsInternal.Add(eventId);
        }

        ImGui.Text("PvP Event Selector:");
        ImGui.Text("Character Filter");
        var shoutcasterSelection = _shoutcasterSelection;
        var shoutcasters = PluginServices.CasterRepository.GetAttributeList().ToList();

        if (ImGui.Combo("###ShoutcasterFilter", ref shoutcasterSelection, shoutcasters))
            _shoutcasterSelection = shoutcasterSelection;

        ImGui.Separator();
        var pvpEventSelection = _pvpEventSelection;

        if (ImGui.Combo("###PvPEvents", ref pvpEventSelection, eventsUserFacingName))
        {
            _pvpEventSelection = pvpEventSelection;
        }


        var currentShoutMapping =
            PluginServices.EventShoutcastMapping.GetShoutcastList(eventsInternal[pvpEventSelection]);

        var currentShoutsForEvent = PluginServices.ShoutcastRepository.GetShoutcasts()
            .Where(s => currentShoutMapping.Contains(s.Id))
            .Where(s => s.Shoutcaster.Equals(shoutcasters[shoutcasterSelection])).Select(e => e.Id).ToList();

        ImGui.Text("Current Voicelines:");
        if (currentShoutsForEvent.Count == 0)
            ImGui.TextWrapped(
                $"No Voicelines for {shoutcasters[shoutcasterSelection]} are present in this event. Add one?");

        var currentShoutSelection = _currentEventShoutSelection;
        if (ImGui.ListBox("###CurrentShouts", ref currentShoutSelection, currentShoutsForEvent))
        {
            _currentEventShoutSelection = currentShoutSelection;
        }

        if (ImGui.Button("Test Selected Shout###CurrentShoutTest"))
        {
            try
            {
                var sc = PluginServices.ShoutcastRepository.GetShoutcast(currentShoutsForEvent[currentShoutSelection]);
                if (sc == null)
                {
                    PluginServices.PluginLog.Warning($"{currentShoutsForEvent[currentShoutSelection]} not found");
                    return;
                }

                PluginServices.Announcer.PlayAndSendBattleTalkForTesting(sc);
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
                var shoutToRemove = currentShoutsForEvent[currentShoutSelection];
                var eventToRemove = eventsInternal[pvpEventSelection];
                PluginServices.EventShoutcastMapping.RemoveShoutcast(eventToRemove, shoutToRemove);
                Save(eventToRemove);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        ImGui.Text("Available Voicelines:");

        var otherShoutsList = PluginServices.ShoutcastRepository.GetShoutcasts()
            .Where(s => !currentShoutsForEvent.Contains(s.Id))
            .Where(s => s.Shoutcaster.Equals(shoutcasters[shoutcasterSelection])).Select(e => e.Id).ToList();
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
                if (sc == null)
                {
                    PluginServices.PluginLog.Warning($"{otherShoutsList[currentShoutSelection]} not found");
                    return;
                }

                PluginServices.Announcer.PlayAndSendBattleTalkForTesting(sc);
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
                var eventToAdd = eventsInternal[pvpEventSelection];
                PluginServices.PluginLog.Verbose($"Adding {shoutToAdd}, {eventToAdd}");
                PluginServices.EventShoutcastMapping.AddShoutcast(eventToAdd, shoutToAdd);
                Save(eventToAdd);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }
    }

    private void Save(string eventT)
    {
        var j = BuildJsonMapping(eventT, PluginServices.EventShoutcastMapping.GetShoutcastList(eventT));
        PluginServices.Config.AddMappingOverride(eventT, j.ToJsonString());
        PluginServices.Config.Save();
        PluginServices.PluginLog.Verbose("Saved: " + j);
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