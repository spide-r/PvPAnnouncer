using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using static PvPAnnouncer.Data.AnnouncerLines;
using static PvPAnnouncer.Data.ScionLines;
namespace PvPAnnouncer.impl.PvPEvents;

public class AllyZoneOutEvent: PvPActorEvent
{
    public AllyZoneOutEvent()
    {
        Name = "Fatal Fall Damage";
        InternalName = "AllyZoneOutEvent";
    }

    public override List<Shoutcast> SoundPaths()
    {
        return [Fallen, Hahahahahaha, OutplayedClearMinds, TwelveGiveMeStrength, KeepChinUpPrettyLeast, 
            SuchShouldBeFate, UnfortunateSuchIsLife, LeftOurselvesOpen, DoYouRequireHealing, ThatsTooBadKrile, GonnaBeSickWuk, CantBe, GodsHelpUs, Unfortunate];
    }

    public override bool InvokeRule(IMessage m) 
    {
        if (m is UserZoneOutMessage)
        {
            return true;
        }
        return false;
    }

}
