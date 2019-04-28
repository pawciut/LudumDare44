using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class EnemySpawnerInfo
{
    public EnemyTypes EnemyType;
    /// <summary>
    /// if not random, then spawn by index in MapLogic config
    /// </summary>
    public int SpawnPointIndex;
    /// <summary>
    /// if false spawns at first spawner
    /// </summary>
    public bool SpawnAtRandom;
}