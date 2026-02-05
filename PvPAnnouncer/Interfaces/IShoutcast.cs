using System;
using System.Collections.Generic;
using Lumina.Text.ReadOnly;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IShoutcast
{
    [Obsolete]
    public uint RowId { get; }
    [Obsolete]
    public uint SubRowId { get; }
    public uint Icon { get; }
    [Obsolete(message:"Use UseSoundPath")]
    public uint Voiceover { get; }
    public string ShouterName { get; }
    [Obsolete(message:"Use Transcription for mahjong but you probably also want TextPath")]
    public string Text { get; }
    public byte Duration { get; }
    public byte Style { get; }
    [Obsolete(message:"Use Attributes")]
    public List<Personalization> Personalization { get; }
    [Obsolete(message:"Use SoundPath")]
    public string Path { get; }
    public Dictionary<string, string> Transcription { get; set; }//language => text

    public string Id { get; set; }
    public List<string> Attributes { get; set; }

    public string SoundPath { get;  }
    public string TextPath { get; }
}