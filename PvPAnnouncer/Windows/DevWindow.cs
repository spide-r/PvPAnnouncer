using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Dalamud.Interface.ImGuiNotification;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.UI;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Components;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Utility;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using Lumina.Excel.Sheets;
using Lumina.Extensions;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl;

namespace PvPAnnouncer.Windows;

public class DevWindow: Window, IDisposable
{
    private List<uint> _orphanedVoLines = [];
    private uint[] _orphanedVoLineArr = [];
    public DevWindow() : base(
        "PvPAnnouncer Dev Window")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        
    }

    //
    // sources of voicelines: 
    // ContentDirectorBattleTalkVO, CutsceneLine
        
    // sources of text:
    // npcyell, cutsceneLine, instancecontenttextdata

    private long _pressedLast = 0;
    private uint _iconId = 0;
    private string _name = "Announcer Name";
    private string _eventId = "UniqueEventId";
    private bool _useIcon = false;
    private uint _npcyell = 0;
    private uint _instanceContentTextDataRow = 0;
    private string _cutsceneLine = "";
    private uint _duration = 1;
    private uint _voLine = 0;
    private uint _style = 6; //0, 6, 7, 11
    public override void Draw()
    {
        ImGui.Text("Configured Voice Language: " + PluginServices.Config.Language); //todo make pretty
        ImGui.Text("Configured Text Language: " + PluginServices.Config.TextLanguage);
        
        if (ImGui.CollapsingHeader("Danger Zone"))
        {
            ShowDangerZone();
        }

        if (ImGui.CollapsingHeader("Announcer Metadata"))
        {
            ShowAnnouncerMetadata();
        }

        if (ImGui.CollapsingHeader("Audio Selection"))
        {
            
            ShowAudioSelection();
            if (ImGui.Button("Clear Selection###AudioSelectionClear"))
            {
                _instanceContentTextDataRow = 0;
                _voLine = 0;
            }
        }

        if (ImGui.CollapsingHeader("Text Selection"))
        {
            ShowTextSelection();
            
            if (ImGui.Button("Clear Selection###TextSelectionClear"))
            {
                _npcyell = 0;
                _cutsceneLine = "";
            }
        }

        var id = _eventId;

        if (ImGui.InputText("Unique Internal Announcement ID", ref id))
        {
            _eventId = id;
        }

        var duration = _duration;
        if (ImGui.SliderUInt("Announcement Duration", ref duration, 1, 10, default, ImGuiSliderFlags.AlwaysClamp))
        {
            _duration = duration;
        }

    
        ShowStyleSelector();
        
        if (ImGui.CollapsingHeader("Object Data"))
        {
            ShowObjectData();
        }

        if (ImGui.Button("Test Current Data"))
        {
            TestData();
        }

        if (ImGui.Button("Save and Write to config"))
        {
            SaveObject();
        }

    }

    private void ShowStyleSelector()
    {
        uint[] ss = [0, 6, 7, 11];
        foreach (var styleInt in ss)
        {
            if (ImGui.RadioButton("Style " + styleInt, _style == styleInt))
            {
                _style = styleInt;
            }
        }
    }

    private void ShowAudioSelection()
    {
        /*
         * ContentDirectorBattleTalk (filter by VoLine existing or not)
         * VOline | Duration | text (if it exists) | play button to hear the thing
         * On apply, set duration and link to the specific row in instancecontentTextdata
         */

        /*
         * CutsceneLine (oh boy)
         * Filter by Character (DO NOT EVER SHOW ALL)
         * Sort by Line Length
         *
         */

        /*
         * Orphaned Vo Lines
         * Show all, play all, select one
         */

        if (ImGui.Button("Content Director Battle Talk"))
        {
            ContentDirectorBattleTalkPopUp();
        }

        if (ImGui.Button("Cutscene Line"))
        {
            CutsceneLinePopup();
        }

        if (ImGui.Button("Orphaned Voice Lines"))
        {
            OrphanedVoLinePopup();
        }
    }

    private void SaveObject()
    {
        //todo impl
    }

    private void TestData()
    {
        //todo build object and fire it off
    }

    private void ShowObjectData()
    {
        ImGui.Text("Object Data:");
        ImGui.Separator();
        if (!_name.Equals(""))
        {
            ImGui.Text("Announcer Name: " + _name);
        }

        if (_useIcon)
        {
            ImGui.Text("Icon: " + _iconId);
        }
        
        ImGui.Text("Duration: " + _duration);
        ImGui.Text("Style: " + _style);
        ImGui.Text("Unique Event ID: " + _eventId);

        if (_npcyell != 0)
        {
            ImGui.Text("NPC Yell: " + _npcyell);
        }

        if (_instanceContentTextDataRow != 0)
        {
            ImGui.Text("Instance Content Text Data: " + _instanceContentTextDataRow);
        }

        if (!_cutsceneLine.Equals(""))
        {
            ImGui.Text("Cutscene Line: " + _cutsceneLine);
        }

        if (_voLine != 0)
        {
            ImGui.Text("Voice Line: " + _voLine);
        }
        ImGui.Separator();
    }
    // -- Announcer Metadata -- 
    //name //sameline Icon browser (limit from X to X+N) (checkbox to say use icon or not)

    // -- Announcer Metadata -- 
        
        
    // voiceline source selector/dropdown (contentDirectorBattleTalk, cutsceneLine, orphaned(change the name) vo-lines w/o )


    // -- Audio Selection --

    // -- Audio Selection --



    // -- Text Selection --

    // display transcription for $LANG
    //play chosen voiceline

    // duration selector - max out at 10s
    // style selector(?)




    private void AddVoicelineToEventsThisIsAPopupButShouldProbablyBeItsOwnWindow()
    {
        //todo
        
        /*
         * Window To add voiceline to events
         * List of events
         * Select events to add it to
         * Edit a mapping json
         */

    }

    private void ActionEventCreatorThisIsAPopupButShouldProbablyBeItsOwnWindow()
    {
        //todo
        
        /*
         * Window to create events tied to specific actions (AllyActionEvent/EnemyActionEvent)
         * Name, internal name
         */

    }
    private void ShowTextSelection()
    {

        var sheet = PluginServices.DataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>();
        var exists = sheet.Flatten().Any(bt => bt.Unknown1 == _voLine);
        if (_voLine != 0 && exists)
        {
            ShowVoLineData();
        } else if (!_cutsceneLine.Equals(""))
        {
            ShowCutsceneLineData();
        }
        else
        {
            if (ImGui.Button("Instance Content Text Data Selector"))
            {
                InstanceContentTextDataPopup();
            }

            if (ImGui.Button("NPC Yell Data Selector"))
            {
                ShowNpcYellSelectionPopup();
            }
        }
        
        //todo display chosen text at end here 
        
        
        // -- Text Selection --
        /*
         * if VOLine set, pull data if exists & show
         * if Cutscene line set, pull data
         * if not selected or found, allow for NPCYell selection
         */

        //dropdown
        //transcription for $LANG (note that its a fallback, not recommended) 


    }

    private void ShowVoLineData()
    {

        var vo = _voLine;
    }

    private void ShowCutsceneLineData()
    {
        var cs = _cutsceneLine;
    }

    private void ShowNpcYellSelectionPopup()
    {
        //todo
    }

    private void LoadAllCutsceneLines()
    {
        //todo - maybe also load this in a file somewhere
    }

    private void ContentDirectorBattleTalkPopUp()
    {
        //todo
    }
    
    private void InstanceContentTextDataPopup()
    {
        //todo
    }

    private void CutsceneLinePopup()
    {
        //todo
    }

    private void OrphanedVoLinePopup()
    {
        //todo
    }
    private void ShowDangerZone()
    {
        ImGui.Text("WARNING! These buttons will take a while and may freeze your game.");
        if (ImGui.Button("Load all Orphaned Voiceover Lines."))
        {
            var sheet = PluginServices.DataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>(); 

            for (uint j = 0; j < 9999999; j++)
            {
                var voLine = $"sound/voice/vo_line/{j}_en.scd";
                if (!PluginServices.DataManager.FileExists(voLine))
                {
                    continue;
                }
                    
                if (sheet.Flatten().All(bt => bt.Unknown1 != j)) // if the VO Line does not exist in sheet, its orphaned so we need to add data
                {
                    _orphanedVoLines.Add(j);
                }
            }
            _orphanedVoLineArr = _orphanedVoLines.ToArray();
        }

        if (ImGui.Button("Load All cutscene lines (DANGER)"))
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (now > (_pressedLast + (3 * 60)))
            {
                _pressedLast = now;
                LoadAllCutsceneLines();
            }
            else
            {
                PluginServices.ChatGui.PrintError("You Must wait 3 minutes to press this button again!");
            }
        }
    }

    private void ShowAnnouncerMetadata()
    {
        var name = _name;
        if (ImGui.InputText("Announcer Name", ref name, 100))
        {
            _name = name;
        }
        ImGui.SameLine();
        var icon = _iconId;
        uint min = 70000;
        uint max = 70010;  //todo change min and max
        if (ImGui.SliderUInt("Announcer Icon", ref icon, min, max, default, ImGuiSliderFlags.AlwaysClamp))
        {
            _iconId = icon;
        }
        ImGui.SameLine();
        var iconImage = PluginServices.TextureProvider.GetFromGameIcon(new GameIconLookup(icon)).GetWrapOrEmpty();
        ImGui.Image(iconImage.Handle, iconImage.Size);
        var useIcon = _useIcon;
        if (ImGui.Checkbox("Use Icon", ref useIcon))
        {
            _useIcon = useIcon;
        }
        ImGui.NewLine();
    }
    public void Dispose()
    {
    }
}