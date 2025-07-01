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
    }

    public override List<string> SoundPaths()
    {
        return [SendingCameras, UpstartBegins];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>()
        {
            {Personalization.BlackCat, [FirstOpponent, NineLives]},
            {Personalization.HoneyBLovely, [HoneyBShowBegun, SavorSting]},
            {Personalization.BruteBomber, [BBMuscled]},
            {Personalization.WickedThunder, [WTReturned]},
            {Personalization.DancingGreen, [DGSteps]},
            {Personalization.SugarRiot, [SRGallery]},
            {Personalization.BruteAbominator, [BAMeansBusiness]},
            {Personalization.HowlingBlade, [WolfLair]},
        };
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is MatchStartedMessage;
    }

}
