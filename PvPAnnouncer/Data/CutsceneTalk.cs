using System.Collections.Generic;
using Lumina.Excel.Sheets;
using Lumina.Text.ReadOnly;

namespace PvPAnnouncer.Data;
//todo make this like battletalk and have it pull from cutscene line data
public class CutsceneTalk(string line, List<Personalization> personalization) : BattleTalk("CutsceneTalk", personalization)
{
    public new string GetPath(string lang)
    {
        return line + "_" + lang + ".scd";

    }
    
}