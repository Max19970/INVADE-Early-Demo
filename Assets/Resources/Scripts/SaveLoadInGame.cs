using System;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadInGame
{
    public static SaveGamestate Save()
    {
        // Dictionary<string, Dictionary<string, Dictionary<string, string>>> sceneData = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>() {};

        // foreach (Saveable obj in FindAllObjectsOfTypeExpensive<Saveable>())
        // {
        //     Dictionary<string, Dictionary<string, Dictionary<string, string>>> objData = obj.SaveObject();
        //     sceneData[objData.Keys.First()] = objData[objData.Keys.First()];
        // }

        // SaveGamestate state = SaveLoadSystem.LoadState(SaveFileCurrent.index);
        // state.data[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name] = sceneData;
        // SaveLoadSystem.SaveState(state, SaveFileCurrent.index);
        Gamestate.dataGlobal["player"] = GameObject.Find("Player").GetComponent<PlayerController>().Save();
        foreach (Location loc in GameObject.Find("Maps").GetComponentsInChildren(typeof(Location)))
        {
            Gamestate.dataLocations[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][loc.gameObject.name] = loc.Save();
            foreach (MapTrigger trigger in loc.gameObject.GetComponentsInChildren(typeof(MapTrigger)))
            {
                Gamestate.dataMapTriggers[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][loc.gameObject.name][trigger.gameObject.transform.parent.gameObject.name] = trigger.SaveMapTrigger();
            }
            Gamestate.dataUsedInteractors[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][loc.gameObject.name] = new List<string>() {};
            foreach (InteractorInfo interactor in loc.gameObject.GetComponentsInChildren(typeof(InteractorInfo)))
            {
                if (interactor.used) {Gamestate.dataUsedInteractors[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name][loc.gameObject.name].Add(interactor.gameObject.name);
                Debug.Log(interactor.gameObject.name);}
            }
        }
        Gamestate.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Gamestate.currentLocation = GameObject.Find("GameManager").GetComponent<MapManager>().currentLocation;

        // foreach (KeyValuePair<string, string> entry in Gamestate.dataGlobal["player"])
        // {
        //     Debug.Log(entry.Value);
        // }
        // foreach (string prop in Gamestate.dataDeletedProps["Game"]["Start"])
        // {
        //     Debug.Log(prop);
        // }

        SaveGamestate state = new SaveGamestate(Gamestate.difficulty, Gamestate.dataGlobal, Gamestate.dataLocations, Gamestate.dataDeletedPickups, Gamestate.dataUsedInteractors, Gamestate.dataMapTriggers, Gamestate.dataDeletedProps, Gamestate.currentScene, Gamestate.currentLocation);
        Debug.Log(state.dataGlobal["player"]["position"]);
        return state;
    }

    public static void Load()
    {
        // SaveGamestate state = SaveLoadSystem.LoadState(SaveFileCurrent.index);
        // foreach(KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> entry in state.data[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name])
        // {
        //     GameObject.Find(entry.Key).GetComponent<Saveable>().LoadObject(entry.Value);
        // }

        // if (Gamestate.dataGlobal["globalAudio"]["clip"] != "") GameObject.Find("Global Music").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(Gamestate.dataGlobal["globalAudio"]["clip"]);
        // if (Gamestate.dataGlobal["locationAudio"]["clip"] != "") GameObject.Find("Location Sound").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(Gamestate.dataGlobal["locationAudio"]["clip"]);

        GameObject.Find("Player").GetComponent<PlayerController>().Load();
        List<string> position = new List<string>(Gamestate.dataGlobal["player"]["position"].Split(new string[] {", "}, StringSplitOptions.None));
        Vector3 playerPosition = new Vector3(float.Parse(position[0], CultureInfo.InvariantCulture.NumberFormat),
                                             float.Parse(position[1], CultureInfo.InvariantCulture.NumberFormat),
                                             float.Parse(position[2], CultureInfo.InvariantCulture.NumberFormat));
        float rotation = float.Parse(Gamestate.dataGlobal["player"]["rotation"], CultureInfo.InvariantCulture.NumberFormat);
        foreach (Location loc in GameObject.Find("Maps").GetComponentsInChildren(typeof(Location)))
        {
            loc.Load();
            loc.DeletePickups();
            loc.RestoreInteractors();
            loc.DeleteProps();
        }
        GameObject.Find("GameManager").GetComponent<MapManager>().ChangeLocation(Gamestate.currentLocation, playerPosition, rotation, "None", 1f);
    }

    // public static IEnumerable<GameObject> GetAllRootGameObjects()
    // {
    //     for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
    //     {
    //         GameObject[] rootObjs = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).GetRootGameObjects();
    //         foreach (GameObject obj in rootObjs)
    //             yield return obj;
    //     }
    // }
 
    // public static IEnumerable<T> FindAllObjectsOfTypeExpensive<T>()
    //     where T : MonoBehaviour
    // {
    //     foreach (GameObject obj in GetAllRootGameObjects())
    //     {
    //         foreach (T child in obj.GetComponentsInChildren<T>(true))
    //             yield return child;
    //     }
    // }
}
