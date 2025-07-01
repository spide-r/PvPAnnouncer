using System.Collections.Generic;

namespace PvPAnnouncer.Data;
using static AnnouncerLines;
public abstract class InternalConstants
{
    public const string MessageTag = "PvPAnnouncer";
    public static readonly List<string> LimitBreakList =
        [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];

    public const string NoRespectText = "This man has absolutely no respect for the rules!";
    public const string UpOnThePostText = "He's up on the post! You know what that means...";
    public const string BombarianPressText = "It's the Bombarian press!";
    public const string OhMercyText = "Oh mercy is she doing what I think she's doing?";
    // For Bitwise settings
    public const int FemPronouns = 1;
    public const int MascPronouns = 2;
    public const int BlackCat = 3;
    public const int HoneyBLovely = 4;
    public const int BruteBomber = 5;
    public const int WickedThunder = 6;
    public const int DancingGreen = 7;
    public const int SugarRiot = 8;
    public const int BruteAbominator = 9;
    public const int HowlingBlade = 10;
}