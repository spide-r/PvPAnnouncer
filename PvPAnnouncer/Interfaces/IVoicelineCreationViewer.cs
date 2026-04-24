using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IVoicelineCreationViewer
{
    void ShowCharacterData(Shoutcast shoutcast);
    void ShowSelectedAudio(Shoutcast shoutcast);
    void ShowSelectedText(Shoutcast shoutcast);
    void ShowMetadata(Shoutcast shoutcast);
    void ShowObject(Shoutcast shoutcast);
    public string GetText(Shoutcast shoutcast);
}