using System.Collections.Generic;

namespace PvPAnnouncer.Data;
using static AnnouncerLines;
public abstract class InternalConstants
{
    public const string MessageTag = "PvPAnnouncer";
    public static readonly List<string> LimitBreakList =
        [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];
}