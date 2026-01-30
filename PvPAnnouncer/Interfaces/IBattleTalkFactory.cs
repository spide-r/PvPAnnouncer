using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IBattleTalkFactory
{
    
    public BattleTalk CreateFromContentDirectorBattleTalk(string name, uint voiceover, List<Personalization> personalization,
        uint icon = 0);

    public BattleTalk CreateFromCutsceneLine(string name, int expac, int duration, string tag, 
        List<Personalization> personalization,
        uint icon = 0);

    public BattleTalk CreateFromNpcYell(string name, uint voiceover, int duration, uint npcYellLine,
        List<Personalization> personalization,
        uint icon = 0);

    public BattleTalk CreateFromInstanceContentTextData(string name, uint voiceover, int duration, uint textDataRow,
        List<Personalization> personalization,
        uint icon = 0);
    
    public BattleTalk CreateFromNoSheet(string name, uint voiceover, int duration, string text,
        List<Personalization> personalization,
        uint icon = 0);
}