using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    bool hasStarted;
    public float CurrentValue = 0;

    public UnityEvent Tick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            CurrentValue += Time.deltaTime;
            if (Tick != null)
                Tick.Invoke();
        }
    }

    public void StartTimer()
    {
        hasStarted = true;
    }
}
