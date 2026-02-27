using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Lumina.Extensions;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public partial class VoicelineDataResolver : IVoicelineDataResolver
{
    private List<string> _orphanedVoLines = [];
    private string[] _orphanedVoLineArr = [];
    private Dictionary<string, List<string>> _cutsceneLines = new();
    private List<ContentDirectorBattleTalk> _ctrList = [];
    private readonly Dictionary<Language, ExcelSheet<NpcYell>> _npcYellMemo = [];
    private readonly Dictionary<string, string> _cSLineTagDict = []; // me when memoization
    private readonly Dictionary<Language, ExcelSheet<InstanceContentTextData>> _instanceContentTextData = new();


    public VoicelineDataResolver()
    {
        InitCutsceneLines();
        InitOrphanedLines();
    }

    public void InitOrphanedLines()
    {
        var sheet = PluginServices.DataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>();
        var allUints = sheet.Flatten().Select(k => k.Unknown1).ToHashSet();
        PluginServices.PluginLog.Verbose("Starting to load all orphaned lines!");
        Task.Run(() =>
        {
            var results = new List<string>();
            for (uint j = 0; j < 9999999; j++)
            {
                var voLine = $"sound/voice/vo_line/{j}_en.scd";
                if (!PluginServices.DataManager.FileExists(voLine)) continue;

                if (!allUints.Contains(j))
                    // if the VO Line does not exist in sheet, its orphaned so we need to add data ourselves
                    results.Add(j.ToString());
            }

            PluginServices.Framework.RunOnFrameworkThread(() =>
            {
                _orphanedVoLines = results;
                _orphanedVoLineArr = _orphanedVoLines.ToArray();
                PluginServices.PluginLog.Verbose("Finished loading all orphaned lines!");
            });
        });
    }

    public void InitCutsceneLines()
    {
        PluginServices.PluginLog.Verbose("Started Loading all Cutscene Lines! ");
        //todo make a program that scans through lumina to create this
        _cutsceneLines = PluginServices.JsonLoader.LoadCutsceneLines();
        PluginServices.PluginLog.Verbose("Finished Loading all Cutscene Lines!");
    }

    public List<string> GetOrphanedLines()
    {
        return _orphanedVoLines;
    }

    public Dictionary<string, List<string>> GetCutsceneLineTags()
    {
        return _cutsceneLines;
    }

    public string ResolveCutsceneLineWithTag(string tag)
    {
        var tagKey = PluginServices.Config.Language + "_" + tag;
        if (_cSLineTagDict.TryGetValue(tagKey, out var value)) return value;

        var splitLine = tag.Split("_");
        var number = splitLine[2];
        var trimmedNumber = number.Substring(0, 3);
        var csvName = $"cut_scene/{trimmedNumber}/VoiceMan_{number}";
        var lang = LanguageUtil.LanguageMap.First(p => p.Value.Equals(PluginServices.Config.Language)).Key;
        var cutscene = PluginServices.DataManager.Excel.GetSheet<CutsceneText>(lang, csvName);
        var row = cutscene.FirstOrNull(r => r.MessageTag.ToString().Equals(tag));
        var dialogue = InternalConstants.ErrorContactDev;
        if (row != null) dialogue = row.Value.Dialogue.ToMacroString();

        dialogue = CutsceneNameRemovalRegex()
            .Replace(dialogue,
                ""); // any dialogue with (- text_here -) at the start will override the name shown in battletalk
        _cSLineTagDict[tagKey] = dialogue;
        return dialogue;
    }


    public string ResolveTextWithNpcYell(uint npcYell)
    {
        var lang = LanguageUtil.LanguageMap.First(p => p.Value.Equals(PluginServices.Config.Language)).Key;
        var y = PullNpcYellMemo(lang);
        if (y.TryGetRow(npcYell, out var row)) return row.Text.ToString();

        return InternalConstants.ErrorContactDev;
    }

    private ExcelSheet<NpcYell> PullNpcYellMemo(Language lang)
    {
        if (_npcYellMemo.TryGetValue(lang, out var cached)) return cached;

        _npcYellMemo[lang] = PluginServices.DataManager.Excel.GetSheet<NpcYell>(lang);
        return _npcYellMemo[lang];
    }

    public string? ResolveTextWithIctdRow(uint row)
    {
        var lang = LanguageUtil.LanguageMap.First(p => p.Value.Equals(PluginServices.Config.Language)).Key;
        if (GetInstanceContentTextData(lang).TryGetRow(row, out var ctrRow)) return ctrRow.Text.ToString();

        return InternalConstants.ErrorContactDev;
    }

    private ExcelSheet<InstanceContentTextData> GetInstanceContentTextData(Language lang)
    {
        if (_instanceContentTextData.TryGetValue(lang, out var data)) return data;

        var instanceContent = PluginServices.DataManager.Excel.GetSheet<InstanceContentTextData>(lang);
        _instanceContentTextData[lang] = instanceContent;
        return instanceContent;
    }

    public List<ContentDirectorBattleTalk> GetCdbtList()
    {
        if (_ctrList.Count > 0) return _ctrList;

        _ctrList = PluginServices.DataManager.Excel.GetSubrowSheet<ContentDirectorBattleTalk>().Flatten().ToList();
        return _ctrList;
    }

    [GeneratedRegex(@"^\(-.*-\)")]
    private static partial Regex CutsceneNameRemovalRegex();
}