using System.Collections.Generic;
using Dalamud.Plugin.Services;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.impl;

public class ShoutcastBuilder(IDataManager dataManager): IShoutcastBuilder
{
    private Shoutcast _instance = NewShoutcast();
    
    //todo research how cutsceneline's work and see if you even need to provide an expac 
    //todo determine if we need to put lookup logic here or leave it in a diff repository / when needed
    //todo consider if you should load all transcriptions into the class - maybe that would work since you could then dynamically change lang on the fly
    
    public Shoutcast Build()
    {
        
        var result = _instance;
        //todo check and warn for screwed up shoutcast obj
        if (result.Transcription.Keys.Count == 0) //no text transcription
        {
            if ( result is {ContentDirectorBattleTalkRow: 0, InstanceContentTextDataRow: 0, NpcYell: 0})
            {
                //todo error out since we dont have text
            }
            //todo we will probably load transcriptions right here using either ct dir battle talk, or CutsceneLine if it exists
            //if nothing exists, throw an error/warn
        }
        
        if (!dataManager.FileExists(result.GetShoutcastSoundPathWithLang("ja")))
        {
            //todo error out since we dont have an audio file
        }

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
    public IShoutcastBuilder WithCutsceneLine(string cutsceneLine) { _instance.CutsceneLine = cutsceneLine; return this; }
    public IShoutcastBuilder WithContentDirectorBattleTalkRow(uint contentDirectorBattleTalkRow) { _instance.ContentDirectorBattleTalkRow = contentDirectorBattleTalkRow; return this; }
    public IShoutcastBuilder WithNpcYell(uint npcYell) { _instance.NpcYell = npcYell; return this; }
    public IShoutcastBuilder WithInstanceContentTextDataRow(uint instanceContentTextDataRow) { _instance.InstanceContentTextDataRow = instanceContentTextDataRow; return this; }

    private static Shoutcast NewShoutcast()
    {
        return new Shoutcast("ShoutcastId", 0, [], 5, 6, "ShouterName", [], "", "", 0, 0, 0);
    }
    
}