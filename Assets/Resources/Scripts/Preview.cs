//using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolBox.Serialization;

public class Preview : MonoBehaviour
{
    public void UpdateImage(int index)
    {
        StartCoroutine(CreateImage(index));
    }

    public IEnumerator CreateImage(int index)
    {
        Camera.main.GetComponent<TakeScreenshot>().TakeScreen(index);
        yield return new WaitUntil(() => Camera.main.GetComponent<TakeScreenshot>().ready);
        Camera.main.GetComponent<TakeScreenshot>().ready = false;
        //SetImage(PlayerPrefs.GetString("saveSlot" + index.ToString()));
        SetImage(PlayerPrefs.GetString("saveSlot" + index.ToString()));
    }

    public void SetImage(string file, int index = -1)
    {
        try
        {
            if (file == "Default")
            {
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Main Menu/saveSlot" + index.ToString());
            }
            else
            {
                var rawData = System.IO.File.ReadAllBytes(file);
                Texture2D tex = new Texture2D(2, 2); // Create an empty Texture; size doesn't matter (she said)
                tex.LoadImage(rawData);
                Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                GetComponent<Image>().sprite = mySprite;
            }
        }
        catch (System.IO.DirectoryNotFoundException e)
        {
            SetImage("Default");
        }
    }

    // private byte[] GetBytesFromFilePath(string pathToOutputLog) {
    //     using (var fileStream = File.Open(pathToOutputLog, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
    //         using (var memoryStream = new MemoryStream()) {
    //             fileStream.CopyTo(memoryStream);
    //             return memoryStream.ToArray();
    //         }
    //     }
    // }

    void Start()
    {

    }
}
