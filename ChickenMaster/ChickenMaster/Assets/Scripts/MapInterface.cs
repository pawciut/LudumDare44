using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TowerPlacementInfo
{
    public TowerTypes TowerType;
    public int Price;
    public GameObject TowerPrefab;
}

public class MapInterface : MonoBehaviour
{

    bool isPlacingTower;
    
    [SerializeField]
    Dictionary<TowerTypes, int> TowerTypePrice;

    [SerializeField]
    TowerPlacementInfo[] TowerConfiguration;

    [SerializeField]
    MapState MapState;

    TowerPlacement TowerPlacement;

    // Start is called before the first frame update
    void Start()
    {
        TowerPlacement = GetComponent<TowerPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTower(int towerType)
    {
        Debug.Log("Place tower clicked");
        var selectedTower = TowerConfiguration.FirstOrDefault(tc => tc.TowerType == (TowerTypes)towerType);
        if (selectedTower == null)
            Debug.LogError($"Tower type {towerType} not supported");
        else if (selectedTower.Price > MapState.EggsSpare)
        {
            Debug.Log("Not enough eggs");
        }
        else
        {

            TowerPlacement.Set(selectedTower);
        }


    }
}
