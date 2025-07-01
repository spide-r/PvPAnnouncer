using System;
using System.Collections.Generic;
using Dalamud.Configuration;
using Dalamud.Game.Text;
using Dalamud.Plugin;
using PvPAnnouncer.Data;


namespace PvpAnnouncer
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 1;
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
            
            _pluginInterface?.SavePluginConfig(this);
        }

        public bool WantsPersonalization(Personalization p)
        {
            var personalization = (int) p;
            return ((1 << personalization) & PersonalizedVoicelines) == (1 << personalization);
        }

        public void SetPersonalization(Personalization p)
        {
            PersonalizedVoicelines = PersonalizedVoicelines | (1 << (int) p);
        }
        
        public void UnSetPersonalization(Personalization p)
        {
            PersonalizedVoicelines = PersonalizedVoicelines | (1 >> (int) p);
        }

        public void Save()
        {
            _pluginInterface?.SavePluginConfig(this);
        }
    }
}
