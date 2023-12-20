using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCorrector : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void CorrectText()
    {
        text.text = text.text.Replace("\\n", "\n");
    }
}
