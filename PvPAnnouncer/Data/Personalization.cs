using System;

namespace PvPAnnouncer.Data;
[Flags]
public enum Personalization: int
{
 None = 0,
 FemPronouns = 1 << 0,
 MascPronouns = 1 << 1,
 BlackCat = 1 << 2,
 HoneyBLovely = 1 << 3,
 BruteBomber = 1 << 4,
 WickedThunder = 1 << 5,
 DancingGreen = 1 << 6,
 SugarRiot = 1 << 7,
 BruteAbominator = 1 << 8,
 HowlingBlade = 1 << 9,
 VampFatale = 1 << 10,
 DeepBlueRedHot = 1 << 11,
 Tyrant = 1 << 12,
 President = 1 << 13,
 MetemAnnouncer = 1 << 14,
 AlphinaudAnnouncer = 1 << 15,
 AlisaieAnnouncer = 1 << 16,
 ThancredAnnouncer = 1 << 17,
 UriangerAnnouncer = 1 << 18,
 YshtolaAnnouncer = 1 << 19,
 EstinienAnnouncer = 1 << 20,
 GrahaAnnouncer = 1 << 21,
 KrileAnnouncer = 1 << 22,
 WukLamatAnnouncer = 1 << 23,
 KoanaAnnouncer = 1 << 24,
 BakoolJaJaAnnouncer = 1 << 25,
 ErenvilleAnnouncer = 1 << 26,
 ZenosAnnouncer = 1 << 27,
}