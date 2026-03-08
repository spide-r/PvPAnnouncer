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
}