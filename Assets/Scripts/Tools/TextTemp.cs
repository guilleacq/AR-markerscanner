using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTemp : MonoBehaviour
{
    public static TextTemp Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetText(string text)
    {
        GetComponent<TMP_Text>().text = text; 
    }

}
