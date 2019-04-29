using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EggPoint : MonoBehaviour
{

    public UnityEvent OnDestroyTower;
    public UnityEvent OnEggStolen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Constants.EnemyTag)
        {
            var enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            if (enemyScript.Info.DestroysRandomTower)
            {
                Debug.Log($"EggPoint.OnDestroy {collision.gameObject.GetInstanceID()}");
                if (OnDestroyTower != null)
                    OnDestroyTower.Invoke();
            }
            else
            {
                if (OnEggStolen != null)
                    OnEggStolen.Invoke();
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
