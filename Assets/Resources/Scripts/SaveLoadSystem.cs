using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadSystem
{
    // public static void SaveOptions(SaveSettings settings)
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Application.persistentDataPath + "/options.game";
    //     FileStream stream = new FileStream(path, FileMode.Create);

    //     SaveSettings data = settings;

    //     formatter.Serialize(stream, data);
    //     stream.Close();
    // }

    // public static SaveSettings LoadOptions()
    // {
    //     string path = Application.persistentDataPath + "/options.game";
    //     if (File.Exists(path))
    //     {
    //         BinaryFormatter formatter = new BinaryFormatter();
    //         FileStream stream = new FileStream(path, FileMode.Open);

    //         SaveSettings data = formatter.Deserialize(stream) as SaveSettings;
    //         stream.Close();

    //         return data;
    //     }
    //     else
    //     { 
    //         return new SaveSettings();
    //     }
    // }

    public static void SaveState(SaveGamestate state, int index)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/state" + index.ToString() + ".save";
        FileStream stream = new FileStream(path, FileMode.Create);

        // SaveGamestate data = new SaveGamestate(state);

        formatter.Serialize(stream, state);
        stream.Close();
    }

    public static SaveGamestate LoadState(int index)
    {
        string path = Application.persistentDataPath + "/state" + index.ToString() + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveGamestate data = formatter.Deserialize(stream) as SaveGamestate;
            stream.Close();

            return data;
        }
        else
        { 
            return new SaveGamestate();
        }
    }

    public static void DeleteState(int index)
    {
        string path = Application.persistentDataPath + "/state" + index.ToString() + ".save";
        File.Delete(path);

        string filename = Application.dataPath;
        if (Application.isEditor)
        {
            // put screenshots in folder above asset path so unity doesn't index the files
            string stringPath = filename + "/..";
            filename = Path.GetFullPath(stringPath);
        }
        filename += "/screens/saveSlot" + index + ".png";
        if (File.Exists(filename)) File.Delete(filename);
    }
}
