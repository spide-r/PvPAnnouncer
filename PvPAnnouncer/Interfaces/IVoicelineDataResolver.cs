using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public interface IVoicelineDataResolver
{
    void InitOrphanedLines();
    void InitCutsceneLines();
    List<string> GetOrphanedLines();
    Dictionary<string, List<string>> GetCutsceneLines();
    void ResolveCutsceneLineWithTag(string tag);
    void ResolveBattleTalkWithVo(uint voline);
}