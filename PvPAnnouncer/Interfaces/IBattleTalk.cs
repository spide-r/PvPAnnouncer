using System.Collections.Generic;
using Lumina.Text.ReadOnly;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IBattleTalk
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

}