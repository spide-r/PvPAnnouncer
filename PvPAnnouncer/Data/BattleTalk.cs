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
  public ReadOnlySeString Text { get; }
  public byte Duration { get; }
  public byte Style { get; }
  public List<Personalization> Personalization { get; }
  public string SoundPath { get; }

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

  protected BattleTalk(string name, List<Personalization> personalization)
  {
    Name = name;
    Personalization = personalization;
  }

  //todo this constructor is bad and needs to be fixed, this means rewriting how we init battletalks - cant be depending on datamanager on being init
  public BattleTalk(string name, uint voiceover, List<Personalization> personalization, uint icon = 0) 
  {

    var sheet = PluginServices.DataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>();
    try
    {
      var t = sheet.SelectMany(row => row)

        .FirstOrDefault(bt => bt.Unknown1 == voiceover); 
      Name = name;
      RowId = t.RowId;
      SubRowId = t.SubrowId;
      Icon = icon != 0 ? icon : t.Unknown0;
      Voiceover = t.Unknown1;
      Text = t.Text.Value.Text;
      Duration = t.Unknown3;
      Style = t.Unknown4;
      Personalization = personalization;
    }
    catch (InvalidOperationException _)
    {
      Name = name;
      Personalization = personalization;
      RowId = 0;
      SubRowId = 0;
      Icon = 0;
      Voiceover = voiceover;
      Text = "Unknown Text! You shouldn't be seeing this";
      Duration = 3;
      Style = 6;
    }
  }


  public BattleTalk(string name, uint voiceover, int duration, string text, List<Personalization> personalization,
    uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0) : this(name, voiceover, duration, personalization, icon, style, rowId, subRowId)
  {//todo does this work?
    Text = text;
  }
  
  public BattleTalk(string name, uint voiceover, int duration, uint text, List<Personalization> personalization,
    uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0) : this(name, voiceover, duration, personalization, icon, style, rowId, subRowId)
  {//todo does this work?
    throw new NotImplementedException();
  }

  private BattleTalk(string name, uint voiceover, int duration, List<Personalization> personalization,
    uint icon = 0, byte style = 6, uint rowId = 0, uint subRowId = 0)
  {
    Name = name;
    Voiceover = voiceover;
    Duration = (byte) duration;
    Personalization = personalization;
    Icon = icon;
    Style = style;
    RowId = rowId;
    SubRowId = subRowId;
  }
  public string GetPath(string lang)
  {
    return "sound/voice/vo_line/" + Voiceover + "_" + lang + ".scd";
  }
  
}