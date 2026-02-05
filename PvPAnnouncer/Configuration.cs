using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Configuration;
using Dalamud.Game.Config;
using Dalamud.Game.Text;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using PvPAnnouncer;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;


namespace PvpAnnouncer
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 5;
        public int RepeatVoiceLineQueue { get; set; } = 3;
        public int RepeatEventCommentaryQueue { get; set; } = 3;
        public int AnimationDelayFactor { get; set; } = 250;
        
        public int CooldownSeconds { get; set; } = 15;
        public bool WantsFem { get; set; } = false;
        public bool WantsMasc { get; set; } = false;
        
        public int PersonalizedVoicelines {get; set;} = 0;

        public Personalization VoicelineSettings = Personalization.MetemAnnouncer;
        
        public bool WolvesDen { get; set; } = false;
        public bool Notify { get; set; } = true;

        public List<string> BlacklistedEvents { get; set; } = [];
        
        public bool Disabled { get; set; } = false;
        public bool Muted { get; set; } = false;

        public bool HideBattleText { get; set; } = false;
        public bool WantsIcon { get; set; } = false;

        public int Percent { get; set; } = 70; 
        
        public bool ShowNotification { get; set; } = false;

        public HashSet<string> Dev_VoLineList { get; set; } = []; //todo remove this crap
        
        //todo:
        //store voiceline definitions
        //store event=>Voiceline[] mapping 
        //store event definitions (later once i've ported voicelines and mapping)
        //preserve readonly defaults, read from them on first load
        //maybe read from resources to get the defaults(press button to reset everything)
        //encourage sharing - export the new stuff maybe
        //find ways to incentivise sharing modification w/ dev
        //maybe only store *modified* shit in this config
        
 
        public string Language { get; set; } = "en";

        [NonSerialized]
        private IDalamudPluginInterface? _pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface, IPlayerStateTracker ps, IGameConfig gameConfig, bool newConfig)
        {
            _pluginInterface = pluginInterface;
            if (newConfig)
            {
                if (ps.CheckCNClient()) 
                {
                    Language = "chs";
                } else if (ps.CheckKRClient())
                {
                    Language = "kr";
                } else
                {
                    gameConfig.TryGet(SystemConfigOption.CutsceneMovieVoice,  out uint configuredLang);
                    gameConfig.TryGet(SystemConfigOption.Language,  out uint clientLang);
                    PluginServices.PluginLog.Verbose($"Lang: {configuredLang}");
                    PluginServices.PluginLog.Verbose($"Lang: {clientLang}");
                    var sw = configuredLang == 4294967295 ? clientLang : configuredLang;
                    Language = sw switch
                    {
                        0 => "ja",
                        1 => "en",
                        2 => "de",
                        3 => "fr",
                        _ => "en"
                    };
                }
                _pluginInterface?.SavePluginConfig(this);
            }

            MigrateOldPluginConfig();
        }

        private void MigrateOldPluginConfig()
        {
            if (Version == 0) //porting due to changes in voice line personalization system
            {
                if (WantsFem)
                {
                    SetPersonalization(Personalization.FemPronouns);
                   // WantsPersonalizedVoiceLines = true;
                }

                if (WantsMasc)
                {
                    SetPersonalization(Personalization.MascPronouns);
                   // WantsPersonalizedVoiceLines = true;
                }
                Version++;
            }

            if (Version == 1) // Port due to new internal name for pvp events
            {
                if (BlacklistedEvents.Count > 0)
                {
                    List<string> oldBL = [];
                    oldBL.AddRange(BlacklistedEvents);
                    BlacklistedEvents.Clear();
                    var oldDict = new Dictionary<string, string>
                    {
                        {"Marksman's Spite (Wicked Thunder)", "AllyMchLBEvent"},
                        {"Seraphism (Wicked Thunder)", "AllySchLbEvent"},
                        {"Tenebrae Lemurum (Wicked Thunder)", "AllyRprLBEvent"},
                        {"Blota", "AllyPullEvent"},
                        {"Rising Phoenix & Flare Star", "AllyFireEvent"},
                        {"Full Swing, Wind's Reply (Brute Bomber)", "AllyKBActionEvent"},
                        {"Swift (Howling Blade)", "AllySwiftEvent"},
                        {"Biolysis (Honey B. Lovely)", "AllyBiolysisEvent"},
                        {"Instant Kills (Brute Bomber + Black Cat)", "AllyInstantKillEvent"},
                        {"Contradance (Honey B. Lovely)", "AllyDncLBEvent"},
                        {"Contradance from enemies.", "EnemyDncLBEvent"},
                        {"Flarethrower (Rival Wings)", "AllyFlarethrowerEvent"},
                        {"Flarethrower from Enemies (Rival Wings)", "EnemyFlarethrowerEvent"},
                        {"Deaths", "AllyDeathEvent"},
                        {"Hit Enemy Hard", "AllyHitEnemyHardEvent"},
                        {"Hit hard by enemy", "AllyHitHardEvent"},
                        {"Hit while under guard", "AllyHitUnderGuardEvent"},
                        {"Limit Breaks", "AllyLimitBreakEvent"},
                        {"Mitigation used", "AllyMitUsedEvent"},
                        {"Pulled By Dark Knight", "AllyPulledByDrkEvent"},
                        {"Resurrection", "AllyResurrectEvent"},
                        {"Fall Damage", "AllyZoneOutEvent"},
                        {"Enemies Fail to hit CC", "EnemyMissedCcEvent"},
                        {"Matches Ending", "MatchEndEvent"},
                        {"Matches Started", "MatchStartEvent"},
                        {"Entered Rival Wings Mech", "EnteredMechEvent"},
                        {"Stormy Weather", "MatchStormyWeatherEvent"},
                        {"Battle High V / Flying High Gained", "MaxBattleFeverEvent"},
                        
                    };
                    foreach (var oldName in oldBL)
                    {
                        if (oldDict.TryGetValue(oldName, out var value))
                        {
                            BlacklistedEvents.Add(value);
                        }
                    }
                }
                Version++;
            }

            if (Version == 2) //2 week spoiler embargo had a personalization Spoiler value of 15 - to remove any wonkyness, remake the personalization int without 15
            {
                for (var i = 1; i < 15; i++)
                {
                    if (WantsPersonalization((Personalization) i))
                    {
                        SetPersonalization((Personalization) i);
                    }
                }
                Version++;
                ShowNotification = false;
            }

            if (Version == 3)
            { // Event Changes
                if (BlacklistedEvents.Contains("AllyHitHardEvent"))
                {
                    BlacklistedEvents.Remove("AllyHitHardEvent");
                    BlacklistedEvents.Add("AllyHitByLimitBreakEvent");
                }
                ShowNotification = true;
                Version++;
            }

            if (Version == 4)
            { // Personalization rework
                for (var i = 1; i < 15; i++)
                {
                    if (((1 << i) & PersonalizedVoicelines) == (1 << i))
                    {
                        SetPersonalization((Personalization) (i - 1));
                    }
                }
                SetPersonalization(Personalization.MetemAnnouncer);
                ShowNotification = true;
                Version++;
            }

            if (Version == 5) // fix ja/jp bug
            {
                if (Language.Equals("jp"))
                {
                    Language = "ja";
                }

                Version++;
            }
            _pluginInterface?.SavePluginConfig(this);
        }

        public bool WantsPersonalization(Personalization p)
        {
            return VoicelineSettings.HasFlag(p);
        }

        public bool WantsAllPersonalization(List<Personalization> ps)
        {
            foreach (var p in ps)
            {
                if (!WantsPersonalization(p))
                {
                    return false;
                }
            }
            return true;
        }

        public void SetPersonalization(Personalization p)
        {
            VoicelineSettings = VoicelineSettings | p;
        }

        public void RemovePersonalization(Personalization toRemove)
        {
            VoicelineSettings &= ~toRemove;
        }

        public void TogglePersonalization(Personalization toSet, bool set)
        {
            if (set)
            {
                SetPersonalization(toSet);
            }
            else
            {
                RemovePersonalization(toSet);
            }
        }
        public void Save()
        {
            _pluginInterface?.SavePluginConfig(this);
        }
    }
}
