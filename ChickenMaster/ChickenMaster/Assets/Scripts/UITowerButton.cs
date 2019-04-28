using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITowerButton : MonoBehaviour
{
    public bool IsSelected;

    public UnityEvent SelectionCanceled;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed secondary button.");
            IsSelected = false;
            if (SelectionCanceled != null)
                SelectionCanceled.Invoke();
        }
    }

}
