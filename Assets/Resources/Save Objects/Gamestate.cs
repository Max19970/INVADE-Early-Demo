using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gamestate
{
    public static string difficulty;
    public static Dictionary<string, Dictionary<string, string>> dataGlobal;
    public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> dataLocations;
    public static Dictionary<string, Dictionary<string, List<string>>> dataDeletedPickups;
    public static Dictionary<string, Dictionary<string, List<string>>> dataUsedInteractors;
    public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> dataMapTriggers;
    public static Dictionary<string, Dictionary<string, List<string>>> dataDeletedProps;
    public static string currentScene;
    public static string currentLocation;
}
