using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyResurrectEvent: PvPActorEvent
{
    public AllyResurrectEvent()
    {
        Name = "Resurrection";
        InternalName = "AllyResurrectEvent";
    }

    public override List<string> SoundPaths()
    {
        return [BackUpGrit, BackOnFeet, RisesAgain];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>();
    }

    public override bool InvokeRule(IMessage m)
    {
        return m is UserResurrectedMessage;
    }

}
