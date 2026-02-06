using System.Collections.Generic;
using Lumina.Text.ReadOnly;

namespace PvPAnnouncer.Interfaces;

public interface IShoutcast
{
    public uint Icon { get; }
    public string Shoutcaster { get; }
    public byte Duration { get; }
    public byte Style { get; }

    public Dictionary<string, string> Transcription { get; set; }//language => text

    public string Id { get; set; }
    public List<string> Attributes { get; set; }

    public string SoundPath { get;  }
    public string CutsceneLine { get; }
    
    public uint ContentDirectorBattleTalkVo {get; set;}
  
    public uint NpcYell {get; set;}
  
    public uint InstanceContentTextDataRow {get; set;}
}