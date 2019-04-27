using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct MapState
{
    [SerializeField]
    public int EggsTotal;

    public int EggsSpare { get { return EggsTotal - 1; } }
}

