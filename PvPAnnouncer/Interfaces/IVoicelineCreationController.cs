using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IVoicelineCreationController
{
    void SelectBattleTalk(uint voLine);
    void SelectCutsceneLine(string cutsceneLine);
    void SelectOrphanedVoLine(uint voLine);
    void SelectNpcYell(uint row);
    void SelectInstanceContentTextDataRow(uint row);
    void SelectAnnouncementId(string announcementId);
    void SelectIcon(uint icon);
    void SelectName(string name);
    void SelectStyle(byte style);
    void SetDuration(byte duration);
    void SetManualTranscription(string lang, string transcription);
    void SetAttributes(List<string> attributes);
    void AddAttribute(string attribute);
    void TestCreation();
    void SaveToConfigAndRegister(Shoutcast sc);
    Shoutcast BuildAndResetToCharacterDefaults();
    Shoutcast BuildAndResetToDefaults();
}