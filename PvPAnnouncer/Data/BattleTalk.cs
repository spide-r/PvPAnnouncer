using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Game;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;
using Lumina.Text.ReadOnly;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Data;

public class  BattleTalk : IBattleTalk // decorator
{
  public uint RowId { get; }
  public uint SubRowId { get; }
  public uint Icon { get; }
  public uint Voiceover { get; }
  public string Name { get; }
  public string Text { get; }
  public byte Duration { get; }
  public byte Style { get; }
  public List<Personalization> Personalization { get; }
  public string Path { get; }

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

  public BattleTalk(string name, uint voiceover, int duration, string text, List<Personalization> personalization,
    uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0, string path = "") 
  {
    Name = name;
    Text = text;
    Voiceover = voiceover;
    Duration = (byte) duration;
    Personalization = personalization;
    Icon = icon;
    Style = style;
    RowId = rowId;
    SubRowId = subRowId;
    Path = path.Equals("") ? "sound/voice/vo_line/" + voiceover : path;
  }
}