using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.impl;

public class ShoutcastBuilder: IShoutcastBuilder
{
    private Shoutcast _instance = NewShoutcast();
    
    public IShoutcast Build()
    {
        
        var result = _instance;
        //todo check and warn for screwed up shoutcast obj
        _instance = NewShoutcast();
        return result;
    }
    public IShoutcastBuilder WithId(string id) { _instance.Id = id; return this; }
    public IShoutcastBuilder WithIcon(uint icon) { _instance.Icon = icon; return this; }
    public IShoutcastBuilder WithTranscription(Dictionary<string, string> transcription) { _instance.Transcription = transcription; return this; }
    public IShoutcastBuilder WithDuration(byte duration) { _instance.Duration = duration; return this; }
    public IShoutcastBuilder WithStyle(byte style) { _instance.Style = style; return this; }
    public IShoutcastBuilder WithName(string name) { _instance.ShouterName = name; return this; }
    public IShoutcastBuilder WithAttributes(List<string> attributes) { _instance.Attributes = attributes; return this; }
    public IShoutcastBuilder WithSoundPath(string path) { _instance.SoundPath = path; return this; }
    public IShoutcastBuilder WithTextPath(string path) { _instance.TextPath = path; return this; }

    private static Shoutcast NewShoutcast()
    {
        return new Shoutcast("ShoutcastId", 0, [], 5, 6, "ShouterName", [], "", "");
    }
    
}