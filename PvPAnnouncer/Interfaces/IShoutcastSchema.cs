using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public class IShoutcastSchema
{
    public uint Icon { get; set; } // only 1 character icon 
    public Dictionary<string, string> Transcription { get; set; } = [];//language => text
    public byte Style { get; set; } = 6; // default 6
    public byte Duration { get; set; } = 5; 
    public string Name { get; set; } = "Unknown Name";
    public List<string> Personalization { get; set; } = [];
    public string? SoundPath { get; set; }
    public string? TextPath { get; set; }
}