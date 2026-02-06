using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IShoutcastBuilder
{
    //instead of voiceover, we use soundpath
    //instead of Text, we use transcription
    //name is assumed to be how customization for announcer is filtered
    //personalization is for fem/masc pronouns, if certain competitors can be mentioned, etc 
    Shoutcast Build();
    public IShoutcastBuilder WithId(string id);
    public IShoutcastBuilder WithIcon(uint icon);
    public IShoutcastBuilder WithTranscription(Dictionary<string, string> transcription);
    public IShoutcastBuilder WithDuration(byte duration);
    public IShoutcastBuilder WithStyle(byte style);
    public IShoutcastBuilder WithShoutcaster(string name);
    public IShoutcastBuilder WithAttributes(List<string> attributes);
    public IShoutcastBuilder WithSoundPath(string path);
    public IShoutcastBuilder WithCutsceneLine(string path);
    public IShoutcastBuilder WithContentDirectorBattleTalkVo(uint contentDirectorBattleTalkVo);
    public IShoutcastBuilder WithNpcYell(uint npcYell);
    public IShoutcastBuilder WithInstanceContentTextDataRow(uint instanceContentTextDataRow);

}