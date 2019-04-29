using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    public EnemyInfo Info;

    [SerializeField]
    Transform EggAttachment;

    public float CurrentHp;
    public bool IsDead;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHp = Info.MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(DamageInfo damage)
    {
        var calculatedDamage = CalculateRealDamage(damage);

        if (CurrentHp - calculatedDamage <= 0)
        {
            //Ginie
            Debug.Log("EnemyDied");

            var followPath = GetComponent<FollowPath>();
            followPath.StopMovement();
            IsDead = true;
            onEnemyKilled(this);
            onEnemyKilled = null;
            GameObject.Destroy(gameObject);
        }
        else
        {
            CurrentHp -= calculatedDamage;
        }
        Debug.Log($"Enemy {GetInstanceID()} got damaged and has {CurrentHp} left");
    }

    float CalculateRealDamage(DamageInfo damage)
    {
        float result = damage.DamageValue;

        if (Info.IsHeroic)
            result = result / 2f;

        return result;
    }

    Action<EnemyScript> onEnemyKilled;
    public void Attach(Action<EnemyScript> onEnemyKilled)
    {
        this.onEnemyKilled = onEnemyKilled;
    }
}
