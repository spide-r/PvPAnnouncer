using System.Collections.Generic;
using Dalamud.Plugin.Services;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class VoicelineCreationController(IDataManager dataManager) : IVoicelineCreationController
{
    private readonly ShoutcastBuilder _shoutcastBuilder = new(dataManager);

    public void SelectBattleTalk(uint voLine)
    {
        _shoutcastBuilder.WithContentDirectorBattleTalkVo(voLine);
    }

    public void SelectCutsceneLine(string cutsceneLine)
    {
        _shoutcastBuilder.WithCutsceneLine(cutsceneLine);
    }

    public void SelectOrphanedVoLine(uint voLine)
    {
        _shoutcastBuilder.WithSoundPath("sound/voice/vo_line/" + voLine);
    }

    public void SelectNpcYell(uint row)
    {
        _shoutcastBuilder.WithNpcYell(row);
    }

    public void SelectInstanceContentTextDataRow(uint row)
    {
        _shoutcastBuilder.WithInstanceContentTextDataRow(row);
    }

    public void SelectAnnouncementId(string announcementId)
    {
        _shoutcastBuilder.WithId(announcementId);
    }

    public void SelectIcon(uint icon)
    {
        _shoutcastBuilder.WithIcon(icon);
    }

    public void SelectName(string name)
    {
        _shoutcastBuilder.WithShoutcaster(name);
    }

    public void SelectStyle(byte style)
    {
        _shoutcastBuilder.WithStyle(style);
    }

    public void SetDuration(byte duration)
    {
        _shoutcastBuilder.WithDuration(duration);
    }

    public void SetManualTranscription(string lang, string transcription)
    {
        var t = _shoutcastBuilder.GetShoutcastMidConstruction().Transcription;
        t[lang] = transcription;
        _shoutcastBuilder.WithTranscription(t);
    }

    public void SetAttributes(List<string> attributes)
    {
        _shoutcastBuilder.WithAttributes(attributes);
    }

    public void AddAttribute(string attribute)
    {
        var a = _shoutcastBuilder.GetShoutcastMidConstruction().Attributes;
        a.Add(attribute);
        _shoutcastBuilder.WithAttributes(a);
    }

    public void TestCreation()
    {
        var sc = _shoutcastBuilder.BuildAndPreserveProperties();
        PluginServices.Announcer.PlayAndSendBattleTalkForTesting(sc);
    }

    public void SaveToConfigAndRegister(Shoutcast sc)
    {
        var json = PluginServices.JsonLoader.BuildJsonShout(sc);
        PluginServices.Config.AddCustomShoutCast(sc.Id, json.ToJsonString());
        PluginServices.Config.Save();
        PluginServices.ShoutcastRepository.SetShoutcast(sc.Id, sc);
        PluginServices.CasterRepository.RegisterAttribute(sc.Shoutcaster);
        foreach (var scAttribute in sc.Attributes) PluginServices.AttributeRepository.RegisterAttribute(scAttribute);

        PluginServices.PluginLog.Verbose("Saved Json: " + json);
        PluginServices.ChatGui.Print($"Saved Shoutcast from {sc.Shoutcaster} with ID {sc.Id}");
    }

    public Shoutcast BuildAndResetToCharacterDefaults()
    {
        var sc = _shoutcastBuilder.BuildAndPreserveCharacter();
        return sc;
    }

    public Shoutcast BuildAndResetToDefaults()
    {
        var sc = _shoutcastBuilder.BuildAndRefreshProperties();
        return sc;
    }
}