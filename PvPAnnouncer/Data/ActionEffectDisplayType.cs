﻿/*
 * Attributed to Kouzukii/ffxiv-deathrecap
 * https://github.com/Kouzukii/ffxiv-deathrecap/blob/master/Game/ActionEffectDisplayType.cs
 */
namespace PvPAnnouncer.Data;

public enum ActionEffectDisplayType : byte {
    HideActionName = 0,
    ShowActionName = 1,
    ShowItemName = 2,
    MountName = 13
}