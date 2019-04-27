using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EggDisplay : MonoBehaviour
{
    TextMeshProUGUI TextGUI;

    private void Start()
    {
        TextGUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateValue(int value)
    {
        if (TextGUI != null)
            TextGUI.text = value.ToString();
    }
}
