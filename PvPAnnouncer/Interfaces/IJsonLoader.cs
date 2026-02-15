using System.Collections.Generic;
using System.Text.Json.Nodes;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IJsonLoader
{
    public void LoadAllValuesIntoMemory();
    public void LoadAndMapCustomEvents();
    public void LoadShoutcasts();
    public void LoadMapping();
    public JsonObject BuildJsonShout(Shoutcast s);
    public Dictionary<string, List<string>> LoadCutsceneLines();
    public Shoutcast ConstructShoutcast(string json);
    public List<string> ConstructMapping(string json);
}