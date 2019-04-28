using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapInterface : MonoBehaviour
{

    bool isPlacingTower;

    [Header("General Configuration")]

    [SerializeField]
    Dictionary<TowerTypes, int> TowerTypePrice;
    [SerializeField]
    TowerPlacementInfo[] TowerConfiguration;
    [SerializeField]
    EnemyConfig[] EnemyConfiguration;

    [Space(20)]
    [Header("LevelConfiguration")]

    [SerializeField]
    WaveInfo[] WaveConfiguration;
    //TODO:currentWaveState?
    [SerializeField]
    Transform[] FoxSpawnPoints;
    [SerializeField]
    TowerSlot[] Slots;

    [Space(20)]
    [Header("GameLogic")]

    [SerializeField]
    MapState MapState;

    WaveState CurrentWave;
    TowerPlacement TowerPlacement;
    TowerSlot selectedSlot;

    [Space(20)]
    [Header("UI")]

    [SerializeField]
    EggDisplay EggDisplay;
    [SerializeField]
    UITowerInfoUpdater UITowerInfo;
    [SerializeField]
    UIWaveAnnouncerUpdater UIWaveAnnouncer;
    [SerializeField]
    UITopMenuUpdater UITopMenu;


    Timer TotalTime;


    // Start is called before the first frame update
    void Start()
    {
        TowerPlacement = GetComponent<TowerPlacement>();
        CurrentWave = GetComponent<WaveState>();
        TotalTime = GetComponent<Timer>();
        TotalTime.StartTimer();


        EggDisplay.UpdateValue(MapState.EggsTotal);
        UpdateTowerTooltip();

        if (WaveConfiguration == null
            || WaveConfiguration.Length <= 0)
        {
            Debug.LogError("No wave to start");
            return;
        }
        CurrentWave.StartWave(WaveConfiguration[0], 0);
        UpdateWaveAnnouncer();

        UITopMenu.HideAll();
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


    public void UpdateWaveAnnouncer()
    {
        if (CurrentWave != null
            && WaveConfiguration != null
            && WaveConfiguration.Length > CurrentWave.Index)
        {
            var wave = WaveConfiguration[CurrentWave.Index];
            UIWaveAnnouncer.Show(wave.WaveTitle, wave.WaveDescription);
        }
        else
            UIWaveAnnouncer.Hide();
    }

    public void UpdateTotalTime()
    {
        int hours = ((int)TotalTime.CurrentValue / 3600) % 24;
        int minutes = ((int)TotalTime.CurrentValue / 60) % 60;
        int seconds = (int)TotalTime.CurrentValue % 60;
        UITopMenu.ShowTime(String.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds));

    }

    public void NextWave()
    {
        if (CurrentWave.Index >= WaveConfiguration.Length - 1)
        {
            //Koniec gry
        }
        else;
        {
            int nextWaveIndex = CurrentWave.Index + 1;
            CurrentWave.StartWave(WaveConfiguration[nextWaveIndex], nextWaveIndex);
            UpdateWaveAnnouncer();
        }
    }


}
