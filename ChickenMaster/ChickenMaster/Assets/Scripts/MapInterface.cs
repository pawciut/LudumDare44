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
    Spawner[] FoxSpawnPoints;
    [SerializeField]
    TowerSlot[] Slots;

    [Space(20)]
    [Header("GameLogic")]

    [SerializeField]
    MapState MapState;

    [SerializeField]
    Transform EnemyGroupingObject;

    WaveState CurrentWave;
    TowerPlacement TowerPlacement;
    TowerSlot selectedSlot;
    int Score;

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
    System.Random random;


    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random(System.DateTime.Now.Millisecond);

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

        UITopMenu.HideAll();

        UITopMenu.ShowScore(Score);
        NextWave();
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
        UITopMenu.ShowTime(FormatTime(TotalTime.CurrentValue));
    }

    string FormatTime(float time)
    {
        int hours = ((int)time / 3600) % 24;
        int minutes = ((int)time / 60) % 60;
        int seconds = (int)time % 60;
        return String.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }

    public void NextWave()
    {
        if (CurrentWave.Index >= WaveConfiguration.Length - 1)
        {
            //Koniec gry
        }
        else
        {
            int nextWaveIndex = CurrentWave.Index + 1;
            CurrentWave.StartWave(WaveConfiguration[nextWaveIndex], nextWaveIndex);
            UpdateWaveAnnouncer();

            UITopMenu.ShowRound(nextWaveIndex + 1);

            float timeBeforeWaveStarts = WaveConfiguration[nextWaveIndex].TimeBeforeWaveStarts;
            if (timeBeforeWaveStarts > 0)
                StartCoroutine(StartBeforeWaveTimer("Round starts in:", timeBeforeWaveStarts));
            StartCoroutine(StartWaveTimer("Round time:", nextWaveIndex, timeBeforeWaveStarts));
            StartCoroutine(StartSpawning(timeBeforeWaveStarts));
        }
    }

    IEnumerator StartBeforeWaveTimer(string beforeTitle, float timeBeforeWaveStarts)
    {
        float countdown = timeBeforeWaveStarts;

        while (countdown > 0)
        {
            UITopMenu.ShowWaveTime(beforeTitle, String.Format("{0}s", (int)countdown));
            yield return new WaitForSeconds(1);
            --countdown;
        }
    }

    IEnumerator StartWaveTimer(string timerTitle, int roundIndex, float timeBeforeWaveStarts)
    {
        yield return new WaitForSeconds(timeBeforeWaveStarts);

        float time = 0;

        while (CurrentWave.Index == roundIndex)
        {
            UITopMenu.ShowWaveTime(timerTitle, FormatTime(time));
            yield return new WaitForSeconds(1);
            ++time;
        }
    }


    IEnumerator StartSpawning(float timeBeforeWaveStarts)
    {
        yield return new WaitForSeconds(timeBeforeWaveStarts);

        var waveInfo = WaveConfiguration[CurrentWave.Index];

        for (int i = 0; i < waveInfo.EnemySequence.Length; ++i)
        {
            var nextSpawnIndex = i;
            CurrentWave.SpawnIndex = nextSpawnIndex;

            var enemyInfo = waveInfo.EnemySequence[i];
            var enemyPrefab = EnemyConfiguration.FirstOrDefault(e => e.EnemyType == enemyInfo.EnemyType).EnemyPrefab;
            Spawner spawner = GetSpawner(enemyInfo);

            SpawnEnemy(enemyPrefab, spawner);
            yield return new WaitForSeconds(waveInfo.TimeBetweenSpawns);
        }
    }

    void SpawnEnemy(GameObject enemyPrefab, Spawner spawner)
    {
        var enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, EnemyGroupingObject);
        var enemyFollowPath = enemy.GetComponent<FollowPath>();
        enemyFollowPath.MyPath = spawner.SpawnerPath;
        enemyFollowPath.StartMovement();
        //TODO:odwracanie wroga powinno byc chhyba w follow path tak samo jak obsluga obrotu zeby to bralo z PathPoint.Tranform
    }

    Spawner GetSpawner(EnemySpawnerInfo enemySpawnerInfo)
    {
        Spawner spawnPoint;

        Spawner[] spawnPoints;

        switch (enemySpawnerInfo.EnemyType)
        {
            case EnemyTypes.Fox:
                spawnPoints = FoxSpawnPoints;
                break;
            default:
                spawnPoints = null;
                break;
        }

        if (enemySpawnerInfo.SpawnAtRandom)
        {
            int spawnPointIndex = random.Next(0, spawnPoints.Length);
            Debug.Log($"Random spawn index {spawnPointIndex} total {spawnPoints.Length}");
            spawnPoint = spawnPoints[spawnPointIndex];
        }
        else
            spawnPoint = spawnPoints[0];
        return spawnPoint;
    }


}
