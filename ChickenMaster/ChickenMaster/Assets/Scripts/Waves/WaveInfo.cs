using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class WaveInfo
{
    public EnemySpawnerInfo[] EnemySequence;
    /// <summary>
    /// in seconds
    /// </summary>
    public float TimeBetweenSpawns = 3;
    public float TimeBeforeWaveStarts = 10;
    public string WaveTitle;
    public string WaveDescription;
}