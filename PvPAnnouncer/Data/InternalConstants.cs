using System.Collections.Generic;

namespace PvPAnnouncer.Data;
using static AnnouncerLines;
public abstract class InternalConstants
{
    public const string MessageTag = "PvPAnnouncer";
    public const ushort MessageColor = 171;
    public static readonly ushort[] pvpTerritories = []; //todo: figure out what pvp territories exist

    public static readonly List<string> LimitBreakList =
        [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
}