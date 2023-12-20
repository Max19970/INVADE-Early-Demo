using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status Text", menuName = "ScriptableObjects/StatusText")]
public class StatusText : ScriptableObject
{
    public string text;
    public Vector3 lookAt;
    public Vector3 offset;
    public Camera cam;
}
