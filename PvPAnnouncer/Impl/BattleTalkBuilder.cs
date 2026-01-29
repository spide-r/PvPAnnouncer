using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class BattleTalkBuilder(IDataManager dataManager) : IBattleTalkBuilder
{
    public IBattleTalk CreateFromVoLine(string name, uint voiceover, List<Personalization> personalization, uint icon = 0)
    {
        var sheet = dataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>(); 

        var t = sheet.Where(sc =>

        {

            return sc.Any(bt => bt.Unknown1.Equals(voiceover));

        }).First().First(aa => aa.Unknown1.Equals(voiceover)); 
        try
        {
      
            return new BattleTalk(name, t.Unknown1, t.Unknown3,
                t.Text.Value.ToString() ?? $"Unknown Text! You shouldn't be seeing this! ({voiceover})", personalization,
                t.Unknown0 != 0 ? t.Unknown0 : icon, t.Unknown4, t.RowId, t.SubrowId);

        }
        catch (InvalidOperationException _)
        {
            return new BattleTalk(name, 0, 5, $"Unknown Text! You shouldn't be seeing this! ({voiceover})", personalization, icon);

        }
    }

    public IBattleTalk CreateFromSoundPath(string name, string voicePath, List<Personalization> personalization, uint icon = 0)
    {
        throw new NotImplementedException();
    }

    public IBattleTalk CreateFromInstanceContentTextData(string name, uint voiceover, int duration, uint textDataRow,
        List<Personalization> personalization,
        uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0) 
    {
        var sheet = dataManager.GetExcelSheet<InstanceContentTextData>();
        var foundText = sheet.TryGetRow(textDataRow, out var row);
        var text = foundText ? row.Text.ToString() : InternalConstants.ErrorContactDev;
        return new BattleTalk(name, voiceover, duration, text, personalization, icon, style, rowId, subRowId);

    }

    public IBattleTalk CreateFromNpcYell(string name, uint voiceover, int duration, uint npcYellLine,
        List<Personalization> personalization,
        uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0) 
    {
        var sheet = dataManager.GetExcelSheet<NpcYell>();
        var foundText = sheet.TryGetRow(npcYellLine, out var row);
        var text = foundText ? row.Text.ToString() : InternalConstants.ErrorContactDev;
        return new BattleTalk(name, voiceover, duration, text, personalization, icon, style, rowId, subRowId);

    }
}