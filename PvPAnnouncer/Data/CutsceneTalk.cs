using Lumina.Excel.Sheets;

namespace PvPAnnouncer.Data;

public class CutsceneTalk: BattleTalk
{
    public string CutsceneLine;
    public CutsceneTalk(string line)
    {
        CutsceneLine = line;
    }
    
    public new string GetPath(string lang)
    {
        return CutsceneLine + "_" + lang + ".scd";

    }
    
}