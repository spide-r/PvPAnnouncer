using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Client.UI;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;
using Enumerable = System.Linq.Enumerable;

namespace PvPAnnouncer.Impl;

public class Announcer(IEventShoutcastMapping eventShoutcastMapping, IShoutcastRepository shoutcastRepository)
    : IAnnouncer
{
    private readonly Queue<string> _lastEvents = new();
    /*
     * Objectives:
     * 1. Comment a Reasonable amount
     * 2. Dont repeat voice lines
     * 3. Don't comment on the same thing twice
     * 4. Don't say more than one thing at a time or comment too quickly
     * 5. Use the appropriate gender for the challenger
     */

    //todo rewrite - divide and conquer
    // per announcer %


    private readonly Queue<string> _lastVoiceLines = new();
    private int _lastVoiceLineLength = 0;
    private long _timestamp;

    public void ClearQueue()
    {
        _lastVoiceLines.Clear();
        _lastEvents.Clear();
    }

    public void PlayAndSendBattleTalkForTesting(Shoutcast shoutcast)
    {
        var p = shoutcast.GetShoutcastSoundPathWithGenderAndLang(PluginServices.Config.Language,
            PluginServices.Config.WantsAttribute("Feminine Pronouns"));

        PlaySound(p);
        SendBattleTalk(shoutcast);
    }

    public void ReceivePvPEvent(bool bypass, PvPEvent pvpEvent)
    {
        PluginServices.PluginLog.Verbose($"PvP Event {pvpEvent.Id} received");
        long newTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        long diff = newTimestamp - _timestamp;

        // == Objective 4 ==
        if (diff < (PluginServices.Config.CooldownSeconds + _lastVoiceLineLength))
        {
            PluginServices.PluginLog.Verbose($"Cooldown not finished");
            return;
        }

        if (bypass)
        {
            PluginServices.PluginLog.Verbose("Bypassing randomness & repeat commentary.");
            PlaySoundAndSendBattleTalk(true, pvpEvent);
            return;
        }

        if (!PluginServices.DutyState
                .IsDutyStarted) //fixes kardia and other stuff at start but does not allow for weather events
        {
            PluginServices.PluginLog.Verbose("Duty not started!");
            return;
        }


        int rand = Random.Shared.Next(100);


        // == Objective 1 ==
        if (rand < 100 - PluginServices.Config.Percent)
        {
            PluginServices.PluginLog.Verbose(
                $"Percent not hit. is {rand} when it should be greater than {100 - PluginServices.Config.Percent}");

            return;
        }

        // == Objective 3 == 
        if (FailsRepeatCommentaryCheck(pvpEvent))
        {
            //PluginServices.PluginLog.Verbose($"Repeat commentary check failed");
            return;
        }

        PlaySoundAndSendBattleTalk(pvpEvent);
    }

    public void ReceivePvPEvent(PvPEvent pvpEvent)
    {
        ReceivePvPEvent(false, pvpEvent);
    }

    public void PlaySound(string sound)
    {
        if (!PluginServices.DataManager.FileExists(sound))
        {
            PluginServices.PluginLog.Error($"Sound file {sound} not found!");
            return;
        }

        PluginServices.PluginLog.Verbose($"Playing sound: {sound}");
        PluginServices.SoundManager.PlaySound(sound);
    }

    public void SendBattleTalk(Shoutcast shoutcast)
    {
        if (PluginServices.Config.HideBattleText) return;
        var transcription = "";
        PluginServices.PluginLog.Verbose(shoutcast.ToString());
        if (shoutcast.GetTranscriptionWithGender(PluginServices.Config.TextLanguage,
                PluginServices.Config.WantsAttribute("Feminine Pronouns"), PluginServices.SeStringEvaluator).Equals(""))
        {
            if (shoutcast.GetTranscriptionWithGender("en", false, PluginServices.SeStringEvaluator).Equals(""))
            {
                PluginServices.PluginLog.Error($"Text empty for {shoutcast.Shoutcaster}, {shoutcast.SoundPath}");
                return;
            }

            transcription = shoutcast.GetTranscriptionWithGender("en",
                PluginServices.Config.WantsAttribute("Feminine Pronouns"), PluginServices.SeStringEvaluator);
            PluginServices.PluginLog.Warning(
                $"Text empty for {shoutcast.Shoutcaster}, {shoutcast.SoundPath} on lang {PluginServices.Config.TextLanguage} - falling back to EN");
        }
        else
        {
            transcription = shoutcast.GetTranscriptionWithGender(PluginServices.Config.TextLanguage,
                PluginServices.Config.WantsAttribute("Feminine Pronouns"), PluginServices.SeStringEvaluator);
        }

        unsafe
        {
            try
            {
                var name = shoutcast.Shoutcaster;
                var duration = shoutcast.Duration;
                var icon = shoutcast.Icon;
                var style = shoutcast.Style;
                if (icon != 0 && PluginServices.Config.WantsIcon)
                    UIModule.Instance()->ShowBattleTalkImage(name, transcription, duration, icon, style);
                else
                    UIModule.Instance()->ShowBattleTalk(name, transcription, duration, style);
            }
            catch (InvalidOperationException)
            {
                UIModule.Instance()->ShowBattleTalk(InternalConstants.PvPAnnouncerDevName,
                    InternalConstants.ErrorContactDev, 6, 6);
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "Issue sending Battle Talk!");
            }
        }
    }


    private void AddEventToRecentList(PvPEvent e)
    {
        PluginServices.PluginLog.Verbose("Adding Event to history");
        if (_lastEvents.Count > PluginServices.Config.RepeatEventCommentaryQueue - 1)
        {
            PluginServices.PluginLog.Verbose($"Dequeuing Event from history");

            _lastEvents.Dequeue();
        }

        _lastEvents.Enqueue(e.Id);
    }


    private void AddVoiceLineToRecentList(Shoutcast talk)
    {
        PluginServices.PluginLog.Verbose($"Adding Voice line {talk.SoundPath} to history");

        if (_lastVoiceLines.Count > PluginServices.Config.RepeatVoiceLineQueue - 1)
        {
            PluginServices.PluginLog.Verbose($"Dequeuing Voice Line from history");

            _lastVoiceLines.Dequeue();
        }

        _lastVoiceLines.Enqueue(talk.Id);
    }

    private bool FailsRepeatCommentaryCheck(PvPEvent pvpEvent)
    {
        bool b = _lastEvents.Contains(pvpEvent.Id);
        foreach (var lastEvent in _lastEvents)
        {
            PluginServices.PluginLog.Verbose($"Last Event: {lastEvent}");
        }

        PluginServices.PluginLog.Verbose($"Repeat commentary check triggered - value is {b}");
        return b;
    }


    private void PlaySoundAndSendBattleTalk(bool bypass, PvPEvent pvpEvent)
    {
        List<Shoutcast> sounds = [];

        foreach (var shoutcastId in eventShoutcastMapping.GetShoutcastList(pvpEvent.Id))
        {
            var sound = shoutcastRepository.GetShoutcast(shoutcastId);
            if (sound == null)
            {
                PluginServices.PluginLog.Warning($"{shoutcastId} not found.");
                continue;
            }

            if (!PluginServices.DataManager.FileExists(
                    sound.GetShoutcastSoundPathWithLang(PluginServices.Config.Language)))
            {
                PluginServices.PluginLog.Error($"Sound file {sound} not found!");
                continue;
            }

            if (PluginServices.Config.MutedShouts.Contains(shoutcastId)) continue;

            // == Objective 5 == 
            if (PluginServices.Config.WantsAllAttributes(sound.Attributes) &&
                PluginServices.Config.WantsAttribute(sound.Shoutcaster))
            {
                if (sound.IsGendered)
                {
                    var masc = PluginServices.Config.WantsAttribute("Masculine Pronouns");
                    var fem = PluginServices.Config.WantsAttribute("Feminine Pronouns");
                    if (masc || fem) // voice is gendered and user wants at least 1
                    {
                        sounds.Add(sound);
                    }
                }
                else
                {
                    sounds.Add(sound);
                }
            }
        }

        // == Objective 2 == 
        if (!bypass)
        {
            foreach (var sound in Enumerable.Where(Enumerable.ToList(sounds),
                         sound => _lastVoiceLines.Contains(sound.Id)))
            {
                sounds.Remove(sound);
            }
        }

        if (sounds.Count < 1)
        {
            PluginServices.PluginLog.Verbose(
                $"Sound list after customization removal is less than 1 for {pvpEvent.Id}");
            return;
        }

        int rand = Random.Shared.Next(sounds.Count);
        var s = sounds[rand];
        WrapUp(pvpEvent, s);
        PluginServices.Framework.RunOnTick(async () =>
        {
            PluginServices.PluginLog.Verbose(
                $"Playing announcement (Delaying): {s.SoundPath} by {PluginServices.Config.AnimationDelayFactor}");
            await Task.Delay(PluginServices.Config
                .AnimationDelayFactor); //delay to prevent shenanigans w/ attacks being announced before their animations finish
            var p = s.GetShoutcastSoundPathWithGenderAndLang(PluginServices.Config.Language,
                PluginServices.Config.WantsAttribute("Feminine Pronouns"));
            PlaySound(p);
            SendBattleTalk(s);
            PluginServices.PluginLog.Verbose($"Finished Playing announcement after delay: {s}");
        });
    }

    public void PlaySoundAndSendBattleTalk(PvPEvent pvpEvent)
    {
        PlaySoundAndSendBattleTalk(false, pvpEvent);
    }

    private void WrapUp(PvPEvent pvpEvent, Shoutcast? chosenLine)
    {
        AddEventToRecentList(pvpEvent);
        if (chosenLine != null)
        {
            AddVoiceLineToRecentList(chosenLine);
            _lastVoiceLineLength = chosenLine.Duration;
        }

        _timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}