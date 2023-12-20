using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSettings
{
    public Dictionary<string, string> gamePageSettings;
    public Dictionary<string, string> audioPageSettings;
    public Dictionary<string, string> otherPageSettings;
    public string[] saveScreens;

    public SaveSettings(Dictionary<string, string> sgamePageSettings = null,
                        Dictionary<string, string> saudioPageSettings = null,
                        Dictionary<string, string> sotherPageSettings = null,
                        string[] ssaveScreens = null)
    {
        if (sgamePageSettings != null)
        {
            gamePageSettings = sgamePageSettings;
            audioPageSettings = saudioPageSettings;
            otherPageSettings = sotherPageSettings;
            saveScreens = ssaveScreens;
        }
        else
        {
            gamePageSettings = new Dictionary<string, string>()
            {
                {"languageCode", "en"}
            };

            audioPageSettings = new Dictionary<string, string>()
            {
                {"masterVolume", "1"},
                {"musicVolume", "1"},
                {"sfxVolume", "1"}
            };

            otherPageSettings = new Dictionary<string, string>() {};

            saveScreens = new string[] {"", "", ""};
        }
    }
}
