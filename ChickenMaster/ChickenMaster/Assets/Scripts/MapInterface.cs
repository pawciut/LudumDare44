using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapInterface : MonoBehaviour
{

    bool isPlacingTower;

    [SerializeField]
    Dictionary<TowerTypes, int> TowerTypePrice;

    [SerializeField]
    TowerPlacementInfo[] TowerConfiguration;

    [SerializeField]
    MapState MapState;

    [SerializeField]
    EggDisplay EggDisplay;
    [SerializeField]
    UITowerInfoUpdater UITowerInfo;

    TowerPlacement TowerPlacement;
    [SerializeField]
    TowerSlot[] Slots;

    TowerSlot selectedSlot;

    // Start is called before the first frame update
    void Start()
    {
        TowerPlacement = GetComponent<TowerPlacement>();
        EggDisplay.UpdateValue(MapState.EggsTotal);
        UpdateTowerTooltip();
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool CanBuild(int price)
    {
        return price <= MapState.EggsSpare;
    }

    public void PickTower(int towerType)
    {
        Debug.Log("Place tower clicked");
        var selectedTower = TowerConfiguration.FirstOrDefault(tc => tc.TowerType == (TowerTypes)towerType);
        if (selectedTower == null)
            Debug.LogError($"Tower type {towerType} not supported");

        TowerPlacement.Set(selectedTower);
        UpdateTowerTooltip();

    }

    public void CancelTowerSelection()
    {
        Debug.Log("Tower selection canceled");
        TowerPlacement.Clear();
        UpdateTowerTooltip();
    }

    public void PlaceTower(int slotId)
    {
        if (CanBuild(TowerPlacement.info.Price))
        {
            var slot = Slots.FirstOrDefault(s => s.Id == slotId);
            if (slot == null)
            {
                Debug.LogError($"Slot not found {slotId}");
                return;
            }

            if (slot.IsEmpty)
            {
                slot.PlaceTower(TowerPlacement.info);
                SubstractEggs(TowerPlacement.info.Price);
                TowerPlacement.VerifyPlacement(slot.IsEmpty);
            }
        }

    }

    void SubstractEggs(int amount)
    {
        MapState.EggsTotal -= amount;
        EggDisplay.UpdateValue(MapState.EggsTotal);
    }


    public void OnSlotEnter(int slotId)
    {
        if (Slots != null)
        {
            var slot = Slots.FirstOrDefault(s => s.Id == slotId);
            if (slot == null)
                Debug.LogError($"Slot not found {slotId}");

            selectedSlot = slot;
            TowerPlacement.VerifyPlacement(slot.IsEmpty && 
                (TowerPlacement.info != null
                    && CanBuild(TowerPlacement.info.Price)
                )
                );
        }
    }
    public void OnSlotExit()
    {
        TowerPlacement.VerifyPlacement(false);
        selectedSlot = null;
    }

    public void AddEgg(int amount)
    {
        MapState.EggsTotal += amount;
        EggDisplay.UpdateValue(MapState.EggsTotal);
        if (TowerPlacement.info != null && selectedSlot != null)
            TowerPlacement.VerifyPlacement(selectedSlot.IsEmpty && CanBuild(TowerPlacement.info.Price));
    }

    public void UpdateTowerTooltip()
    {
        if (TowerPlacement != null && TowerPlacement.info != null && TowerPlacement.info.TowerType != TowerTypes.Unknown)
            UITowerInfo.Show(TowerPlacement.info.Name, TowerPlacement.info.Desc, TowerPlacement.info.Price.ToString());
        else
            UITowerInfo.Hide();
    }
}
