using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility.Raii;

namespace PvPAnnouncer.Data;

public abstract class ImguiTools
{
    // Taken from https://github.com/Infiziert90/ChatTwo/blob/main/ChatTwo/Util/ImGuiUtil.cs 

    public static bool CtrlShiftButton(string label, string tooltip = "Press Ctrl+Shift to enable this button")
    {
        var ctrlShiftHeld = ImGui.GetIO() is {KeyCtrl: true, KeyShift: true};

        bool ret;
        using (ImRaii.Disabled(!ctrlShiftHeld))
        {
            ret = ImGui.Button(label) && ctrlShiftHeld;
        }

        if (!string.IsNullOrEmpty(tooltip) && ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
            Tooltip(tooltip);

        return ret;
    }

    public static void Tooltip(string tooltip)
    {
        using (ImRaii.Tooltip())
        using (ImRaii.TextWrapPos(ImGui.GetFontSize() * 35.0f))
        {
            ImGui.TextUnformatted(tooltip);
        }
    }
}