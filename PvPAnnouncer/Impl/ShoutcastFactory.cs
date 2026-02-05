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

public class ShoutcastFactory(IDataManager dataManager) : IShoutcastFactory
{
    
    public Shoutcast CreateFromContentDirectorBattleTalk(string name, uint voiceover, List<Personalization> personalization, uint icon = 0)
    {
        var sheet = dataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>(); 
    
        var t = sheet.SelectMany(row => row)

            .FirstOrDefault(bt => bt.Unknown1 == voiceover); 
        try
        {
            var text = t.Text.Value.Text.ExtractText();
            if (text.Equals(""))
            {
                text = $"Unknown Text! You shouldn't be seeing this! {voiceover}";
            }
            return new Shoutcast(name, t.Unknown1, t.Unknown3, text, personalization,
                t.Unknown0 != 0 ? t.Unknown0 : icon, t.Unknown4, t.RowId, t.SubrowId);
        }
        catch (InvalidOperationException)
        {
            return new Shoutcast(InternalConstants.PvPAnnouncerDevName, 0, 5, InternalConstants.ErrorContactDev, personalization, InternalConstants.PvPAnnouncerDevIcon);

        }
    }

    public Shoutcast CreateFromCutsceneLine(string name, int expac, int duration, string tag, 
        List<Personalization> personalization,
        uint icon = 0)
    {
            
        var splitLine = tag.Split("_");
        var number = splitLine[2];
        var secondNumber = splitLine[3];
        var trimmedNumber = number.Substring(0,3);
        var csvName = $"cut_scene/{trimmedNumber}/VoiceMan_{number}";
        var cutscene = PluginServices.DataManager.GetExcelSheet<CutsceneText>(name: csvName);
        var audio = $"cut/ex{expac}/sound/voicem/voiceman_{number}/vo_voiceman_{number}_{secondNumber}_m";
            
        var row = cutscene.FirstOrNull(r => r.MessageTag.ExtractText().Equals(tag));
        var dialogue = InternalConstants.ErrorContactDev;
        if (row != null)
        {
            dialogue = row.Value.Dialogue.ExtractText();
        }
        dialogue = Regex.Replace(dialogue, @"^\(-.*-\)", ""); // any dialogue with (- text_here -) at the start will override the name shown in battletalk
        
        return new Shoutcast(name, 0, duration, dialogue, personalization, icon, 6, 0, 0, audio);
    }

    public Shoutcast CreateFromInstanceContentTextData(string name, uint voiceover, int duration, uint textDataRow,
        List<Personalization> personalization,
        uint icon = 0) 
    {
        var sheet = dataManager.GetExcelSheet<InstanceContentTextData>();
        var foundText = sheet.TryGetRow(textDataRow, out var row);
        var text = foundText ? row.Text.ToString() : InternalConstants.ErrorContactDev;
        return new Shoutcast(name, voiceover, duration, text, personalization, icon);

    }

    public Shoutcast CreateFromNoSheet(string name, uint voiceover, int duration, string text, List<Personalization> personalization,
        uint icon = 0)
    {
        return new Shoutcast(name, voiceover, duration, text, personalization, icon);

    }

    public Shoutcast CreateFromNpcYell(string name, uint voiceover, uint npcYellLine,
        List<Personalization> personalization,
        uint icon = 0) 
    {
        var sheet = dataManager.GetExcelSheet<NpcYell>();
        var foundText = sheet.TryGetRow(npcYellLine, out var row);
        var text = foundText ? row.Text.ToString() : InternalConstants.ErrorContactDev;
        var duration = foundText ? row.BalloonTime : 5;
        return new Shoutcast(name, voiceover, (int) duration, text, personalization, icon);

    }
}