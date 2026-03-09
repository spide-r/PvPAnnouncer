using System;
using System.Numerics;
using Dalamud.Interface.Windowing;

namespace PvPAnnouncer.Windows;

public class DevWindow : Window, IDisposable
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


    public void Dispose()
    {
    }
}