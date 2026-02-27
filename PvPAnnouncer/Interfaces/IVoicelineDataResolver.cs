using System.Collections.Generic;
using Lumina.Excel.Sheets;

namespace PvPAnnouncer.Interfaces;

public interface IVoicelineDataResolver
{
    void InitOrphanedLines();
    void InitCutsceneLines();
    List<string> GetOrphanedLines();
    Dictionary<string, List<string>> GetCutsceneLineTags();
    string ResolveCutsceneLineWithTag(string tag);
    string ResolveTextWithNpcYell(uint npcYell);
    string? ResolveTextWithIctdRow(uint row);
    List<ContentDirectorBattleTalk> GetCdbtList();
}