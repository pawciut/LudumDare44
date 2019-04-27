using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    GameObject TowerPrefab;
    Transform CurrentTower;

    public void Set(TowerPlacementInfo towerPlacementInfo)
    {
        Debug.Log("Tower placement set");
        TowerPrefab = towerPlacementInfo.TowerPrefab;


        var mousePos = Input.mousePosition;
        //mousePos.z = 2.0; // we want 2m away from the camera position.
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        CurrentTower = Instantiate(TowerPrefab, objectPos, Quaternion.identity).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTower != null)
        {
            //Vector3 m = Input.mousePosition;
            //m = new Vector3(m.x, m.y, transform.position.y);
            //Vector3 p = Camera.main.ScreenToWorldPoint(m);
            //CurrentTower.position = new Vector3(p.x, p.y, 0);
            var mousePos = Input.mousePosition;
            //mousePos.z = 2.0; // we want 2m away from the camera position.
            var p = Camera.main.ScreenToWorldPoint(mousePos);
            CurrentTower.position = new Vector3(p.x, p.y, 0);
        }
    }
}
