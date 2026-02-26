using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dalamud.Plugin.Services;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Data;

public partial class Shoutcast : IShoutcast // decorator
{
    public uint Icon { get; set; }

    public string Shoutcaster { get; set; }

    public byte Duration { get; set; }
    public byte Style { get; set; }
    public Dictionary<string, string> Transcription { get; set; }
    public string Id { get; set; }
    public List<string> Attributes { get; set; }
    public string SoundPath { get; set; }
    public string CutsceneLine { get; set; }

    public uint ContentDirectorBattleTalkVo { get; set; }

    public uint NpcYell { get; set; }

    public uint InstanceContentTextDataRow { get; set; }

    public bool IsGendered { get; set; }

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

    public Shoutcast(string id, uint icon, Dictionary<string, string> transcription, byte duration, byte style,
        string shoutcaster, List<string> attributes, string soundPath, string cutsceneLine,
        uint contentDirectorBattleTalkVo, uint npcYell, uint instanceContentTextDataRow, bool isGendered)
    {
        Id = id;
        Icon = icon;
        Transcription = transcription;
        Duration = duration;
        Style = style;
        Shoutcaster = shoutcaster;
        Attributes = attributes;
        SoundPath = soundPath;
        CutsceneLine = cutsceneLine;
        ContentDirectorBattleTalkVo = contentDirectorBattleTalkVo;
        NpcYell = npcYell;
        InstanceContentTextDataRow = instanceContentTextDataRow;
    }

    public string GetShoutcastSoundPathWithLang(string lang)
    {
        return SoundPath + "_" + lang + ".scd";
    }

    [GeneratedRegex(@"<if\(gnum4,(.*?),(.*?)\)>")]
    private static partial Regex
        GenderRegex(); //"This Whole Thing Smacks Of Gender," i holler as i overturn my plugin and turn the fall of dalamud into the fall of Shit

    public string GetTranscriptionWithGender(string lang, bool fem, ISeStringEvaluator evaluator)
    {
        var transcription = Transcription.GetValueOrDefault(lang, "");
        if (!IsGendered)
        {
            return evaluator.EvaluateMacroString(transcription).ToString();
        }

        var toEval = transcription;
        PluginServices.PluginLog.Verbose($"To Eval: {toEval}, Fem: {fem}");


        //Only parameters in SeStrings are if(PlayerParameter(4) for gender stuff - largest amt of params i saw as of 7.0 was 3 - setting 5 just to be safe but due to how this plugin works i dont think we'll go past 1 

        if (fem)
        {
            var femStr = evaluator.EvaluateMacroString(GenderRegex().Replace(toEval, "$1")).ToString();
            PluginServices.PluginLog.Verbose($"Evaluated: {femStr}");
            return femStr;
        }
        else
        {
            var mascStr = evaluator.EvaluateMacroString(GenderRegex().Replace(toEval, "$2")).ToString();
            PluginServices.PluginLog.Verbose($"Evaluated: {mascStr}");

            return mascStr;
        }

        //return evaluator.EvaluateMacroString(Transcription.GetValueOrDefault(lang, ""), new Span<SeStringParameter>())
    }

    public string GetShoutcastSoundPathWithGenderAndLang(string lang, bool fem)
    {
        if (fem && IsGendered) // if this is a gendered voiceline and the user wants the fem version
        {
            return SoundPath.Replace("_m", "_f") + "_" + lang + ".scd";
        }

        // masc default
        return GetShoutcastSoundPathWithLang(lang);
    }

    public string GetFemSoundPath()
    {
        return SoundPath.Replace("_m", "_f") + "_ja.scd";
    }

    public override string ToString()
    {
        var at = Attributes.Aggregate("", (current, attribute) => current + attribute + " | ");
        var tr = Transcription.Aggregate("",
            (current, attribute) => current + attribute.Key + ":" + attribute.Value + " | ");
        return
            $"iD: {Id}, icon: {Icon}, Duration: {Duration}, Style: {Style}, Shoutcaster: {Shoutcaster}, attributes: {at}, transcription: {tr}, " +
            $"SoundPath: {SoundPath}, CutsceneLine: {CutsceneLine},  ContentDirectorBattleTalkVo: {ContentDirectorBattleTalkVo}, NPCYell: {NpcYell}, InstanceContentTextDataRow: {InstanceContentTextDataRow}";
    }
}