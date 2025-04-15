using System;
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
        
        public int CooldownSeconds { get; set; } = 6;
        public bool WantsFem { get; set; } = false;
        public bool WantsMasc { get; set; } = false;
        public bool WantsLightParty { get; set; } = true;
        public bool WantsFullParty { get; set; } = false;

        public List<string> BlacklistedEvents { get; set; } = [];
        
        public bool Disabled { get; set; } = false;
        public bool Muted { get; set; } = false;

        public string Language { get; set; } = "en";

        [NonSerialized]
        private IDalamudPluginInterface? _pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            _pluginInterface = pluginInterface;
        }

        public void Save()
        {
            _pluginInterface!.SavePluginConfig(this);
        }
    }
}
