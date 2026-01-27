namespace PvPAnnouncer.Data;
/*
 * Attributed to Kouzukii/ffxiv-deathrecap
 * https://github.com/Kouzukii/ffxiv-deathrecap/blob/master/Game/ActionEffect.cs
 */
public enum ActorControlCategory : ushort {
    Death = 0x6,
    CancelAbility = 0xF,
    GainEffect = 0x14,//todo - does this only come from external non-player sources? loseeffect happens as normal
    LoseEffect = 0x15,
    UpdateEffect = 0x16,
    TargetIcon = 0x22,
    Tether = 0x23,
    Targetable = 0x36,
    DirectorUpdate = 0x6D,
    SetTargetSign = 0x1F6,
    LimitBreak = 0x1F9,
    HoT = 0x604,
    DoT = 0x605
}
