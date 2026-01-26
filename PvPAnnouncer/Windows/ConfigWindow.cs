using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Components;
using Dalamud.Interface.ImGuiNotification;
using Dalamud.Interface.Utility;
using PvpAnnouncer;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;
using PvPAnnouncer.Interfaces.PvPEvents;

namespace PvPAnnouncer.Windows;

public class ConfigWindow : Window, IDisposable
{
    private IEventListenerLoader listenerLoader;
    private BattleTalk[]? _allBattleTalks;

    private readonly Configuration _configuration; public ConfigWindow() : base(
        "PvPAnnouncer Configuration")
    {

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(500, 425),
            MaximumSize = new Vector2(800, 1000)
        };

        SizeCondition = ImGuiCond.Always;

        _configuration = PluginServices.Config;
        listenerLoader = PluginServices.ListenerLoader;
        _allEvents = listenerLoader.GetPvPEvents();
        var v1 = InternalConstants.GetStaticReadOnlyFields<BattleTalk>(typeof(ScionLines));
        var v2 = InternalConstants.GetStaticReadOnlyFields<BattleTalk>(typeof(AnnouncerLines));
        _allBattleTalks = v1.Concat(v2).ToArray()!;
        foreach (var pvPEvent in _allEvents)
        {
            _eventskv.Add(pvPEvent.InternalName, pvPEvent);
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
    private readonly Dictionary<string, PvPEvent> _eventskv = new Dictionary<string, PvPEvent>();
    public override void Draw()
    {
        var disabled = _configuration.Disabled;
        var muted = _configuration.Muted;
        var hideBattleText = _configuration.HideBattleText;
        var lang = _configuration.Language;
        var blEvents = _configuration.BlacklistedEvents;
        var cooldown = _configuration.CooldownSeconds;
        var percent = _configuration.Percent;
        var repeatVoiceLine = _configuration.RepeatVoiceLineQueue;
        var repeatEventCommentary = _configuration.RepeatEventCommentaryQueue;
        var animationDelayFactor = _configuration.AnimationDelayFactor;
        var wolvesDen = _configuration.WolvesDen;
        var notify = _configuration.Notify;
        var icon = _configuration.WantsIcon;
        
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
        var zenos = _configuration.WantsPersonalization(Personalization.ZenosAnnouncer);
        
        if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
        {
            ImGui.Separator();
            ImGui.TextWrapped("Dawntrail is not installed! This plugin needs the expansion installed in order to work!");
            ImGui.Separator();
        }
        
        
        if (ImGui.Button("Test The Announcer"))
        {
            PluginServices.PlayerStateTracker.CheckSoundState();
            var bt = _allBattleTalks.Where(bt => PluginServices.Config.WantsAllPersonalization(bt.Personalization)).ToArray();
            if (bt.Length != 0) // no announcers selected
            {
                
                var e = bt[Random.Shared.Next(bt.Length)];
                PluginServices.Announcer.PlaySound(e.GetPath(PluginServices.Config.Language));
                PluginServices.Announcer.SendBattleTalk(e);
                PluginServices.ChatGui.Print($"Playing Voiceline for {e.Name}", InternalConstants.MessageTag);

                if (!PluginServices.PlayerStateTracker.IsDawntrailInstalled())
                {
                    Notification n = new Notification();
                    n.Title = "Dawntrail Not installed!";
                    n.Type = NotificationType.Error;
                    n.Minimized = false;
                    n.MinimizedText = "Dawntrail is not installed!";
                    n.Content = "You must install Dawntrail for this plugin to work!";
                    PluginServices.NotificationManager.AddNotification(n);
                }
            }
            else
            {
                PluginServices.Announcer.SendBattleTalk(new BattleTalk(InternalConstants.PvPAnnouncerDevName, 0, 5, "You don't have any announcers selected!", [], InternalConstants.PvPAnnouncerDevIcon));
            }
        }
        
        
        if (ImGui.Checkbox("Disabled", ref disabled))
        {
            _configuration.Disabled = disabled;
            _configuration.Save();
        }
        ImGui.Separator();
        
        ImGui.Text("Announcers: ");

        if (ImGui.Checkbox("Alphinaud", ref alphinaud))
        {
            _configuration.TogglePersonalization(Personalization.AlphinaudAnnouncer, alphinaud);
            _configuration.Save();
        }
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Alisaie", ref alisaie))
        {
            _configuration.TogglePersonalization(Personalization.AlisaieAnnouncer, alisaie);
            _configuration.Save();
        }
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Thancred", ref thancred))
        {
            _configuration.TogglePersonalization(Personalization.ThancredAnnouncer, thancred);
            _configuration.Save();
        }
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Urianger", ref urianger))
        {
            _configuration.TogglePersonalization(Personalization.UriangerAnnouncer, urianger);
            _configuration.Save();
        }
       
        if (ImGui.Checkbox("Y'shtola", ref yshtola))
        {
            _configuration.TogglePersonalization(Personalization.YshtolaAnnouncer, yshtola);
            _configuration.Save();
        }
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Estinien", ref estinien))
        {
            _configuration.TogglePersonalization(Personalization.EstinienAnnouncer, estinien);
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("G'raha Tia", ref graha))
        {
            _configuration.TogglePersonalization(Personalization.GrahaAnnouncer, graha);
            _configuration.Save();
        }
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Krile", ref krile))
        {
            _configuration.TogglePersonalization(Personalization.KrileAnnouncer, krile);
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Wuk Lamat", ref wuk))
        {
            _configuration.TogglePersonalization(Personalization.WukLamatAnnouncer, wuk);
            _configuration.Save();
        }
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Koana", ref koana))
        {
            _configuration.TogglePersonalization(Personalization.KoanaAnnouncer, koana);
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("Bakool Ja Ja", ref bjj))
        {
            _configuration.TogglePersonalization(Personalization.BakoolJaJaAnnouncer, bjj);
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("Erenville", ref erenville))
        {
            _configuration.TogglePersonalization(Personalization.ErenvilleAnnouncer, erenville);
            _configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("Zenos", ref zenos))
        {
            _configuration.TogglePersonalization(Personalization.ZenosAnnouncer, zenos);
            _configuration.Save();
        }
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Metem", ref metem))
        {
            _configuration.TogglePersonalization(Personalization.MetemAnnouncer, metem);
            _configuration.Save();
        }

       
        if (metem)
        {
            ImGui.Spacing();
            ImGui.Text("Personalized Voice Lines");
            ImGuiComponents.HelpMarker("These values let Metem use he/she, or directly mention Arcadion fighter names. No other announcers use these features.");

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
        
        if (ImGui.Checkbox("Show Announcer Portrait", ref icon))
        {
            _configuration.WantsIcon = icon;
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
        ImGui.Text("Announcer Language:");
        if (PluginServices.PlayerStateTracker.CheckKRClient() || PluginServices.PlayerStateTracker.CheckCNClient()) 
        {
            if (PluginServices.PlayerStateTracker.CheckCNClient())
            {
                if (ImGui.RadioButton("Chinese", lang.Equals("chs")))
                {
                    _configuration.Language = "chs";
                    _configuration.Save();
                }
                ImGui.SameLine();
                if (ImGui.RadioButton("Japanese", lang.Equals("ja")))
                {
                    _configuration.Language = "ja";
                    _configuration.Save();
                }
            }
        }
        else
        {
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
        ImGui.Separator();
        
   
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
        bool modifiedList = false;
        foreach (var keyValuePair in _eventskv)
        {
            var internalName = keyValuePair.Key;
            var e = keyValuePair.Value;
            if (!blEvents.Contains(internalName))
            {
                if (WantsAnyInEvent(e))
                {
                    activeEvents.Add(e.Name);
                    activeEventsInternal.Add(internalName);
                }
                else
                {
                    modifiedList = true;
                }
             
            }
        }
   
        
        List<string> listDisabledInternal = [];
        List<string> listDisabledPublic = [];
        foreach (string internalName in blEvents)
        {
            var e = _eventskv[internalName];
            if (WantsAnyInEvent(e))
            {
                listDisabledInternal.Add(internalName);
                listDisabledPublic.Add(e.Name);
            }
            else
            {
                modifiedList = true;
            }
           
        }

        if (modifiedList)
        {
            ImGui.Text("Event list looking a little empty?");
            ImGui.SameLine();
            ImGuiComponents.HelpMarker("Some events are hidden as they do not contain any voice lines that would match your settings.");
        }

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

    private bool WantsAnyInEvent(PvPEvent e)
    {
        foreach (var battleTalk in e.SoundPaths())
        {
            if (PluginServices.Config.WantsAllPersonalization(battleTalk.Personalization))
            {
                return true;
            }
        }

        return false;
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
