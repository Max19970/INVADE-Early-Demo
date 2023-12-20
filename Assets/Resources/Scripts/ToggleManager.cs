using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener((bool _) => {GetComponent<Animator>().SetBool("Active", _); if (!_) EventSystem.current.SetSelectedGameObject(null);});
    }
}
