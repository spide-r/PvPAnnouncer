using System.Collections.Generic;
using Lumina.Excel.Sheets;

namespace PvPAnnouncer.Data;

public class CutsceneTalk: BattleTalk
{
    public string CutsceneLine;
    public readonly List<Personalization> Personalization;

    public CutsceneTalk(string line, List<Personalization> personalization)
    {
        CutsceneLine = line;
        var i = Personalization;
        Personalization = personalization;
    }
    
    public new string GetPath(string lang)
    {
        return CutsceneLine + "_" + lang + ".scd";

    }
    
}