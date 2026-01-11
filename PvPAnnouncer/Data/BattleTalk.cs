using System.Linq;
using Dalamud.Game;
using Lumina.Excel.Sheets;
using Lumina.Text.ReadOnly;

namespace PvPAnnouncer.Data;

public class BattleTalk // decorator
{
  public readonly uint RowId;
  public readonly uint SubRowId;
  public readonly uint Icon;
  public readonly uint Voiceover;
  public readonly ReadOnlySeString Text;
  public readonly byte Duration;
  public readonly byte Style;
  
  public BattleTalk()
  {
    RowId = 0;
    SubRowId = 0;
    Icon = 0;
    Voiceover = 0;
    Text = new ReadOnlySeString();
    Duration = 5;
    Style = 6;
  }

  public BattleTalk(string voiceover)
  {
    uint vo = uint.Parse(voiceover);
    var sheet = PluginServices.DataManager.GetSubrowExcelSheet<ContentDirectorBattleTalk>();
    var t = sheet.Where(sc =>
    {
      return sc.Any(bt => bt.Unknown1.Equals(vo));
    }).First().First(aa => aa.Unknown1.Equals(vo));
    RowId = t.RowId;
    SubRowId = t.SubrowId;
    Icon = t.Unknown0;
    Voiceover = t.Unknown1;
    Text = t.Text.Value.Text;
    Duration = t.Unknown3;
    Style = t.Unknown4;
  }
  
  public BattleTalk(string voiceover, int duration, string text)
  {
    uint vo = uint.Parse(voiceover);
    
    
    
    
    RowId = 0;
    SubRowId = 0;
    Icon = 0;
    Voiceover = vo;
    Text = text;
    Duration = (byte) duration;
    Style = 6;
  }

  private static ClientLanguage GetLanguage(string lang)
  {
    return lang switch
    {
      "en" => ClientLanguage.English,
      "fr" => ClientLanguage.French,
      "ja" => ClientLanguage.Japanese,
      "de" => ClientLanguage.German,
      _ => ClientLanguage.English
    };
  }

  public BattleTalk(ContentDirectorBattleTalk t)
  {
    RowId = t.RowId;
    SubRowId = t.SubrowId;
    Icon = t.Unknown0;
    Voiceover = t.Unknown1;
    Text = t.Text.Value.Text;
    Duration = t.Unknown3;
    Style = t.Unknown4;
  }

  public string GetPath(string lang)
  {
    return "sound/voice/vo_line/" + Voiceover + "_" + lang + ".scd";

  }




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
}