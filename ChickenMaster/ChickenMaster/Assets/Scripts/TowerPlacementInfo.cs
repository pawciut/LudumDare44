using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class TowerPlacementInfo
{
    public TowerTypes TowerType;
    public int Price;
    public GameObject TowerPrefab;
    public string Name;
    public string Desc;
}