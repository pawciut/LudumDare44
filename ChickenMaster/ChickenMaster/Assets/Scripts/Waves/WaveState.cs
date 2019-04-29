using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveState : MonoBehaviour
{
    [SerializeField]
    public int Index = -1;

    public int TotalEnemies;
    public int EnemiesKilled;
    /// <summary>
    /// Wave score
    /// </summary>
    public int Score;

    WaveInfo WaveInfo;
    public int SpawnIndex = -1;

    public UnityEvent WaveCleared;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave(WaveInfo waveInfo, int index)
    {
        this.WaveInfo = waveInfo;
        Index = index;
        SpawnIndex = -1;

        if (WaveInfo != null && WaveInfo.EnemySequence != null)
            this.TotalEnemies = WaveInfo.EnemySequence.Length;
        else
            this.TotalEnemies = 0;
    }

    public void EnemyKilled(int score)
    {
        ++EnemiesKilled;
        Score += score;

        if(EnemiesKilled >= TotalEnemies)
        {
            if (WaveCleared != null)
                WaveCleared.Invoke();
        }
    }
}
