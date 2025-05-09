﻿using System;
using System.Collections.Generic;
using Dalamud.Configuration;
using Dalamud.Game.Text;
using Dalamud.Plugin;


namespace PvpAnnouncer
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;
        public int RepeatVoiceLineQueue { get; set; } = 3;
        public int RepeatEventCommentaryQueue { get; set; } = 3;
        
        public int CooldownSeconds { get; set; } = 15;
        public bool WantsFem { get; set; } = false;
        public bool WantsMasc { get; set; } = false;
        public bool WolvesDen { get; set; } = false;
        public bool Notify { get; set; } = true;

        public List<string> BlacklistedEvents { get; set; } = [];
        
        public bool Disabled { get; set; } = false;
        public bool Muted { get; set; } = false;

        public bool HideBattleText { get; set; } = false;

        public int Percent { get; set; } = 70; 
 
        public string Language { get; set; } = "en";
        //public string BattleTalkLang { get; set; } = "en"; //not shown to the end-user until i figure out why i'm unable to get other languages

        [NonSerialized]
        private IDalamudPluginInterface? _pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            _pluginInterface = pluginInterface;
        }

        public void Save()
        {
            _pluginInterface?.SavePluginConfig(this);
        }
    }
}
