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
    public Transform[] SpawnPoints;
    /// <summary>
    /// if false spawns at first spawner
    /// </summary>
    public bool SpawnAtRandom;

    public GameObject EnemyPrefab;
}