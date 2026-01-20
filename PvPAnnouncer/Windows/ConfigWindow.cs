using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Components;
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
        foreach (var pvPEvent in _allEvents)
        {
            _eventskv.Add(pvPEvent.InternalName, pvPEvent.Name);
        }
    }

    public void Dispose() { }

    private int _activeEventsSelectedItem;
    private int _disabledEventsSelectedItem = 0;
    private string[] _activeEventsArr = [];
    private string[] _activeEventsArrInternal = [];
    private string[] _disabledEventsArr = [];
    private string[] _disabledEventsArrInternal = [];
    private readonly PvPEvent[] _allEvents;
    private readonly Dictionary<string, string> _eventskv = new Dictionary<string, string>();
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
        var animationDelayFactor = _configuration.AnimationDelayFactor;
        var wolvesDen = _configuration.WolvesDen;
        var notify = _configuration.Notify;
        
        
        var personalization = _configuration.WantsPersonalizedVoiceLines;
        
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
        var vf = _configuration.WantsPersonalization(Personalization.VampFatale);
        var dbrh = _configuration.WantsPersonalization(Personalization.DeepBlueRedHot);
        var tt = _configuration.WantsPersonalization(Personalization.Tyrant);
        var pr = _configuration.WantsPersonalization(Personalization.President);
        
        // Announcers
        var metem = _configuration.WantsPersonalization(Personalization.MetemAnnouncer);
        var alphinaud = _configuration.WantsPersonalization(Personalization.AlphinaudAnnouncer);
        var alisaie = _configuration.WantsPersonalization(Personalization.AlisaieAnnouncer);
        var thancred = _configuration.WantsPersonalization(Personalization.ThancredAnnouncer);
        var urianger = _configuration.WantsPersonalization(Personalization.UriangerAnnouncer);
        var yshtola = _configuration.WantsPersonalization(Personalization.YshtolaAnnouncer);
        var estinien = _configuration.WantsPersonalization(Personalization.EstinienAnnouncer);
        var graha = _configuration.WantsPersonalization(Personalization.GrahaAnnouncer);
        var krile = _configuration.WantsPersonalization(Personalization.KrileAnnouncer);
        var wuk = _configuration.WantsPersonalization(Personalization.WukLamatAnnouncer);
        var koana = _configuration.WantsPersonalization(Personalization.KoanaAnnouncer);
        var bjj = _configuration.WantsPersonalization(Personalization.BakoolJaJaAnnouncer);
        var erenville = _configuration.WantsPersonalization(Personalization.ErenvilleAnnouncer);
        
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
        if (metem)
        {
            ImGui.Text("Personalized Voice Lines");
            ImGuiComponents.HelpMarker("These values let Metem use he/she, or directly mention Arcadion fighter names. As of now, no other announcers use these features.");

            ImGui.Separator();
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
            ImGuiComponents.HelpMarker("These two values allow this plugin to use voice lines usually reserved for the Arcadion fighters.\nMetem may say \"She's grown wings! How wickedly divine!\" if feminine pronouns are enabled.");
            
            ImGui.TextWrapped("Use announcer voice lines mentioning the following competitors names:");
            ImGuiComponents.HelpMarker("This allows Metem to mention Arcadion fighters directly.\nFor example: \"The Honey B. Lovely show has begun!\"");
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

            if (ImGui.Checkbox("Vamp Fatale", ref vf))
            {
                SetPersonalization(vf, Personalization.VampFatale);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("Deep Blue & Red Hot", ref dbrh))
            {
                SetPersonalization(dbrh, Personalization.DeepBlueRedHot);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("The Tyrant", ref tt))
            {
                SetPersonalization(tt, Personalization.Tyrant);
                _configuration.Save();
            }
            ImGui.SameLine();

            if (ImGui.Checkbox("The President", ref pr))
            {
                SetPersonalization(pr, Personalization.President);
                _configuration.Save();
            }
            
            
            ImGui.Separator();
        }

        if (ImGui.Checkbox("Use Voice Lines in the Wolves Den", ref wolvesDen))
        {
            _configuration.WolvesDen = wolvesDen;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Notify when Voice Volume is muted", ref notify))
        {
            _configuration.Notify = notify;
            _configuration.Save();
        }
        ImGui.Separator();
        ImGui.TextWrapped("Minimum delay between announcements");
        ImGui.Indent();
        if (ImGui.SliderInt("###SliderCooldown", ref cooldown, 1, 120,"%ds"))
        {
            _configuration.CooldownSeconds = cooldown;
            _configuration.Save();
        }
        ImGui.Unindent();
        
        
        ImGui.TextWrapped("Announcement Frequency");
        ImGuiComponents.HelpMarker("This controlls the chance of Metem announcing any given event.");
        ImGui.Indent();

        if (ImGui.SliderInt("###SliderPercent", ref percent, 1, 100, "%d%%"))
        {
            _configuration.Percent = percent;
            _configuration.Save();
        }
        ImGui.Unindent();
        
        ImGui.TextWrapped("Announcement Delay");
        ImGuiComponents.HelpMarker("Sometimes Metem announces a split-second too early. This setting adds a very minor delay which should prevent announcements before an action finishes.");
        ImGui.Indent();

        if (ImGui.SliderInt("###SliderAnimationFactor", ref animationDelayFactor, 250, 2000, "%dms"))
        {
            _configuration.AnimationDelayFactor = animationDelayFactor;
            _configuration.Save();
        }
        ImGui.Unindent();
        
        
        ImGui.TextWrapped("Minimum unique voice lines to play before a repeat is allowed.");
        ImGui.Indent();
        if (ImGui.SliderInt("##SliderVoicelines", ref repeatVoiceLine, 1, 25))
        {
            _configuration.RepeatVoiceLineQueue = repeatVoiceLine;
            _configuration.Save();
        }
        ImGui.Unindent();
        

        ImGui.TextWrapped("Minimum number of events to announce before a repeat is allowed.");
        ImGui.Indent();
        if (ImGui.SliderInt("###SliderEvents", ref repeatEventCommentary, 1, 10))
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
        
        List<string> activeEvents = new List<string>();
        List<string> activeEventsInternal = new List<string>();
        foreach (var keyValuePair in _eventskv)
        {
            string internalName = keyValuePair.Key;
            string publicName = keyValuePair.Value;
            if (!blEvents.Contains(internalName))
            {//todo: before you update, this must be filtered based on the actual settings and not a stupid .contains 
                if (publicName.ToLower().Contains("masculine") && !masc)
                {
                    continue;
                }

                if (publicName.ToLower().Contains("feminine") && !fem)
                {
                    continue;
                }
                activeEvents.Add(publicName);
                activeEventsInternal.Add(internalName);
            }
        }
        /*foreach (var ee in _allEvents)
        {
            string name = ee.Name;
            if (!blEvents.Contains(name) && !name.Contains("Not Implemented"))
            {
                activeEvents.Add(ee.Name);

            }
        }*/
        _activeEventsArr = activeEvents.ToArray();
        _activeEventsArrInternal = activeEventsInternal.ToArray();
        ImGui.Text("Enabled Events:");
        ImGui.ListBox("###EnabledEvents", ref _activeEventsSelectedItem, _activeEventsArr);
        if (ImGui.Button("Disable"))
        {
            if (_activeEventsSelectedItem < _activeEventsArrInternal.Length)
            {
                _configuration.BlacklistedEvents.Add(_activeEventsArrInternal[_activeEventsSelectedItem]);
                _configuration.Save();
                
            }

        }
        
        List<string> listDisabledInternal = [];
        List<string> listDisabledPublic = [];
        foreach (string internalName in blEvents)
        {
            var publicName = _eventskv[internalName];
            if (publicName.ToLower().Contains("masculine") && !masc)
            {
                continue;
            }

            if (publicName.ToLower().Contains("feminine") && !fem)
            {
                continue;
            }
            listDisabledInternal.Add(internalName);
            listDisabledPublic.Add(_eventskv.First(pair => pair.Key.Equals(internalName)).Value);
        }

     
        _disabledEventsArrInternal = listDisabledInternal.ToArray();
        _disabledEventsArr = listDisabledPublic.ToArray();
        ImGui.Text("Disabled Events:");
        ImGui.ListBox("###DisabledEvents", ref _disabledEventsSelectedItem, _disabledEventsArr);
        if (ImGui.Button("Enable"))
        {
            if (_disabledEventsSelectedItem < _disabledEventsArrInternal.Length)
            {
                _configuration.BlacklistedEvents.Remove(_disabledEventsArrInternal[_disabledEventsSelectedItem]);
                _configuration.Save();
                
            }
    
        }
    }
    
    private void SetPersonalization(bool b, Personalization p)
    {
        if (b)
        {
            _configuration.SetPersonalization(p);
        }
        else
        {
            _configuration.RemovePersonalization(p);
        }
    }
}
