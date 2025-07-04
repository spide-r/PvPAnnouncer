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
        Name = "Entered Rival Wings Mech";
        InternalName = "EnteredMechEvent";
    }
    public override List<string> SoundPaths()
    {
        return [ColossalThing];
    }

    public override Dictionary<Personalization, List<string>> PersonalizedSoundPaths()
    {
        return new Dictionary<Personalization, List<string>>();
    }

    public override bool InvokeRule(IMessage message)
    {
        return message is UserEnteredMechMessage;
    }
}
