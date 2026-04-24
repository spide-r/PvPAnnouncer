using System;
using System.Linq;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Textures;
using Dalamud.Utility;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class VoicelineCreationViewer(IVoicelineDataResolver dataResolver) : IVoicelineCreationViewer
{
    public void ShowCharacterData(Shoutcast shoutcast) // name, icon, style
    {
        ImGui.Text("Name: " + shoutcast.Shoutcaster);
        if (shoutcast.Icon != 0)
            try
            {
                var iconImage = PluginServices.TextureProvider.GetFromGameIcon(new GameIconLookup(shoutcast.Icon))
                    .GetWrapOrDefault();
                if (iconImage != null) ImGui.Image(iconImage.Handle, new Vector2(250, 210));
            }
            catch (Exception)
            {
                ImGui.Text("Issue rendering icon!");
            }
        else
            ImGui.Text("No Icon Selected!");

        ImGui.Text("Style: " + shoutcast.Style);
    }

    public void ShowSelectedAudio(Shoutcast shoutcast) //voLine, ctrBA
    {
        if (!shoutcast.SoundPath.IsNullOrEmpty())
            ImGui.TextWrapped("Chosen Sound Path: " + shoutcast.SoundPath);
        if (shoutcast.ContentDirectorBattleTalkVo != 0)
            ImGui.Text("Battle Talk Row: " + shoutcast.ContentDirectorBattleTalkVo);
        if (!shoutcast.CutsceneLine.IsNullOrEmpty())
            ImGui.Text("Cutscene Line: " + shoutcast.CutsceneLine);
    }

    public void ShowSelectedText(Shoutcast shoutcast) // transcription of the default lang
    {
        if (shoutcast.Transcription.TryGetValue(PluginServices.Config.Language, out var value))
            ImGui.Text("Text Transcription: " + value);

        if (shoutcast.NpcYell != 0)
        {
            ImGui.Text("NPC Yell: " + shoutcast.NpcYell);
            ImGui.TextWrapped(dataResolver.ResolveTextWithNpcYell(shoutcast.NpcYell));
        }

        if (!shoutcast.CutsceneLine.IsNullOrEmpty())
        {
            ImGui.Text("Cutscene Line: " + shoutcast.CutsceneLine);
            ImGui.Text("Text: " + dataResolver.ResolveCutsceneLineWithTag(shoutcast.CutsceneLine));
        }

        if (shoutcast.InstanceContentTextDataRow != 0)
        {
            ImGui.Text("Text Data Row: " + shoutcast.InstanceContentTextDataRow);
            ImGui.Text("Text: " + dataResolver.ResolveTextWithIctdRow(shoutcast.InstanceContentTextDataRow));
        }
    }

    public string GetText(Shoutcast shoutcast)
    {
        if (shoutcast.Transcription.TryGetValue(PluginServices.Config.Language, out var value))
            return value;

        if (shoutcast.NpcYell != 0) return dataResolver.ResolveTextWithNpcYell(shoutcast.NpcYell);

        if (!shoutcast.CutsceneLine.IsNullOrEmpty())
            return dataResolver.ResolveCutsceneLineWithTag(shoutcast.CutsceneLine);

        if (shoutcast.InstanceContentTextDataRow != 0)
            return dataResolver.ResolveTextWithIctdRow(shoutcast.InstanceContentTextDataRow) ?? "";

        return "";
    }

    public void ShowMetadata(Shoutcast shoutcast)
    {
        ImGui.Text("Shoutcast ID: " + shoutcast.Id);
        ImGui.Text("Shoutcast Duration: " + shoutcast.Duration);
        ImGui.Text("Shoutcast Attributes:");

        var attr =
            shoutcast.Attributes.Aggregate("", (current, shoutcastAttribute) => current + ", " + shoutcastAttribute);
        if (attr.Length > 0)
            ImGui.TextWrapped(attr[1..]);
        else
            ImGui.Text("No Attributes Selected.");
    }

    public void ShowObject(Shoutcast shoutcast)
    {
        if (ImGui.CollapsingHeader("Raw Data (For Nerds):")) ImGui.TextWrapped(shoutcast.ToString());
    }
}