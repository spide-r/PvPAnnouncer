using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Game;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;
using Lumina.Text.ReadOnly;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Data;

public class  Shoutcast : IShoutcast // decorator
{
  public uint Icon { get; set; }

  public string ShouterName { get; set; }

  public byte Duration { get; set; }
  public byte Style { get; set; }
  public Dictionary<string, string> Transcription { get; set; }
  public string Id { get; set; }
  public List<string> Attributes { get; set; }
  public string SoundPath { get; set; }
  public string CutsceneLine { get; set; }

  public uint ContentDirectorBattleTalkRow {get; set;}
  
  public uint NpcYell {get; set;}
  
  public uint InstanceContentTextDataRow {get; set;}

  /*
      name: ContentDirectorBattleTalk
      fields:
        - name: Unknown0 -> BattleTalkIcon
        - name: Unknown1 -> Voiceover
        - name: Text
          type: link
          targets: [InstanceContentTextData]
        - name: Unknown3 -> BattleTalkDuration
        - name: Unknown4 -> BattleTalkStyle
  
     */
  

  [Obsolete]
  public Shoutcast(string shouterName, uint voiceover, int duration, string text, List<Personalization> personalization,
    uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0, string path = "") 
  {
    ShouterName = shouterName;
    Duration = (byte) duration;
    Icon = icon;
    Style = style;
  }

  public Shoutcast(string id, uint icon, Dictionary<string, string> transcription, byte duration, byte style,
    string shouterName, List<string> attributes, string soundPath, string cutsceneLine, uint contentDirectorBattleTalkRow, uint npcYell, uint instanceContentTextDataRow)
  {
    Id = id;
    Icon = icon;
    Transcription = transcription;
    Duration = duration;
    Style = style;
    ShouterName = shouterName;
    Attributes = attributes;
    SoundPath = soundPath;
    CutsceneLine = cutsceneLine;
    ContentDirectorBattleTalkRow = contentDirectorBattleTalkRow;
    NpcYell = npcYell;
    InstanceContentTextDataRow = instanceContentTextDataRow;
  }

  public string GetShoutcastSoundPathWithLang(string lang)
  {
    return SoundPath + "_" + lang + ".scd";
  }

  public string GetTranscriptionWithLang(string lang)
  {
    return Transcription[lang];
  }

  public Shoutcast()
  {
    
  }
}