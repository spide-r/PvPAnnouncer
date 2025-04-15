using System;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public unsafe interface IActionEffect: IMessage
{
    int SourceId { get; set; }
    IntPtr SourceCharacter { get; set; }
    IntPtr Pos { get; set; }
    ActionEffectHeader* EffectHeader { get; set; }
    ActionEffect* EffectArray { get; set; }
    ulong* EffectTrail { get; set; }
    
}