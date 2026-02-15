using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;
using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.ImGuiNotification;
using Newtonsoft.Json;

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
    }
    private void ActionEventCreator()
    {
        /*
         * Window to create events tied to specific actions (AllyActionEvent/EnemyActionEvent)
         * Name, internal name, action id's, voicelines
         */

    }

    private string GetB64(object? obj)
    {
        var ser = Newtonsoft.Json.JsonSerializer.Create();
        var writer = new StringWriter();
        ser.Serialize(writer, obj);
        var str = writer.ToString();
        var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        
        return b64;
    }
    
    public void Dispose()
    {
    }
}