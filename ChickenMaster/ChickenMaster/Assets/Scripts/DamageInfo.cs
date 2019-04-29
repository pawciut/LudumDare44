using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class DamageInfo
{
    public DamageTypes DamageType;
    public float DamageValue;
    public GameObject HitPrefab;
}