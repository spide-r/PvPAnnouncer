using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IBattleTalkBuilder
{
    
    public IBattleTalk CreateFromVoLine(string name, uint voiceover, List<Personalization> personalization,
        uint icon = 0);
    
    public IBattleTalk CreateFromSoundPath(string name, string voicePath, List<Personalization> personalization,
        uint icon = 0);

    public IBattleTalk CreateFromNpcYell(string name, uint voiceover, int duration, uint npcYellLine,
        List<Personalization> personalization,
        uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0);

    public IBattleTalk CreateFromInstanceContentTextData(string name, uint voiceover, int duration, uint textDataRow,
        List<Personalization> personalization,
        uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0);
}