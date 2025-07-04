using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class MatchStartEvent: PvPMatchEvent
{
    public MatchStartEvent()
    {
        Name = "Matches Started";
        InternalName = "MatchStartEvent";
    }

    public override List<string> SoundPaths()
    {
        return [SendingCameras, UpstartBegins];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>()
        {
            {Personalization.BlackCat, [FirstOpponent, NineLives, IntroBc]},
            {Personalization.HoneyBLovely, [HoneyBShowBegun, SavorSting, IntroHbl]},
            {Personalization.BruteBomber, [BBMuscled, IntroBb]},
            {Personalization.WickedThunder, [WTReturned, IntroWt]},
            {Personalization.DancingGreen, [DGSteps, IntroDg]},
            {Personalization.SugarRiot, [SRGallery, IntroDg]},
            {Personalization.BruteAbominator, [BAMeansBusiness, IntroBb]},
            {Personalization.HowlingBlade, [WolfLair, IntroHb]},
        };
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchStartedMessage;
    }

}
