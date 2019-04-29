using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class EnemyInfo
{
    public EnemyTypes EnemyType;
    public bool IsHeroic;
    public bool DestroysRandomTower;
    public int Score;
    public float MaxHp;
}