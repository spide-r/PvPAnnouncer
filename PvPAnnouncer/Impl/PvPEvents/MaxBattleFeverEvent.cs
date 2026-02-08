using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class MaxBattleFeverEvent: PvPEvent
{
    public MaxBattleFeverEvent()
    {
        Name = "Gaining Battle High V / Flying High";
        Id = "MaxBattleFeverEvent";
    }

    public override bool InvokeRule(IMessage message)
    {
        if (message is BattleHighMessage bhm)
        {
            return bhm.Level == 5;
        } 
        return message is FlyingHighMessage;
    }
}