using System.Collections.Generic;

namespace PvPAnnouncer.Interfaces;

public interface IVoicelineDataResolver
{
    void InitOrphanedLines();
    void InitCutsceneLines();
    List<string> GetOrphanedLines();
    Dictionary<string, List<string>> GetCutsceneLineTags();
    string ResolveCutsceneLineWithTag(string tag);
    uint ResolveBattleTalkWithVo(uint voline);
    string ResolveTextWithNpcYell(uint npcYell);
    string? ResolveTextWithIctdRow(uint row);
}