using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class ConfigManager(Configuration pluginConfiguration, IJsonLoader jsonLoader)
{
    public void ReloadConfig()
    {
        jsonLoader.LoadAllValuesIntoMemory();
        ApplyCustomValues();
    }

    public void ApplyCustomValues()
    {
        PluginServices.PluginLog.Verbose("Applying custom values!");
        foreach (var keyValuePair in pluginConfiguration.CustomShoutcasts)
        {
            var sc = keyValuePair.Value;
            var scObj = PluginServices.JsonLoader.ConstructShoutcast(sc);
            PluginServices.ShoutcastRepository.SetShoutcast(keyValuePair.Key, scObj);
        }

        foreach (var keyValuePair in pluginConfiguration.MappingOverride)
        {
            var jsonMapping = keyValuePair.Value;
            PluginServices.EventShoutcastMapping.ReplaceMapping(keyValuePair.Key,
                PluginServices.JsonLoader.ConstructMappingFromJson(jsonMapping));
        }

        foreach (var keyValuePair in pluginConfiguration.CustomEvents)
        {
            //unused - thank goodness
        }
    }

    public void DeleteAndDeregisterShoutcast(string shoutcastId)
    {
        PluginServices.Config.DeleteCustomShoutCast(shoutcastId);
        PluginServices.ShoutcastRepository.DeleteShoutcast(shoutcastId);
        PluginServices.EventShoutcastMapping.PurgeMapping(shoutcastId);
        PurgeShoutFromCustomMapping(shoutcastId);
        PluginServices.Config.Save();
    }

    private void PurgeShoutFromCustomMapping(string shout)
    {
        foreach (var (eventName, value) in PluginServices.Config.MappingOverride)
        {
            var currentList = PluginServices.JsonLoader.ConstructMappingFromJson(value);
            currentList.Remove(shout);
            var currentJson = PluginServices.JsonLoader.BuildJsonMapping(eventName, currentList);
            PluginServices.Config.MappingOverride[eventName] = currentJson.ToJsonString();
        }
    }


    //Notes for when we're letting people customize things
    //store overriden voiceline definitions (done)
    //store overriden event=>Voiceline[] mapping (done)
    //store overriden event definitions (done)
    //preserve readonly defaults, read from them on first load (done)
    //maybe read from resources to get the defaults(press button to reset everything)
    //encourage sharing - export the new stuff maybe
    //find ways to incentivise sharing modification w/ dev
    //maybe only store *modified* shit in this config (done)
    //make sure config overrides are applied correctly (done)
}