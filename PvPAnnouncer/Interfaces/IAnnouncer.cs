﻿using System.Collections;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Interfaces;

public interface IAnnouncer
{
    void ReceivePvPEvent(PvPEvent pvpEvent);
    
    void PlaySound(string sound);
    void SendBattleTalk(string sound);
}
