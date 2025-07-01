using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using PvpAnnouncer;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration _configuration;
    public ConfigWindow() : base(
        "PvPAnnouncer Configuration")
    {

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(500, 425),
            MaximumSize = new Vector2(800, 1000)
        };

        SizeCondition = ImGuiCond.Always;

        _configuration = PluginServices.Config;
        _allEvents = PluginServices.ListenerLoader.GetPvPEvents();
    }

    public void Dispose() { }

    private int _activeEventsSelectedItem;
    private int _disabledEventsSelectedItem = 0;
    private string[] _activeEventsArr;
    private string[] _disabledEventsArr;
    private readonly PvPEvent[] _allEvents;
    public override void Draw()
    {
        var disabled = _configuration.Disabled;
        var muted = _configuration.Muted;
        var hideBattleText = _configuration.HideBattleText;
        var lang = _configuration.Language;
        //var battleTalkLang = _configuration.BattleTalkLang;
        var blEvents = _configuration.BlacklistedEvents;
        var cooldown = _configuration.CooldownSeconds;
        var percent = _configuration.Percent;
        var repeatVoiceLine = _configuration.RepeatVoiceLineQueue;
        var repeatEventCommentary = _configuration.RepeatEventCommentaryQueue;
        var wolvesDen = _configuration.WolvesDen;
        var notify = _configuration.Notify;
        
        
        var personalization = _configuration.WantsPersonalizedVoiceLines;

        var personalizationInt = _configuration.PersonalizedVoicelines;
        
        //personalization 
        var fem = _configuration.WantsPersonalization(Personalization.FemPronouns);
        var masc = _configuration.WantsPersonalization(Personalization.MascPronouns);
        var bc = _configuration.WantsPersonalization(Personalization.BlackCat);
        var bb = _configuration.WantsPersonalization(Personalization.BruteBomber);
        var hbl = _configuration.WantsPersonalization(Personalization.HoneyBLovely);
        var wt = _configuration.WantsPersonalization(Personalization.WickedThunder);
        var dg = _configuration.WantsPersonalization(Personalization.DancingGreen);
        var sr = _configuration.WantsPersonalization(Personalization.SugarRiot);
        var ba = _configuration.WantsPersonalization(Personalization.BruteAbominator);
        var hb = _configuration.WantsPersonalization(Personalization.HowlingBlade);
        
        if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
        {
            ImGui.Separator();
            ImGui.TextWrapped("Dawntrail is not installed! This plugin needs the expansion installed in order to work!");
            ImGui.Separator();
        }
        
        if (ImGui.Checkbox("Disabled", ref disabled))
        {
            _configuration.Disabled = disabled;
            _configuration.Save();
        }
        
        if(ImGui.Checkbox("Do you want to personalize announcer voicelines?", ref personalization)){
            _configuration.WantsPersonalizedVoiceLines = personalization;
            _configuration.Save();
        };

        if (personalization)
        {
            ImGui.Separator();
            ImGui.TextWrapped(_configuration.PersonalizedVoicelines.ToString());
            ImGui.TextWrapped("Use Voice Lines with: ");
            ImGui.SameLine();
            if (ImGui.Checkbox("Feminine Pronouns", ref fem))
            {
                SetPersonalization(fem, Personalization.FemPronouns);
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.Checkbox("Masculine Pronouns", ref masc))
            {
                SetPersonalization(masc, Personalization.MascPronouns);
                _configuration.Save();
            }
            ImGui.Indent();
        
            ImGui.TextWrapped("Note: These two values allow this plugin to use voice lines usually reserved for the Arcadion fighters.\nFor example: Metem may say \"She's grown wings! How wickedly divine!\" if feminine pronouns are enabled.");
            ImGui.Unindent();
            
            ImGui.TextWrapped("Use voice lines mentioning the following competitors names:");
            
            if (ImGui.Checkbox("Black Cat", ref bc))
            {
                SetPersonalization(bc, Personalization.BlackCat);
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.Checkbox("Honey B. Lovely", ref hbl))
            {
                SetPersonalization(hbl, Personalization.HoneyBLovely);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("Brute Bomber", ref bb))
            {
                SetPersonalization(bb, Personalization.BruteBomber);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("Wicked Thunder", ref wt))
            {
                SetPersonalization(wt, Personalization.WickedThunder);
                _configuration.Save();
            }
            
            if (ImGui.Checkbox("Dancing Green", ref dg))
            {
                SetPersonalization(dg, Personalization.DancingGreen);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("Sugar Riot", ref sr))
            {
                SetPersonalization(sr, Personalization.SugarRiot);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("Brute Abominator", ref ba))
            {
                SetPersonalization(ba, Personalization.BruteAbominator);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("Howling Blade", ref hb))
            {
                SetPersonalization(hb, Personalization.HowlingBlade);
                _configuration.Save();
            }
            ImGui.Separator();
        }

        if (ImGui.Checkbox("Use Voice Lines in the Wolves Den", ref wolvesDen))
        {
            _configuration.WolvesDen = wolvesDen;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Notify me when my Voice volume is muted", ref notify))
        {
            _configuration.Notify = notify;
            _configuration.Save();
        }
        ImGui.Separator();
        ImGui.TextWrapped("What is the minimum amount of seconds to wait between announcements?");
        ImGui.Indent();
        if (ImGui.SliderInt("(S)", ref cooldown, 1, 30))
        {
            _configuration.CooldownSeconds = cooldown;
            _configuration.Save();
        }
        ImGui.Unindent();
        
        
        ImGui.TextWrapped("What percent of Events should have an announcement?");
        ImGui.Indent();
        if (ImGui.SliderInt("%", ref percent, 1, 100))
        {
            _configuration.Percent = percent;
            _configuration.Save();
        }
        ImGui.Unindent();
        
        
        ImGui.TextWrapped("How many unique Voice Lines should be said before a potential repeat?");
        ImGui.Indent();
        if (ImGui.SliderInt("# Of Voice lines", ref repeatVoiceLine, 1, 25))
        {
            _configuration.RepeatVoiceLineQueue = repeatVoiceLine;
            _configuration.Save();
        }
        ImGui.Unindent();
        

        ImGui.TextWrapped("How many unique Events should be commented on before a duplicate happens?");
        ImGui.Indent();
        if (ImGui.SliderInt("# Of Events", ref repeatEventCommentary, 1, 10))
        {
            _configuration.RepeatEventCommentaryQueue = repeatEventCommentary;
            _configuration.Save();
        }
        ImGui.Unindent();
        ImGui.Separator();
        if (!muted)
        {
            ImGui.Text("Announcer Language:");
            if (ImGui.RadioButton("English", lang.Equals("en")))
            {
                _configuration.Language = "en";
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.RadioButton("German", lang.Equals("de")))
            {
                _configuration.Language = "de";
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.RadioButton("French", lang.Equals("fr")))
            {
                _configuration.Language = "fr";
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.RadioButton("Japanese", lang.Equals("ja")))
            {
                _configuration.Language = "ja";
                _configuration.Save();
            }
        }

        /*if (!hideBattleText)
        {
            ImGui.Text("Battle Text Language:");
            if (ImGui.RadioButton("English Text", battleTalkLang.Equals("en")))
            {
                _configuration.BattleTalkLang = "en";
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.RadioButton("German Text", battleTalkLang.Equals("de")))
            {
                _configuration.BattleTalkLang = "de";
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.RadioButton("French Text", battleTalkLang.Equals("fr")))
            {
                _configuration.BattleTalkLang = "fr";
                _configuration.Save();
            }
            ImGui.SameLine();
            if (ImGui.RadioButton("Japanese Text", battleTalkLang.Equals("ja")))
            {
                _configuration.BattleTalkLang = "ja";
                _configuration.Save();
            }
        }*/
   
        if (ImGui.Checkbox("Mute Announcer", ref muted))
        {
            _configuration.Muted = muted;
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("Hide Battle Text", ref hideBattleText))
        {
            _configuration.HideBattleText = hideBattleText;
            _configuration.Save();
        }
        ImGui.Separator();
        
        List<String> list = new List<string>();
        foreach (var ee in _allEvents)
        {
            string name = ee.Name;
            if (!blEvents.Contains(name) && !name.Contains("Not Implemented"))
            {
                list.Add(ee.Name);

            }
        }
        _activeEventsArr = list.ToArray();
        ImGui.ListBox("Enabled Events", ref _activeEventsSelectedItem, _activeEventsArr, _activeEventsArr.Length);
        if (ImGui.Button("Disable"))
        {
            _configuration.BlacklistedEvents.Add(_activeEventsArr[_activeEventsSelectedItem]);
            _configuration.Save();
        }
        
        List<String> listDisabled = new List<string>();
        foreach (String ee in blEvents)
        {
            listDisabled.Add(ee);
        }

        foreach (var ee in _allEvents)
        {
            string name = ee.Name;
            if (name.Contains("Not Implemented"))
            {
                listDisabled.Add(ee.Name);

            }
        }
        _disabledEventsArr = listDisabled.ToArray();
        ImGui.ListBox("Disabled Events", ref _disabledEventsSelectedItem, _disabledEventsArr, _disabledEventsArr.Length);
        if (ImGui.Button("Enable"))
        {
            if (!_disabledEventsArr[_disabledEventsSelectedItem].Contains("Not Implemented"))
            {
                _configuration.BlacklistedEvents.Remove(_disabledEventsArr[_disabledEventsSelectedItem]);
                _configuration.Save();
            }
            
        }
        
        ImGui.TextWrapped("More events will be added! I may also let you create custom events. Please let me know if this is something you would like to see!");
    }

    private void RemovePersonalization(Personalization toRemove)
    {
        var persInt = 0;
        for (int i = 1; i < 11; i++) //todo: sloppy - hardcoded value of 11
        {
            if ((int) toRemove != i)
            {
                if (_configuration.WantsPersonalization(i))
                {
                    persInt = persInt | (1 << i);
                }
            }
        }
        _configuration.PersonalizedVoicelines = persInt;
        
    }
    private void SetPersonalization(bool b, Personalization p)
    {
        if (b)
        {
            _configuration.SetPersonalization(p);
        }
        else
        {
            RemovePersonalization(p);
        }
    }
}
