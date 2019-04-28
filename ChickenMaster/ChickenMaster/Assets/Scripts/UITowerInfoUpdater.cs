using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerInfoUpdater : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TowerNameGUI;
    [SerializeField]
    TextMeshProUGUI TowerDescGUI;
    [SerializeField]
    TextMeshProUGUI TowerPriceGUI;
    [SerializeField]
    Image EggImage;
    

    private void Start()
    {
    }

    public void Show(string towerName, string towerDesc, string towerPrice)
    {
        gameObject.SetActive(true);

        if (TowerNameGUI != null)
            TowerNameGUI.text = towerName.ToString();
        if (TowerDescGUI != null)
            TowerDescGUI.text = towerDesc.ToString();
        if (TowerPriceGUI != null)
            TowerPriceGUI.text = towerPrice.ToString();
        if (EggImage != null)
            EggImage.enabled = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        if (TowerNameGUI != null)
            TowerNameGUI.text = string.Empty;
        if (TowerDescGUI != null)
            TowerDescGUI.text = string.Empty;
        if (TowerPriceGUI != null)
            TowerPriceGUI.text = string.Empty;
        

    }
}
