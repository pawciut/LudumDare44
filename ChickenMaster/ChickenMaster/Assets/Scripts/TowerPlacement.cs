using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    GameObject TowerPreviewPrefab;
    Transform CurrentTowerPreview;

    [SerializeField]
    Color AllowedColor;
    [SerializeField]
    Color DeniedColor;

    public TowerPlacementInfo info;

    public void Set(TowerPlacementInfo towerPlacementInfo)
    {
        Debug.Log("Tower placement set");

        if(towerPlacementInfo == null)
        {
            if (CurrentTowerPreview != null)
                Destroy(CurrentTowerPreview.gameObject);
            info = null;
            return;
        }

        TowerPreviewPrefab = towerPlacementInfo.TowerPrefab;
        info = towerPlacementInfo;

        if (CurrentTowerPreview != null)
            Destroy(CurrentTowerPreview.gameObject);

        var mousePos = Input.mousePosition;
        //mousePos.z = 2.0; // we want 2m away from the camera position.
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        //korekta zeby bylo na srodku myszki
        var towerPreview = TowerPreviewPrefab.GetComponent<TowerPlacementPreview>();
        if (towerPreview != null && towerPreview.BorderRenderer != null && towerPreview.BorderRenderer.sprite != null)
            objectPos = new Vector3(objectPos.x - towerPreview.BorderRenderer.bounds.size.x / 2, objectPos.y - towerPreview.BorderRenderer.bounds.size.y / 2, objectPos.z);
        
            
            CurrentTowerPreview = Instantiate(TowerPreviewPrefab, objectPos, Quaternion.identity).transform;
        SetBorderColor(DeniedColor);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTowerPreview != null)
        {
            //Vector3 m = Input.mousePosition;
            //m = new Vector3(m.x, m.y, transform.position.y);
            //Vector3 p = Camera.main.ScreenToWorldPoint(m);
            //CurrentTower.position = new Vector3(p.x, p.y, 0);
            var mousePos = Input.mousePosition;
            //mousePos.z = 2.0; // we want 2m away from the camera position.
            var p = Camera.main.ScreenToWorldPoint(mousePos);

            //korekta zeby bylo na srodku myszki
            var towerPreview = CurrentTowerPreview.gameObject.GetComponent<TowerPlacementPreview>();
            if (towerPreview != null && towerPreview.BorderRenderer != null && towerPreview.BorderRenderer.sprite != null)
                p = new Vector3(p.x - towerPreview.BorderRenderer.bounds.size.x / 2, p.y - towerPreview.BorderRenderer.bounds.size.y / 2, p.z);

            CurrentTowerPreview.position = new Vector3(p.x, p.y, 0);
        }
    }

    internal void Clear()
    {
        Set(null);
    }

    public void VerifyPlacement(bool  isAllowed)
    {
        //Debug.Log($"VerifyPlacement {isAllowed}");
        if (CurrentTowerPreview != null)
        {
            SetBorderColor(isAllowed ? AllowedColor : DeniedColor);
        }
    }

    void SetBorderColor(Color color)
    {
        var preview = CurrentTowerPreview.gameObject.GetComponent<TowerPlacementPreview>();
        if (preview != null)
            preview.BorderRenderer.color = color;
    }

}
