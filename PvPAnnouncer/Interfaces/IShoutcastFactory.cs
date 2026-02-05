using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IShoutcastFactory
{
    
    public Shoutcast CreateFromContentDirectorBattleTalk(string name, uint voiceover, List<Personalization> personalization,
        uint icon = 0);

    public Shoutcast CreateFromCutsceneLine(string name, int expac, int duration, string tag, 
        List<Personalization> personalization,
        uint icon = 0);

    public Shoutcast CreateFromNpcYell(string name, uint voiceover, uint npcYellLine,
        List<Personalization> personalization,
        uint icon = 0);

    public Shoutcast CreateFromInstanceContentTextData(string name, uint voiceover, int duration, uint textDataRow,
        List<Personalization> personalization,
        uint icon = 0);
    
    public Shoutcast CreateFromNoSheet(string name, uint voiceover, int duration, string text,
        List<Personalization> personalization,
        uint icon = 0);
}