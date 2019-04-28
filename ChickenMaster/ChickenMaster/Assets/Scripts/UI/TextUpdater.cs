using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    TextMeshProUGUI TextGUI;

    private void Start()
    {
        TextGUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateValue(string text)
    {
        if (TextGUI != null)
            TextGUI.text = text.ToString();
    }
}
