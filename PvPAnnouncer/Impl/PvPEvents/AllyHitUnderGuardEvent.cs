using System.Linq;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using PvPAnnouncer.Data;
using PvPAnnouncer.Impl.Messages;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.impl.PvPEvents;

public class AllyHitUnderGuardEvent: PvPActionEvent
{
    public AllyHitUnderGuardEvent()
    {
        Name = "Hit while under Guard";
        InternalName = "AllyHitUnderGuardEvent";
    }

    public override bool InvokeRule(IMessage arg)
    {
        if (arg is ActionEffectMessage pp)
        {
            if (pp.GetTargetIds().Contains((uint) pp.SourceId))
            {
                return false;
            }
            foreach (var target in pp.GetTargetIds())
            {
                if (PluginServices.PvPMatchManager.IsMonitoredUser(target))
                {
                    IGameObject? obj = pp.GetGameObject(target);
                    if (obj is IPlayerCharacter)
                    {
                        IPlayerCharacter? player = obj as IPlayerCharacter;
                        var list = player?.StatusList;
                        if (list != null)
                            foreach (var status in list)
                            {
                                if (status.StatusId == StatusIds.Guard || status.StatusId == StatusIds.HardenedScales)
                                {
                                    return true;
                                }
                            }
                    }
                }
            }
        }

        return false;
    }
}
