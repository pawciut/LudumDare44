using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerState : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer Range;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRange()
    {
        if (Range != null)
            Range.enabled = true;
    }

    public void HideRange()
    {
        if (Range != null)
            Range.enabled = false;
    }
}
