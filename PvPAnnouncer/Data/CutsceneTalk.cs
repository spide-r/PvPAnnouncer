using System.Collections.Generic;
using Lumina.Excel.Sheets;
using Lumina.Text.ReadOnly;

namespace PvPAnnouncer.Data;

public class CutsceneTalk(string line, List<Personalization> personalization) : BattleTalk("CutsceneTalk", personalization)
{
    public new string GetPath(string lang)
    {
        return line + "_" + lang + ".scd";

    }
    
}