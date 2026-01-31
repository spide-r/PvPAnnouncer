using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;
using Lumina.Extensions;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class BattleTalkFactory(IDataManager dataManager) : IBattleTalkFactory
{
    
    public BattleTalk CreateFromContentDirectorBattleTalk(string name, uint voiceover, List<Personalization> personalization, uint icon = 0)
    {
        var sheet = dataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>(); 

        try
        {
            
            var t = sheet.Where(sc =>
            {
                return sc.Any(bt => bt.Unknown1.Equals(voiceover));

            }).First().First(aa => aa.Unknown1.Equals(voiceover)); 
            return new BattleTalk(name, t.Unknown1, t.Unknown3,
                t.Text.Value.ToString() ?? $"Unknown Text! You shouldn't be seeing this! ({voiceover})", personalization,
                t.Unknown0 != 0 ? t.Unknown0 : icon, t.Unknown4, t.RowId, t.SubrowId);
        }
        catch (InvalidOperationException)
        {
            return new BattleTalk(InternalConstants.PvPAnnouncerDevName, 0, 5, InternalConstants.ErrorContactDev, personalization, InternalConstants.PvPAnnouncerDevIcon);

        }
    }

    public BattleTalk CreateFromCutsceneLine(string name, int expac, int duration, string tag, 
        List<Personalization> personalization,
        uint icon = 0)
    {
            
        var splitLine = tag.Split("_");
        var number = splitLine[2];
        var secondNumber = splitLine[3];
        var trimmedNumber = number.Substring(0,3);
        var csvName = $"cut_scene/{trimmedNumber}/VoiceMan_{number}";
        var cutscene = PluginServices.DataManager.GetExcelSheet<CutsceneText>(name: csvName);
        var audio = $"cut/ex{expac}/sound/voicem/voiceman_{number}/vo_voiceman_{number}_{secondNumber}_m_";
            
        var row = cutscene.FirstOrNull(r => r.MessageTag.ExtractText().Equals(tag));
        var dialogue = InternalConstants.ErrorContactDev;
        if (row != null)
        {
            dialogue = row.Value.Dialogue.ExtractText();
        }
        dialogue = Regex.Replace(dialogue, @"^\(-.*-\)", ""); // any dialogue with (- text_here -) at the start will override the name shown in battletalk
        
        return new BattleTalk(name, 0, duration, dialogue, personalization, icon, 6, 0, 0, audio);
    }

    public BattleTalk CreateFromInstanceContentTextData(string name, uint voiceover, int duration, uint textDataRow,
        List<Personalization> personalization,
        uint icon = 0) 
    {
        var sheet = dataManager.GetExcelSheet<InstanceContentTextData>();
        var foundText = sheet.TryGetRow(textDataRow, out var row);
        var text = foundText ? row.Text.ToString() : InternalConstants.ErrorContactDev;
        return new BattleTalk(name, voiceover, duration, text, personalization, icon);

    }

    public BattleTalk CreateFromNoSheet(string name, uint voiceover, int duration, string text, List<Personalization> personalization,
        uint icon = 0)
    {
        return new BattleTalk(name, voiceover, duration, text, personalization, icon);

    }

    public BattleTalk CreateFromNpcYell(string name, uint voiceover, int duration, uint npcYellLine,
        List<Personalization> personalization,
        uint icon = 0) 
    {
        var sheet = dataManager.GetExcelSheet<NpcYell>();
        var foundText = sheet.TryGetRow(npcYellLine, out var row);
        var text = foundText ? row.Text.ToString() : InternalConstants.ErrorContactDev;
        return new BattleTalk(name, voiceover, duration, text, personalization, icon);

    }
}