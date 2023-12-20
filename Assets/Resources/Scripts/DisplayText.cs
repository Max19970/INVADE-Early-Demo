using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public StatusText stext;
    private Vector3 offset;

    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Text>().text = stext.text;
    }

    void Update()
    {
        //Vector3 pos = stext.cam.WorldToScreenPoint(stext.lookAt + stext.offset);
        Vector3 pos = stext.lookAt + stext.offset;

        transform.position = pos;
    }
}
