using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPage : MonoBehaviour
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>(){};
    }

    public void LoadData(Dictionary<string, string> data) {}
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
