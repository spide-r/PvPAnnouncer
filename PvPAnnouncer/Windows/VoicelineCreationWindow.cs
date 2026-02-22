using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Components;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Lumina.Extensions;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Windows;

public partial class VoicelineCreationWindow: Window, IDisposable
{
    
    private List<string> _orphanedVoLines = [];
    private string[] _orphanedVoLineArr = [];
    private int _voLineSelector = 0;
    private Dictionary<string, List<string>> _cutsceneLines = new();
    public VoicelineCreationWindow() : base(
        "PvPAnnouncer Voiceline Creation")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        LoadAllCutsceneLines();
        LoadAllOrphanedLines();

    }

    //
    // sources of voicelines: 
    // ContentDirectorBattleTalkVO, CutsceneLine
        
    // sources of text:
    // npcyell, cutsceneLine, instancecontenttextdatarow

    private uint _iconId = 0;
    private string _name = "Announcer Name";
    private string _eventId = "UniqueEventId";
    private bool _useIcon = false;
    private uint _npcyell = 0;
    private uint _instanceContentTextDataRow = 0;
    private string _cutsceneLineTag = "";
    private uint _duration = 1;
    private uint _voLine = 0;
    private uint _style = 6; //0, 6, 7, 11
    private string _displayText = "";
    private List<string> _attributes = [];
    private string _attributeToAdd = "";
    private bool _autoFilled = false;
    private bool _useBackup = false;

    private void Reset()
    {
        _iconId = 0;
        _name = "Announcer Name";
        _eventId =  "UniqueEventId";
        _useIcon = false;
        _npcyell = 0;
        _instanceContentTextDataRow = 0;
        _cutsceneLineTag = "";
        _duration = 1;
        _voLine = 0;
        _style = 6;
        _displayText = "";
        _attributes = [];
        _attributeToAdd = "";
        _autoFilled = false;
        _useBackup = false;
    }

    private void ClearTextData()
    {
        _instanceContentTextDataRow = 0;
        _npcyell = 0;
        _displayText = "";
        _useBackup = false;
    }

    private void ClearAudioData()
    {
        _voLine = 0;
        _autoFilled = false;
        _cutsceneLineTag = "";
    }
    
    private void TestData()
    {

        var sc = BuildShoutcast();
        PluginServices.Announcer.PlayForTesting(sc);
    }

    private Shoutcast BuildShoutcast()
    {
        var b = PluginServices.ShoutcastBuilder
            .WithId(_eventId)
            .WithShoutcaster(_name)
            .WithDuration((byte) _duration)
            .WithStyle((byte) _style)
            .WithAttributes(_attributes)
            .WithInstanceContentTextDataRow(_instanceContentTextDataRow)
            .WithCutsceneLine(_cutsceneLineTag)
            .WithContentDirectorBattleTalkVo(_voLine)
            .WithNpcYell(_npcyell);
        if (_useIcon)
        {
            b.WithIcon(_iconId);
        }

        var cfgLang = PluginServices.Config.Language;
        if (_useBackup)
        {
            b.WithTranscription(new Dictionary<string, string> {[cfgLang] = _displayText});
        }

        var sc = b.Build();
        return sc;
    }

    public override void Draw()
    {

        if (ImGui.CollapsingHeader("Step 1: Character Data"))
        {
            ShowCharacterData();
        }

        if (ImGui.CollapsingHeader("Step 2: Audio Selection"))
        {
            
            ShowAudioSelection();
            if (ImGui.Button("Clear Selection###AudioSelectionClear"))
            {
                _instanceContentTextDataRow = 0;
                _voLine = 0;
            }

            if (_voLine != 0)
            {
                ImGui.Text("Selected Voiceover: " + _voLine);

            }
            if (!_cutsceneLineTag.Equals(""))
            {
                ImGui.Text("Selected Cutscene Line: " + _cutsceneLineTag);
            }
        }

        if (ImGui.CollapsingHeader("Step 3: Text Selection"))
        {
            ShowTextSelection();

            if (_autoFilled)
            {
                if (ImGui.Button("Clear Selection###TextSelectionClear"))
                {
                    ClearTextData();
                }
            }
        }

        if (ImGui.CollapsingHeader("Step 4: Announcement Data"))
        {
            ShowAnnouncementMetadata();
        }
        if (ImGui.CollapsingHeader("Step 5: Confirm Everything Looks Good"))
        {
            ShowObjectData();
        }

        if (ImGui.CollapsingHeader("Step 6: Save and Reset"))
        {
            if (ImGui.Button("Save and Write to config"))
            {
                SaveAndRegisterObject();
            }
            ImGuiComponents.HelpMarker("This button saves the voiceline and lets you use it in the mapping window.");
            ImGui.Separator();
            if (ImGui.Button("Reset To Defaults"))
            {
                Reset();
            }
        }
  

    }

    private void ShowAnnouncementMetadata()
    {
        var id = _eventId;

        if (ImGui.InputText("Unique Internal Announcement ID", ref id))
        {
            _eventId = id;
        }
        ImGuiComponents.HelpMarker("This must be unique across all voicelines. If a duplicate is found, one may overwrite the other.");

        var duration = _duration;
        if (ImGui.SliderUInt("Announcement Duration", ref duration, 1, 10, default, ImGuiSliderFlags.AlwaysClamp))
        {
            _duration = duration;
        }
        ImGuiComponents.HelpMarker("This value determines how long text will stay on the screen.");

    
        ImGui.TextWrapped("Attributes:");
        ImGuiComponents.HelpMarker("Attributes are optional fields that act as filters. " +
                                   "If an attribute is not enabled in the configuration, the voiceline will never be shown during gameplay. " +
                                   "This is useful for if the line uses pronouns or calls ingame characters by name.");

        
        var attributeToAdd = _attributeToAdd;
        if(ImGui.InputText("###AddAttrText", ref attributeToAdd))
        {
            _attributeToAdd = attributeToAdd;
        }

        if (ImGui.Button("Add Attribute"))
        {
            if (!attributeToAdd.Trim().IsNullOrEmpty())
            {
                _attributes.Add(attributeToAdd.Trim());
            }

        }
        ImGui.SameLine();

        if (ImGui.Button("Clear Attributes"))
        {
            _attributes.Clear();
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
            ImGui.SameLine();
        }
        ImGuiComponents.HelpMarker("I don't know why, but the game labels shoutcast styles this way.");
        ImGui.NewLine();
    }

    private void ShowAudioSelection()
    {
        ImGui.TextWrapped("You must pick one source of audio. Battle Talk and Cutscene Line link both audio and text, so they are easier to work with.");
        if (ImGui.Button("Battle Talk"))
        {
            ImGui.OpenPopup("CtrPop");

        }
        ImGuiComponents.HelpMarker("Using this is the easiest, as it auto-fills style, icon, and voiceline duration.");
        ContentDirectorBattleTalkPopUp();


        if (ImGui.Button("Cutscene Line"))
        {
            ImGui.OpenPopup("CutscenePop");

        }
        ImGuiComponents.HelpMarker("This contains ALL cutscene lines filtered by character. If nothing is shown, check the \"First Step\" section and press the button.");
        CutsceneLinePopup();


        if (ImGui.Button("Orphaned Voice Lines"))
        {
            if (_orphanedVoLineArr.Length > 1)
            {
                ImGui.OpenPopup("OrphanedPop");
            }
            else
            {
                PluginServices.ChatGui.PrintError("Game data has not yet loaded! Please try again in a few moments.");
            }

        }
        ImGuiComponents.HelpMarker("These voicelines are not connected to any other sheet or transcription. These are the trickiest to use, but have a lot of trust voicelines, other battle NPC's, etc. I will elaborate more on this soon.");
        OrphanedVoLinePopup();

    }

    private void SaveAndRegisterObject()
    {
        var sc = BuildShoutcast();
        var json = PluginServices.JsonLoader.BuildJsonShout(sc);
        PluginServices.Config.AddCustomShoutCast(sc.Id, json.ToJsonString());
        PluginServices.Config.Save();
        PluginServices.ShoutcastRepository.SetShoutcast(sc.Id, sc);
        PluginServices.CasterRepository.RegisterAttribute(sc.Shoutcaster);
        foreach (var scAttribute in sc.Attributes)
        {
            PluginServices.AttributeRepository.RegisterAttribute(scAttribute);

        }
        PluginServices.PluginLog.Verbose("Saved Json: " + json);
        PluginServices.ChatGui.Print($"Saved Shoutcast from {sc.Shoutcaster} with ID {sc.Id}");
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

        if (!_cutsceneLineTag.Equals(""))
        {
            ImGui.Text("Cutscene Line: " + _cutsceneLineTag);
        }

        if (_voLine != 0)
        {
            ImGui.Text("Voice Line: " + _voLine);
        }

        if (_attributes.Count > 0)
        {
            ImGui.Text("Properties:");
            foreach (var property in _attributes)
            {
                ImGui.Text(property + ", ");
                ImGui.SameLine();
            }
        }
        if (ImGui.Button("Test Current Announcement"))
        {
            TestData();
        }
        ImGui.Separator();
    }

    private string _inputTextBackup = "";
    
    private void ShowTextSelection()
    {
        ImGui.TextWrapped("If audio from Cutscene Line or Battle Talk was selected, this will be filled in automatically.");
        var sheet = PluginServices.DataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>();
        var contentDir = sheet.Flatten().FirstOrNull(bt => bt.Unknown1 == _voLine);
        if (_displayText.Equals(""))
        {
            if (_voLine != 0 && contentDir != null)
            {
                PullVoLineData((ContentDirectorBattleTalk) contentDir);
            } else if (!_cutsceneLineTag.Equals(""))
            {
                PullCutsceneLineData();
            }
        }
        else
        {
            if (ImGui.Button("Instance Text Data Selector"))
            {
                ImGui.OpenPopup("ICTDPop");

            }
            ImGuiComponents.HelpMarker("This selects from most instance battle text.");
            InstanceContentTextDataPopup();


            if (ImGui.Button("NPC Yell Data Selector"))
            {
                ImGui.OpenPopup("NPCPop");

            }
            ImGuiComponents.HelpMarker("Trust/Regular NPC Speech Bubbles");
            ShowNpcYellSelectionPopup();
            if (ImGui.CollapsingHeader("I can't find the text in either of the above places!"))
            {
                var inputTextBackup = _inputTextBackup;
                ImGui.TextWrapped("While it is encouraged to use ingame data due to easy translation, " +
                                  "sometimes the text isn't present. Use this field to add the transcription yourself.");
                if (ImGui.InputText("###InputBackup", ref inputTextBackup, 255))
                {
                    _inputTextBackup = inputTextBackup;
                    _displayText = inputTextBackup;
                }

                var backup = _useBackup;
                if (ImGui.Checkbox("Use Backup Text", ref backup))
                {
                    _useBackup = backup;
                }
            }

        }
        
        
        ImGui.Text("Selected Text: " + _displayText);
    }

    private void PullVoLineData(ContentDirectorBattleTalk bt)
    {
        var lang = LanguageUtil.LanguageMap.First(p => p.Value.Equals(PluginServices.Config.Language)).Key;
        var foundEntry = GetInstanceContentTextData(lang).TryGetRow(bt.Text.RowId, out var ctrRow);
        var text = foundEntry ? ctrRow.Text.ToString() : InternalConstants.ErrorContactDev;
        _displayText = text;
    }

    private Dictionary<Language, ExcelSheet<InstanceContentTextData>> _instanceContentTextData = new();
    private ExcelSheet<InstanceContentTextData> GetInstanceContentTextData(Language lang)
    {
        if (_instanceContentTextData.TryGetValue(lang, out var data))
        {
            return data;
        }

        var instanceContent = PluginServices.DataManager.Excel.GetSheet<InstanceContentTextData>(language:lang);
        _instanceContentTextData[lang] = instanceContent;
        return instanceContent;

    }

    private void PullCutsceneLineData()
    {
        _displayText = GetCsLineWithTag(_cutsceneLineTag);

    }

    private readonly Dictionary<string, string> _cSLineTagDict = []; // me when memoization
    private string GetCsLineWithTag(string tag)
    {
        if (_cSLineTagDict.TryGetValue(tag, out var value))
        {
            return value;
        }
        
        
        var splitLine = tag.Split("_");
        var number = splitLine[2];
        var trimmedNumber = number.Substring(0,3);
        var csvName = $"cut_scene/{trimmedNumber}/VoiceMan_{number}";
        var lang = LanguageUtil.LanguageMap.First(p => p.Value.Equals(PluginServices.Config.Language)).Key;
        var cutscene = PluginServices.DataManager.Excel.GetSheet<CutsceneText>(lang, csvName);
        var row = cutscene.FirstOrNull(r => r.MessageTag.ToString().Equals(tag));
        var dialogue = InternalConstants.ErrorContactDev;
        if (row != null)
        {
            dialogue = row.Value.Dialogue.ToMacroString(); 
        }
        dialogue = CutsceneNameRemovalRegex().Replace(dialogue, ""); // any dialogue with (- text_here -) at the start will override the name shown in battletalk
        _cSLineTagDict[tag] = dialogue;
        return dialogue;
    }
    [GeneratedRegex(@"^\(-.*-\)")]
    private static partial Regex CutsceneNameRemovalRegex();

    private string _npcYellFilter = "";
    private Dictionary<Language, List<NpcYell>> _npcYellMemo = [];

    private List<NpcYell> PullNpcYellMemo(Language lang)
    {
        if(_npcYellMemo.TryGetValue(lang, out var cached))
        {
            return cached;
        }
        _npcYellMemo[lang] = PluginServices.DataManager.Excel.GetSheet<NpcYell>(language: lang).ToList();
        return _npcYellMemo[lang];

    }
    private void ShowNpcYellSelectionPopup()
    {
        var lang = LanguageUtil.LanguageMap.First(p => p.Value.Equals(PluginServices.Config.Language)).Key;

        var filter = _npcYellFilter;
     
        ImGui.SetNextWindowSize(new Vector2(300, 300), ImGuiCond.FirstUseEver);
        if (ImGui.BeginPopupModal("NPCPop"))
        {
            if (ImGui.InputText("Filter###NPCYellFilter", ref filter, 300))
            {
                _npcYellFilter = filter;
            }
            if (ImGui.BeginTable("NPCYell###NPCYellTable", 2,
                    ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingStretchProp))
            {
                ImGui.TableSetupColumn("Text", ImGuiTableColumnFlags.WidthStretch);
                ImGui.TableSetupColumn("Button", ImGuiTableColumnFlags.WidthFixed);
                ImGui.TableHeadersRow();
                var yells = PullNpcYellMemo(lang);
                foreach (var y in yells)
                {
                    var text =  y.Text.ToString();
                    if (!_npcYellFilter.Equals(""))
                    {
                        if (!text.Contains(_npcYellFilter))
                        {
                            continue;
                        }
                    }
                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();
                    ImGui.Text(text);
                    ImGui.TableNextColumn();
                    if (ImGui.Button("Select###SelectYell" + y.RowId))
                    {
                        ClearTextData();
                        _npcyell = y.RowId;
                        _displayText = text;
                        ImGui.CloseCurrentPopup();
                    }
                }
                ImGui.EndTable();
            }

            ImGui.EndPopup();
        }
    }
    private string _textDataFilter = "";

    private void InstanceContentTextDataPopup()
    {
        var lang = LanguageUtil.LanguageMap.First(p => p.Value.Equals(PluginServices.Config.Language)).Key;
        
        ImGui.SetNextWindowSize(new Vector2(300, 300), ImGuiCond.FirstUseEver);
        if (ImGui.BeginPopupModal("ICTDPop"))
        {
            var filter = _textDataFilter;
            if (ImGui.InputText("Filter###TextDataFilter", ref filter, 300))
            {
                _textDataFilter = filter;
            }
            if (ImGui.BeginTable("Text Data###TextDataTable", 2,
                    ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingStretchProp))
            {
                ImGui.TableSetupColumn("Text", ImGuiTableColumnFlags.WidthStretch);
                ImGui.TableSetupColumn("Button", ImGuiTableColumnFlags.WidthFixed);
                ImGui.TableHeadersRow();
                var textData = GetInstanceContentTextData(lang);
                foreach (var td in textData)
                {
                    var text =  td.Text.ToString();
                    if (!_textDataFilter.Equals(""))
                    {
                        if (!text.Contains(_textDataFilter))
                        {
                            continue;
                        }
                    }
                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();
                    ImGui.TextWrapped(text);
                    ImGui.TableNextColumn();
                    if (ImGui.Button("Select###SelectTextData" + td.RowId))
                    {
                        ClearTextData();
                        _displayText = text;
                        _instanceContentTextDataRow = td.RowId;
                        ImGui.CloseCurrentPopup();
                    }
                }
                ImGui.EndTable();
            }

            ImGui.EndPopup();
        }
    }
    private void LoadAllCutsceneLines()
    {
        PluginServices.PluginLog.Verbose("Started Loading all Cutscene Lines! ");
        //todo make a program that scans through lumina to create this
        _cutsceneLines = PluginServices.JsonLoader.LoadCutsceneLines();
        PluginServices.PluginLog.Verbose("Finished Loading all Cutscene Lines!");
    }

    private string _contentDirectorFilter = "";
    private readonly ExcelSheet<InstanceContentTextData> _ictd = PluginServices.DataManager.Excel.GetSheet<InstanceContentTextData>();

    private List<ContentDirectorBattleTalk> _ctrList = [];
    private List<ContentDirectorBattleTalk> PullCTRMemo()
    {
        if (_ctrList.Count > 0)
        {
            return _ctrList;
        }
        
        _ctrList = PluginServices.DataManager.Excel.GetSubrowSheet<ContentDirectorBattleTalk>().Flatten().ToList();
        return _ctrList;
    }
    private void ContentDirectorBattleTalkPopUp()
    {
        ImGui.SetNextWindowSize(new Vector2(600, 600), ImGuiCond.FirstUseEver);
        if (ImGui.BeginPopupModal("CtrPop"))
        {
              
            var filter = _contentDirectorFilter;
            if (ImGui.InputText("Filter###CtrFilter", ref filter, 300))
            {
                _contentDirectorFilter = filter;
            }
            if (ImGui.BeginTable("Content DirectorBattleTalk###ContentDirectorBattleTalk", 5,
                    ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingStretchProp))
            {
                ImGui.TableSetupColumn("Text", ImGuiTableColumnFlags.WidthStretch);
                ImGui.TableSetupColumn("Style", ImGuiTableColumnFlags.WidthFixed);
                ImGui.TableSetupColumn("Icon", ImGuiTableColumnFlags.WidthFixed);
                ImGui.TableSetupColumn("VoLine", ImGuiTableColumnFlags.WidthFixed);
                ImGui.TableSetupColumn("Button", ImGuiTableColumnFlags.WidthFixed);
                ImGui.TableHeadersRow();

                foreach (var td in PullCTRMemo())
                {
                    var rId = td.Text.RowId;
                    var foundEntry = _ictd.TryGetRow(rId, out var ctrRow);
                    var text = InternalConstants.ErrorContactDev;
                    if (foundEntry)
                    {
                        text = ctrRow.Text.ToString();
                    }
                    var voLine = td.Unknown1;
                    var style = td.Unknown4;
                    var icon = td.Unknown0;
                    var duration = td.Unknown3;
                    if (!_contentDirectorFilter.Equals(""))
                    {
                        if (!text.Contains(_contentDirectorFilter))
                        {
                            continue;
                        }
                    }

                    if (voLine == 0)
                    {
                        continue;
                    }
                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();
                    ImGui.TextWrapped(text);
                    ImGui.TableNextColumn();
                    ImGui.Text(style.ToString());
                    ImGui.TableNextColumn();
                    ImGui.Text(icon.ToString());
                    ImGui.TableNextColumn();
                    ImGui.Text(voLine.ToString());
                    ImGui.TableNextColumn();
                    if (ImGui.Button("Select###SelectCtr" + voLine))
                    {
                        ClearAudioData();
                        ClearTextData();
                        _autoFilled = true;
                        _voLine = voLine;
                        if (style != 0)
                        {
                            _style = style;
                        }

                        if (icon != 0)
                        {
                            _iconId = icon;
                        }

                        if (duration != 0)
                        {
                            _duration = duration;
                        }
                    
                        ImGui.CloseCurrentPopup();
                    }
                }
                ImGui.EndTable();
            }

            ImGui.EndPopup();
        }
    }

    private string _chosenChar = "";
    private string _cutsceneLineFilter = "";
    private void CutsceneLinePopup()
    {
        
        ImGui.SetNextWindowSize(new Vector2(300, 300), ImGuiCond.FirstUseEver);
        if (ImGui.BeginPopupModal("CutscenePop"))
        {
           if (ImGui.BeginCombo("Character Picker", _chosenChar))
           {
               //todo character filter / searcher + explanation why everything is in caps
               foreach (var character in _cutsceneLines.Keys)
               {
                   bool selected = _chosenChar.Equals(character);
                   if (ImGui.Selectable(character, selected))
                   {
                       _chosenChar = character;
                   }
               }
               ImGui.EndCombo();
           }
        
           if (_chosenChar.Equals(""))
           {
               ImGui.Text("Select A Character!");
           }
           else
           {
               var filter = _cutsceneLineFilter;
               if (ImGui.InputText("Filter###CutsceneLineFilter", ref filter, 300))
               {
                   _cutsceneLineFilter = filter;
               }
               if (ImGui.BeginTable("Character Cutscene Lines", 2,
                       ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingStretchProp))
               {
                   ImGui.TableSetupColumn("Text", ImGuiTableColumnFlags.WidthStretch);
                   ImGui.TableSetupColumn("Button", ImGuiTableColumnFlags.WidthFixed);

                   ImGui.TableHeadersRow();
                   foreach (var cutsceneLineTag in _cutsceneLines[_chosenChar])
                   {
                           
                       var text = GetCsLineWithTag(cutsceneLineTag);
                       if (!_cutsceneLineFilter.Equals(""))
                       {
                           if (!text.Contains(_cutsceneLineFilter))
                           {
                               continue;
                           }
                       }
                       
                       if (text.Equals("No subtitles, report if displayed."))
                       {
                           continue;
                       }
                       if (text.Equals(""))
                       {
                           text = "Untranslated Text! Contact the PvPAnnouncer developer if you wish to contribute!";
                       }
                       

                       ImGui.TableNextRow();
                       ImGui.TableNextColumn();
                   
                       ImGui.TextWrapped(text);

                       ImGui.TableNextColumn();
                       
                       if (ImGui.Button("Select###Select" + cutsceneLineTag))
                       {
                           ClearAudioData();
                           ClearTextData();
                           _cutsceneLineTag = cutsceneLineTag;
                           ImGui.CloseCurrentPopup();
                       }
                   }
                   ImGui.EndTable();
               }
           }
           ImGui.EndPopup();    
        }
    }

    private void OrphanedVoLinePopup()
    {
        var selector = _voLineSelector;
        ImGui.SetNextWindowSize(new Vector2(300, 300), ImGuiCond.FirstUseEver);
        if (ImGui.BeginPopupModal("OrphanedPop"))
        {
            
            ImGui.ListBox("Voice Lines###OrphanedVoLinesList", ref _voLineSelector, _orphanedVoLineArr);
            if (ImGui.Button("Play###OrphanedVoLinesPlay"))
            {
                PluginServices.SoundManager.PlaySound(GetVoLineToPlay(selector));

            }
            ImGui.Separator();
            if (ImGui.Button("Go Back And Play###OrphanedVoLinesBackward"))
            {
                _voLineSelector = selector - 1;
                PluginServices.SoundManager.PlaySound(GetVoLineToPlay(selector-1));
            }
            ImGui.SameLine();
            if (ImGui.Button("Go Forward And Play###OrphanedVoLinesForward"))
            {
                _voLineSelector = selector + 1;
                PluginServices.SoundManager.PlaySound(GetVoLineToPlay(selector+1));
            }

            var currentVo = _orphanedVoLineArr[selector];
            if (ImGui.Button("Select Voiceline " + currentVo + "###OrphanedVoLinesSelect" + currentVo))
            {
                ClearAudioData();
                ClearTextData();
                _voLine = Convert.ToUInt32(currentVo);
                ImGui.CloseCurrentPopup();
            }
        
            ImGui.EndPopup();
        }

    }

    private string GetVoLineToPlay(int selector)
    {
        return "sound/voice/vo_line/" + _orphanedVoLineArr[selector] + "_" +
                            PluginServices.Config.Language + ".scd";
    }
    private void LoadAllOrphanedLines()
    {
        var sheet = PluginServices.DataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>();
        var all = sheet.Flatten().ToList();
        PluginServices.PluginLog.Verbose("Starting to load all orphaned lines!");
        Task.Run( () =>
        {
            var results = new List<string>();
            for (uint j = 0; j < 9999999; j++)
            {
                var voLine = $"sound/voice/vo_line/{j}_en.scd";
                if (!PluginServices.DataManager.FileExists(voLine))
                {
                    continue;
                }

                if (all.All(bt => bt.Unknown1 != j)) 
                    // if the VO Line does not exist in sheet, its orphaned so we need to add data
                {
                    results.Add(j.ToString());
                }
            }

            PluginServices.Framework.RunOnFrameworkThread(() =>
            {
                _orphanedVoLines = results;
                _orphanedVoLineArr = _orphanedVoLines.ToArray();
                PluginServices.PluginLog.Verbose("Finished loading all orphaned lines!");
            });
        });

    }

    private void ShowCharacterData()
    {
        var name = _name;
        ImGui.Text("Name:");
        if (ImGui.InputText("###Announcer Name", ref name, 100))
        {
            _name = name;
        }
        var icon = _iconId;
        uint min = 73001;
        uint max = 73287;  
        ImGui.Text("Icon: ");
        if (ImGui.SliderUInt("###AnnouncerIcon", ref icon, min, max, default, ImGuiSliderFlags.AlwaysClamp))
        {
            _iconId = icon;
        }
        ImGui.SameLine();
        if (ImGui.ArrowButton("###GoDownIcon", ImGuiDir.Left))
        {
            if (_iconId < min)
            {
                _iconId = min;
            }
            _iconId = icon - 1;
        }
        ImGui.SameLine();
        if (ImGui.ArrowButton("###GoUpIcon", ImGuiDir.Right))
        {

            if (_iconId > max)
            {
                _iconId = max;
            }
            _iconId = icon + 1;
        }
        


        try
        {
            var iconImage = PluginServices.TextureProvider.GetFromGameIcon(new GameIconLookup(icon)).GetWrapOrDefault();
            if (iconImage != null)
            {
                ImGui.Image(iconImage.Handle, new Vector2(250, 210));
            }
        }
        catch (Exception)
        {
            ImGui.Text("Issue rendering icon!");
        }
        var useIcon = _useIcon;
        ImGui.NewLine();
        if (ImGui.Checkbox("Use Icon", ref useIcon))
        {
            _useIcon = useIcon;
        }
        ImGuiComponents.HelpMarker("Be sure to check this box if you wish for the icon to be used.");
        ImGui.NewLine();
        ShowStyleSelector();

    }
    public void Dispose()
    {
    }
}