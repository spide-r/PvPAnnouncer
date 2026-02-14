using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json.Nodes;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;

namespace PvPAnnouncer.Windows;

public class DevWindow: Window, IDisposable
{
    public DevWindow() : base(
        "PvPAnnouncer Dev Window")
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(450, 225),
        };
        
    }
    public override void Draw()
    {
        if (ImGui.Button("Open Voice Line Creation Window"))
        {
            PluginServices.VoicelineCreationWindow.Toggle();
        }
        if (ImGui.Button("Open Voice Line Mapping Window"))
        {
            PluginServices.VoicelineMappingWindow.Toggle();
        }

        //todo reset button
        //todo export button
    }
    private void ActionEventCreator()
    {
        /*
         * Window to create events tied to specific actions (AllyActionEvent/EnemyActionEvent)
         * Name, internal name, action id's, voicelines
         */

    }
    public void Dispose()
    {
    }
}