using System;
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
        ClearTextData();
        ClearAudioData();
        _shoutcastBuilder.WithContentDirectorBattleTalkVo(voLine);
    }

    public void SelectCutsceneLine(string cutsceneLine)
    {
        ClearTextData();
        ClearAudioData();
        _shoutcastBuilder.WithCutsceneLine(cutsceneLine);
    }

    public void SelectOrphanedVoLine(uint voLine)
    {
        ClearAudioData();
        _shoutcastBuilder.WithSoundPath("sound/voice/vo_line/" + voLine);
    }

    public void SelectNpcYell(uint row)
    {
        ClearTextData();
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
        ClearTextData(); //if people are kind enough to transcribe in multiple languages only one will stick. is this worth fixing?
        var t = _shoutcastBuilder.GetShoutcastMidConstruction().Transcription;
        t[lang] = transcription;
        _shoutcastBuilder.WithTranscription(t);
    }

    public void ClearManualTranscription()
    {
        _shoutcastBuilder.WithTranscription([]);
    }

    public void SetAttributes(List<string> attributes)
    {
        _shoutcastBuilder.WithAttributes(attributes);
    }

    public void AddAttribute(string attribute)
    {
        _shoutcastBuilder.AddAttribute(attribute);
    }

    public void TestCreation()
    {
        try
        {
            var sc = _shoutcastBuilder.FromShoutcast(_shoutcastBuilder.GetShoutcastMidConstruction(),
                PluginServices.DataManager);
            PluginServices.Announcer.PlayAndSendBattleTalkForTesting(sc.BuildAndPreserveCharacter());
        }
        catch (InvalidOperationException e)
        {
            PluginServices.ChatGui.PrintError(e.Message, InternalConstants.MessageTag);
        }
    }

    public void ClearTextData()
    {
        _shoutcastBuilder.WithTranscription([]);
        _shoutcastBuilder.WithNpcYell(0);
        _shoutcastBuilder.WithInstanceContentTextDataRow(0);
    }

    public void ClearAudioData()
    {
        _shoutcastBuilder.WithContentDirectorBattleTalkVo(0);
        _shoutcastBuilder.WithCutsceneLine("");
        _shoutcastBuilder.WithSoundPath("");
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

    public void ResetToDefaults()
    {
        _shoutcastBuilder.RefreshProperties();
    }

    public Shoutcast GetCurrentShoutcast()
    {
        return _shoutcastBuilder.GetShoutcastMidConstruction();
    }
}