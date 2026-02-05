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
  [Obsolete]
  public uint RowId { get; }
  [Obsolete]
  public uint SubRowId { get; }
  public uint Icon { get; set; }

  [Obsolete]
  public uint Voiceover { get; }
  public string ShouterName { get; set; }

  [Obsolete]
  public string Text { get; }
  public byte Duration { get; set; }
  public byte Style { get; set; }

  [Obsolete]
  public List<Personalization> Personalization { get; }
  public string Path { get; }
  public Dictionary<string, string> Transcription { get; set; }
  public string Id { get; set; }
  public List<string> Attributes { get; set; }
  public string SoundPath { get; set; }
  public string TextPath { get; set; }

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
  

  public Shoutcast(string shouterName, uint voiceover, int duration, string text, List<Personalization> personalization,
    uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0, string path = "") 
  {
    ShouterName = shouterName;
    Text = text;
    Voiceover = voiceover;
    Duration = (byte) duration;
    Personalization = personalization;
    Icon = icon;
    Style = style;
    RowId = rowId;
    SubRowId = subRowId;
    Path = path.Equals("") ? "sound/voice/vo_line/" + voiceover : path; //todo logic shouldnt be here - shitty! sad! remove!
  }

  public Shoutcast(string id, uint icon, Dictionary<string, string> transcription, byte duration, byte style,
    string shouterName, List<string> attributes, string soundPath, string textPath)
  {
    Id = id;
    Icon = icon;
    Transcription = transcription;
    Duration = duration;
    Style = style;
    ShouterName = shouterName;
    Attributes = attributes;
    SoundPath = soundPath;
    TextPath = textPath;
  }
  public Shoutcast()
  {
    
  }
}