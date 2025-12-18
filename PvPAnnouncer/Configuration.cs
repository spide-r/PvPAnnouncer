using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Configuration;
using Dalamud.Game.Text;
using Dalamud.Plugin;
using PvPAnnouncer.Data;


namespace PvpAnnouncer
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 2;
        public int RepeatVoiceLineQueue { get; set; } = 3;
        public int RepeatEventCommentaryQueue { get; set; } = 3;
        
        public int CooldownSeconds { get; set; } = 15;
        public bool WantsFem { get; set; } = false;
        public bool WantsMasc { get; set; } = false;
        public bool WantsPersonalizedVoiceLines { get; set; } = false;
        
        public int PersonalizedVoicelines {get; set;} = 0; 
        
        public bool WolvesDen { get; set; } = false;
        public bool Notify { get; set; } = true;

        public List<string> BlacklistedEvents { get; set; } = [];
        
        public bool Disabled { get; set; } = false;
        public bool Muted { get; set; } = false;

        public bool HideBattleText { get; set; } = false;

        public int Percent { get; set; } = 70; 
        
        public bool Spoilers { get; set; } = false;

        public bool WasShownNotification { get; set; } = false;
        
 
        public string Language { get; set; } = "en";
        //public string BattleTalkLang { get; set; } = "en"; //not shown to the end-user until i figure out why i'm unable to get other languages to show up

        [NonSerialized]
        private IDalamudPluginInterface? _pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            _pluginInterface = pluginInterface;
            MigrateOldPluginConfig();
        }

        private void MigrateOldPluginConfig()
        {
            if (Version == 0) //porting due to changes in voice line personalization system
            {
                if (WantsFem)
                {
                    SetPersonalization(Personalization.FemPronouns);
                    WantsPersonalizedVoiceLines = true;
                }

                if (WantsMasc)
                {
                    SetPersonalization(Personalization.MascPronouns);
                    WantsPersonalizedVoiceLines = true;
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
            _pluginInterface?.SavePluginConfig(this);
        }

        public bool WantsPersonalization(Personalization p)
        {
            var personalization = (int) p;
            return WantsPersonalization(personalization);
        }
        
        public bool WantsPersonalization(int p)
        {
            return ((1 << p) & PersonalizedVoicelines) == (1 << p);
        }

        public void SetPersonalization(Personalization p)
        {
            PersonalizedVoicelines = PersonalizedVoicelines | (1 << (int) p);
        }

        public void Save()
        {
            _pluginInterface?.SavePluginConfig(this);
        }
    }
}
