using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGamestate
{
    public string difficulty;
    public Dictionary<string, Dictionary<string, string>> dataGlobal;
    public Dictionary<string, Dictionary<string, Dictionary<string, string>>> dataLocations;
    public Dictionary<string, Dictionary<string, List<string>>> dataDeletedPickups;
    public Dictionary<string, Dictionary<string, List<string>>> dataUsedInteractors;
    public Dictionary<string, Dictionary<string, Dictionary<string, string>>> dataMapTriggers;
    public Dictionary<string, Dictionary<string, List<string>>> dataDeletedProps;
    public string currentScene;
    public string currentLocation;

    public SaveGamestate(string sdifficulty = null,
                         Dictionary<string, Dictionary<string, string>> sdataGlobal = null,
                         Dictionary<string, Dictionary<string, Dictionary<string, string>>> sdataLocations = null,
                         Dictionary<string, Dictionary<string, List<string>>> sdataDeletedPickups = null,
                         Dictionary<string, Dictionary<string, List<string>>> sdataUsedInteractors = null,
                         Dictionary<string, Dictionary<string, Dictionary<string, string>>> sdataMapTriggers = null,
                         Dictionary<string, Dictionary<string, List<string>>> sdataDeletedProps = null,
                         string scurrentScene = null,
                         string scurrentLocation = null)
    {
        if (sdataGlobal != null)
        {
            difficulty = sdifficulty;
            dataGlobal = sdataGlobal;
            dataLocations = sdataLocations;
            dataDeletedPickups = sdataDeletedPickups;
            dataUsedInteractors = sdataUsedInteractors;
            dataMapTriggers = sdataMapTriggers;
            dataDeletedProps = sdataDeletedProps;
            currentScene = scurrentScene;
            currentLocation = scurrentLocation;
        }
        else
        {
            difficulty = "None";
            dataGlobal = new Dictionary<string, Dictionary<string, string>>()
            {
                {"player", new Dictionary<string, string>() {
                    {"position", "-0.01, 12.2, 0"},
                    {"rotation", "1"},
                    {"currentHealth", "100"},
                    {"inventory", "Nothing, Nothing, Nothing, Nothing, 1"},
                    {"abilities", "Unknown, Unknown, Unknown, Unknown, Unknown"}
                }}
            };
            dataLocations = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>()
            {
                {"Game", new Dictionary<string, Dictionary<string, string>>() {
                    {"Start", new Dictionary<string, string>() {
                        {"effectName", "None"},
                        {"isEffectEnabled", "0"},
                        {"mySound", "Audio/alarmSound"},
                        {"mySoundSecond", ""},
                        {"myMusic", ""},
                        {"myMusicSecond", ""},
                        {"soundAtEnter", "0"},
                        {"isEntering", "0"}
                    }},
                    {"Laborotory1", new Dictionary<string, string>() {
                        {"effectName", "Alarm Effect"},
                        {"isEffectEnabled", "0"},
                        {"mySound", "Audio/alarmSound"},
                        {"mySoundSecond", ""},
                        {"myMusic", "Audio/startMusic"},
                        {"myMusicSecond", "Audio/labSection"},
                        {"soundAtEnter", "1"},
                        {"isEntering", "0"}
                    }},
                    {"Corridor1", new Dictionary<string, string>() {
                        {"effectName", "None"},
                        {"isEffectEnabled", "0"},
                        {"mySound", ""},
                        {"mySoundSecond", ""},
                        {"myMusic", "Audio/labSection"},
                        {"myMusicSecond", ""},
                        {"soundAtEnter", "1"},
                        {"isEntering", "1"}
                    }},
                    {"Rofl", new Dictionary<string, string>() {
                        {"effectName", "None"},
                        {"isEffectEnabled", "0"},
                        {"mySound", ""},
                        {"mySoundSecond", ""},
                        {"myMusic", "Audio/labSection"},
                        {"myMusicSecond", ""},
                        {"soundAtEnter", "1"},
                        {"isEntering", "0"}
                    }},
                }}
            };
            dataDeletedPickups = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                {"Game", new  Dictionary<string, List<string>>() {
                    {"Start", new List<string>()},
                    {"Laborotory1", new List<string>()},
                    {"Corridor1", new List<string>()},
                    {"Rofl", new List<string>()}
                }}
            };
            dataMapTriggers = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>()
            {
                {"Game", new Dictionary<string, Dictionary<string, string>>() {
                    {"Start", new Dictionary<string, string>() {}},
                    {"Laborotory1", new Dictionary<string, string>() {
                        {"Simple Door", "0, 1"}
                    }},
                    {"Corridor1", new Dictionary<string, string>() {
                        {"Simple Door1", "0, 0"},
                        {"Simple Door2", "0, 0"}
                    }},
                    {"Rofl", new Dictionary<string, string>() {
                        {"Simple Door3", "0, 0"}
                    }}
                }}
            };
            dataUsedInteractors = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                {"Game", new  Dictionary<string, List<string>>() {
                    {"Start", new List<string>()},
                    {"Laborotory1", new List<string>()},
                    {"Corridor1", new List<string>()},
                    {"Rofl", new List<string>()}
                }}
            };
            dataDeletedProps = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                {"Game", new  Dictionary<string, List<string>>() {
                    {"Start", new List<string>()},
                    {"Laborotory1", new List<string>()},
                    {"Corridor1", new List<string>()},
                    {"Rofl", new List<string>()}
                }}
            };
            currentScene = "Game";
            currentLocation = "Start";
        }
    }
}
