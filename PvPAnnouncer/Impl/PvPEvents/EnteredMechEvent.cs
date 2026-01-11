using System;
using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class EnteredMechEvent: PvPEvent
{
    public EnteredMechEvent()
    {
        Name = "Entering a RW Mech";
        InternalName = "EnteredMechEvent";
    }
    public override List<BattleTalk> SoundPaths()
    {
        return [ColossalThing];
    }

    public override Dictionary<Personalization, List<BattleTalk>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<BattleTalk>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is UserEnteredMechMessage;
    }
}
