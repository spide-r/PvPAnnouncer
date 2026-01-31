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
using Dalamud.Interface.Utility;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using Lumina.Excel.Sheets;
using Lumina.Extensions;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Windows;

public class DevWindow: Window, IDisposable
{
    private List<string> _voLines = [];
    private string[] _voLineArr = [];
    public DevWindow() : base(
        "PvPAnnouncer Dev Window")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        
/*#if DEBUG
        for (uint j = 0; j < 9999999; j++)
        {
            var voLine = $"sound/voice/vo_line/{j}_en.scd";
            if (PluginServices.DataManager.FileExists(voLine))
            {
                _voLines.Add(j.ToString());
            }
        }
        _voLineArr = _voLines.ToArray();
#endif*/
    }

    private long ll = 0L;
    private string icon = "";
    private string ss = "";
    private string _name = "";
    private string _voicelineTranscription = "";
    private long _duration = 0L;
    private bool hide = false;
    private string _textData = "";
    private int _voLineSelector = 0;
    private string _tag = "";
    private int _expac = 3;

    
    public override void Draw()
    {

        var tag = _tag;
        var expac = _expac;
        if(ImGui.InputText("Tag", ref tag))
        {
            _tag = tag;
        }

        if (ImGui.InputInt("Expac", ref expac))
        {
            _expac = expac;
        }

        if (ImGui.Button("Pull the lever kronk"))
        {
            /*
             * File: ./csv/en/cut_scene/050/VoiceMan_05001.csv
            Header: key,0,1
            Match: 121,"TEXT_VOICEMAN_05001_001170_EMETSELCH","(SIGH)"
            TEXT_VOICEMAN_05001_001170_EMETSELCH
            text:  cut_scene/050/VoiceMan_05001/
            audio: cut/ex3/sound/voicem/voiceman_05001/vo_voiceman_05001_001170_m_en.scd


            // step 1: get the text transcription
            // step 2: convert text address into cutscene address (how do we check per-expac??? - we should probably just add a param)
            // step 3: add cutscene audio address

            //step N: make another function that creates from voiceline???? maybe not
            */
            var line = tag;
            var ex = 3;
            
            
            /*
            var splitLine = line.Split("_");
            var number = splitLine[2];
            var secondNumber = splitLine[3];
            var trimmedNumber = number.Substring(0,3);
            var csvName = $"cut_scene/{trimmedNumber}/VoiceMan_{number}";
            var cutscene = PluginServices.DataManager.GetExcelSheet<CutsceneText>(name: csvName);
            var audio = $"cut/ex{ex}/sound/voicem/voiceman_{number}/vo_voiceman_{number}_{secondNumber}_m";
            
            var row = cutscene.FirstOrNull(r => r.MessageTag.ExtractText().Equals(line));
            var dialogue = InternalConstants.ErrorContactDev;
            if (row != null)
            {
                dialogue = row.Value.Dialogue.ExtractText();
            }
            dialogue = Regex.Replace(dialogue, @"^\(-.*-\)", ""); // any dialogue with (- text_here -) at the start will override the name shown in battletalk
            */


            var bt = PluginServices.BattleTalkFactory.CreateFromCutsceneLine("Emet Selch", 0, 5, tag, [], 73256);
            PluginServices.Announcer.SendBattleTalk(bt);
            PluginServices.Announcer.PlaySound(bt.Path + "en.scd");
            
        }
        var l = ll;
        var ic = icon;
        if(ImGui.InputText("Icon###Icon", ref ic))
        {
            icon = ic;
        }

        if (ImGui.Button("Test Icon"))
        {
            PluginServices.Announcer.SendBattleTalk(new BattleTalk("Unknown", 8291265, 2, "Asdf", [], Convert.ToUInt32(icon)));

        }
        var s = ss;
        ImGui.Text("Play A Sound path");
        if (ImGui.InputText("###SoundPath", ref s))
        {
            ss = s;

        }
        ImGui.SameLine();
        if (ImGui.Button("Play###SoundPathButton"))
        {
            PluginServices.Announcer.PlaySound(s);
        }

        ImGui.Text("Play sound/voice/vo_line/"+ l + "_en.scd");
        if(ImGui.InputScalar("###SoundLong", ref l, 1L, 1L))
        {
            ll = l;
        }
        ImGui.SameLine();
        if (ImGui.Button("Play###VoLineButton"))
        {
           play(ll);
        }
        var h = hide;
        if (ImGui.Checkbox("Hide Battle Talk", ref h))
        {
            hide = h;
        }
        ImGui.Separator();
        ImGui.Text("Last Action Used: " + PluginServices.PvPEventBroker.GetLastAction());
        if (ImGui.Button("Get Statuses"))
        {
            if (PluginServices.ObjectTable.LocalPlayer != null)
            {
                foreach (var status in PluginServices.ObjectTable.LocalPlayer.StatusList)
                {
                    PluginServices.PluginLog.Verbose(status.StatusId + " " + status.GameData.Value.Name.ToString());
                }
            }
       
        }
        ImGui.Separator();
        var n = _name;
        if (ImGui.InputText("Name", ref n))
        {
            _name = n;
        }
        var vlTranscription = _voicelineTranscription;
        if (ImGui.InputText("Voice Line Transcription", ref _voicelineTranscription))
        {
            _voicelineTranscription = vlTranscription;
        }

        var textData = _textData;
        if (ImGui.InputText("Sheet Line number", ref _textData))
        {
            _textData = textData;
        }

        ImGui.ListBox("Voice Lines", ref _voLineSelector, _voLineArr);
        if (ImGui.Button("Play##dumblist"))
        {
            PluginServices.SoundManager.PlaySound("sound/voice/vo_line/"+ _voLineArr[_voLineSelector] + "_en.scd");

        }
        ImGui.Separator();
        var d = _duration;
        if (ImGui.Button("Go Back And Play"))
        {
            _voLineSelector = _voLineSelector - 1;
            PluginServices.SoundManager.PlaySound("sound/voice/vo_line/"+ _voLineArr[_voLineSelector] + "_en.scd");
        }
        ImGui.SameLine();
        if (ImGui.Button("Go Forward And Play"))
        {
            _voLineSelector = _voLineSelector + 1;
            PluginServices.SoundManager.PlaySound("sound/voice/vo_line/"+ _voLineArr[_voLineSelector] + "_en.scd");
            
        }
        if (ImGui.InputScalar("Duration", ref d, 1L, 1L))
        {
            _duration = d;
        }

        if (ImGui.Button("Save This"))
        {
            var argChosen = "";
            if (!vlTranscription.Equals(""))
            {
                argChosen = vlTranscription;
            }

            if (!textData.Equals(""))
            {
                argChosen = textData;
            }
            PluginServices.Config.Dev_VoLineList.Add($" public static readonly BattleTalk VO{_voLineArr[_voLineSelector]} new BattleTalk({_name.Trim()}, \"{_voLineArr[_voLineSelector]}\", {_duration}, {argChosen}, GetPersonalization([Personalization.{_name}Announcer])); // {_voicelineTranscription}");
            PluginServices.Config.Save();
            _voicelineTranscription = "";
            _textData = "";
        }
        
        ImGui.Text("Events: ");
        var i = 1;
        foreach (var pvPEvent in PluginServices.ListenerLoader.GetPvPEvents())
        {
            if (ImGui.Button(pvPEvent.Name))
            {
                try
                {
                    PluginServices.Announcer.ReceivePvPEvent(true, pvPEvent);
                    PluginServices.Announcer.ClearQueue();
                }
                catch (Exception e)
                {
                    PluginServices.PluginLog.Error(e.Message);
                }
         
            }

            if (i % 4 != 0)
            {
                ImGui.SameLine();
            }

            i++;
        }
        
    }

    private void play(long l)
    {
        PluginServices.SoundManager.PlaySound("sound/voice/vo_line/"+ l + "_en.scd");
        if (!hide)
        {
            ;
            PluginServices.Announcer.SendBattleTalk(PluginServices.BattleTalkFactory.CreateFromContentDirectorBattleTalk("Unknown", (uint) l, []));
        }
    }
    
    public void Dispose()
    {
    }
}