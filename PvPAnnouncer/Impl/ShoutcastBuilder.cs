using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dalamud.Plugin.Services;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Excel.Exceptions;
using Lumina.Excel.Sheets;
using Lumina.Extensions;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.impl;

public partial class ShoutcastBuilder(IDataManager dataManager): IShoutcastBuilder
{
    private Shoutcast _instance = NewShoutcast();
    
    //todo make the config finally let you change the language of the text as well as the audio 
    
    public Shoutcast Build()
    {
        
        var result = _instance;
        //todo check and warn for screwed up shoutcast obj
        if (result.Transcription.Keys.Count == 0) //no manual text transcription
        {
            if ( result is {ContentDirectorBattleTalkVo: 0, InstanceContentTextDataRow: 0, NpcYell: 0, CutsceneLine: ""})
            {
                PluginServices.PluginLog.Error($"No transcription or sheet entry found for {result.Id}");
                return NewShoutcast();
            } 
            if (result.ContentDirectorBattleTalkVo != 0)
            {
                /*
                    name: ContentDirectorBattleTalk
                    fields:
                    - name: Unknown0 -> BattleTalkIcon
                    - name: Unknown1 -> Voiceover
                    - name: Text
                    type: link
                    targets: [InstanceContentTextData]
                    - name: Unknown3 -> BattleTalkDuration
                    - name: Unknown4 -> BattleTalkStyle
                */
                result.SoundPath = "sound/voice/vo_line/" + result.ContentDirectorBattleTalkVo;
                var d = GetContentDirectorBattleTalkAllLanguages(result.ContentDirectorBattleTalkVo);
                if (d.Keys.Count > 0) // record exists in the sheets - attempt to set the other values
                {
                    var sheet = dataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>(); 

                    var t = sheet.SelectMany(row => row)

                        .FirstOrDefault(bt => bt.Unknown1 == result.ContentDirectorBattleTalkVo);
                    result.Icon = t.Unknown0;
                    result.Style = t.Unknown4;
                    result.Duration = t.Unknown3;
                    result.Transcription = d;
                }
            
            }
            if (result.InstanceContentTextDataRow != 0)
            {
                result.Transcription = GetInstanceContentTextDataAllLang(result.InstanceContentTextDataRow);
            }
            
            if (result.NpcYell != 0)
            {
                var d = GetNpcYellAllLang(result.NpcYell);
                var sheet = dataManager.GetExcelSheet<NpcYell>();
                var foundEntry = sheet.TryGetRow(result.NpcYell, out var row);
                var duration = foundEntry ? row.BalloonTime : 5;
                
                result.Duration = (byte) duration;
                result.Transcription = d;
            }
        }

        if (result.CutsceneLine is not "")
        {
            PluginServices.PluginLog.Verbose("Csline: " + result.CutsceneLine);
            var expac = 69; //todo this is going to fail until you can fiddle with what that ex(N) means
            var splitLine = result.CutsceneLine.Split("_");
            var number = splitLine[2];
            var secondNumber = splitLine[3];
            var audio = $"cut/ex{expac}/sound/voicem/voiceman_{number}/vo_voiceman_{number}_{secondNumber}_m";
            result.SoundPath = audio;
            var d = GetCutsceneLineAllLang(result.CutsceneLine);
            result.Transcription = d;
        }
        
        if (!dataManager.FileExists(result.GetShoutcastSoundPathWithLang("ja")))
        {
            PluginServices.PluginLog.Error($"No sound path found for shoutcast {result.Id}: {result.GetShoutcastSoundPathWithLang("ja")}");
           // return NewShoutcast();
        }

        _instance = NewShoutcast();
        return result;
    }
    private readonly Language[] _langList =
    [
        Language.English, Language.French, Language.German, 
        Language.Japanese, Language.ChineseSimplified, Language.ChineseTraditional, Language.Korean
    ];
    private Dictionary<string, string> GetCutsceneLineAllLang(string tag)
    {
        var splitLine = tag.Split("_");
        var number = splitLine[2];
        var trimmedNumber = number.Substring(0,3);
        var csvName = $"cut_scene/{trimmedNumber}/VoiceMan_{number}";
        Dictionary<string, string> dict = new Dictionary<string, string>();
        
        foreach (var lang in _langList)
        {
            var langStr = LanguageUtil.GetLanguageStr(lang);
            try
            {
                var cutscene = PluginServices.DataManager.Excel.GetSheet<CutsceneText>(lang, csvName);
                var row = cutscene.FirstOrNull(r => r.MessageTag.ExtractText().Equals(tag));
                var dialogue = InternalConstants.ErrorContactDev;
                if (row != null)
                {
                    dialogue = row.Value.Dialogue.ExtractText();
                }
                dialogue = CutsceneNameRemovalRegex().Replace(dialogue, ""); // any dialogue with (- text_here -) at the start will override the name shown in battletalk
                dict[langStr] = dialogue;

            }
            catch (UnsupportedLanguageException)
            {
                PluginServices.PluginLog.Verbose($"Attempted to pull Cutscene Lang for {langStr} and failed due to unsupported language");   
            }
           

        }
        return dict;
    }
    
    private Dictionary<string, string> GetNpcYellAllLang(uint yell)
    {

        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var lang in _langList)
        {
            var langStr = LanguageUtil.GetLanguageStr(lang);
            try
            {
                var sheet = PluginServices.DataManager.Excel.GetSheet<NpcYell>(language: lang);
                var foundEntry = sheet.TryGetRow(yell, out var row);
                var text = foundEntry ? row.Text.ExtractText() : InternalConstants.ErrorContactDev;
                dict[langStr] = text;

            }
            catch (UnsupportedLanguageException)
            {
                PluginServices.PluginLog.Verbose($"Attempted to pull NPC Yell for {langStr} and failed due to unsupported language");   
            }
           

        }
        return dict;
    }
    
    private Dictionary<string, string> GetContentDirectorBattleTalkAllLanguages(uint voiceover)
    {

        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var lang in _langList)
        {
            var langStr = LanguageUtil.GetLanguageStr(lang);
            try
            {
                var sheet = PluginServices.DataManager.Excel.GetSubrowSheet<ContentDirectorBattleTalk>(language: lang);
                var entry = new ContentDirectorBattleTalk();
                var foundEntry = false;
                foreach (SubrowCollection<ContentDirectorBattleTalk> row in sheet)
                foreach (var talk in row.Where(talk => talk.Unknown1 == voiceover))
                {
                    entry = talk;
                    foundEntry = true;
                    break;
                }

                if (foundEntry)
                {
                    var text = entry.Text.Value.Text.ExtractText();
                    dict[langStr] = text;
                }
            }
            catch (UnsupportedLanguageException)
            {
                PluginServices.PluginLog.Verbose($"Attempted to pull NPC Yell for {langStr} and failed due to unsupported language");   
            }
           

        }
        return dict;
    }
    
    
    private Dictionary<string, string> GetInstanceContentTextDataAllLang(uint textDataRow)
    {

        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var lang in _langList)
        {
            var langStr = LanguageUtil.GetLanguageStr(lang);
            try
            {
                var sheet = PluginServices.DataManager.Excel.GetSheet<InstanceContentTextData>(language: lang);
                var foundEntry = sheet.TryGetRow(textDataRow, out var row);
                var text = foundEntry ? row.Text.ExtractText() : InternalConstants.ErrorContactDev;
                dict[langStr] = text;

            }
            catch (UnsupportedLanguageException)
            {
                PluginServices.PluginLog.Verbose($"Attempted to pull Instance Content Text Data for {langStr} and failed due to unsupported language");   
            }
           

        }
        return dict;
    }
    public IShoutcastBuilder WithId(string id) { _instance.Id = id; return this; }
    public IShoutcastBuilder WithIcon(uint icon) { _instance.Icon = icon; return this; }
    public IShoutcastBuilder WithTranscription(Dictionary<string, string> transcription) { _instance.Transcription = transcription; return this; }
    public IShoutcastBuilder WithDuration(byte duration) { _instance.Duration = duration; return this; }
    public IShoutcastBuilder WithStyle(byte style) { _instance.Style = style; return this; }
    public IShoutcastBuilder WithShoutcaster(string name) { _instance.Shoutcaster = name; return this; }
    public IShoutcastBuilder WithAttributes(List<string> attributes) { _instance.Attributes = attributes; return this; }
    public IShoutcastBuilder WithSoundPath(string path) { _instance.SoundPath = path; return this; }
    public IShoutcastBuilder WithCutsceneLine(string cutsceneLine) { _instance.CutsceneLine = cutsceneLine; return this; }
    public IShoutcastBuilder WithContentDirectorBattleTalkVo(uint contentDirectorBattleTalkVo) { _instance.ContentDirectorBattleTalkVo = contentDirectorBattleTalkVo; return this; }
    public IShoutcastBuilder WithNpcYell(uint npcYell) { _instance.NpcYell = npcYell; return this; }
    public IShoutcastBuilder WithInstanceContentTextDataRow(uint instanceContentTextDataRow) { _instance.InstanceContentTextDataRow = instanceContentTextDataRow; return this; }

    private static Shoutcast NewShoutcast()
    {
        return new Shoutcast("ShoutcastId", 0, [], 5, 6, "Shoutcaster", [], "", "", 0, 0, 0);
    }

    [GeneratedRegex(@"^\(-.*-\)")]
    private static partial Regex CutsceneNameRemovalRegex();
}