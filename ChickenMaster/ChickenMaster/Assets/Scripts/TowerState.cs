using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerState : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer Range;
    [SerializeField]
    Collider2D[] Colliders;
    [SerializeField]
    int MaxTargets;
    [SerializeField]
    float AttackSpeed;
    [SerializeField]
    DamageInfo Damage;

    [SerializeField]
    AttackTypes AttackType;

    [SerializeField]
    Transform ProjectileSource;
    [SerializeField]
    Transform MeleeHitSource;

    [SerializeField]
    GameObject ProjectilePrefab;



    List<EnemyScript> TargetedEnemies;

    const int ColliderResultSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        TargetedEnemies = new List<EnemyScript>();
    }

    bool CanTarget()
    {
        return TargetedEnemies.Count < MaxTargets;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanTarget())
            return;

        Collider2D[] results = new Collider2D[ColliderResultSize];
        foreach(Collider2D collider in Colliders)
        {
            ContactFilter2D filter = new ContactFilter2D();
            int overlapResult = Physics2D.OverlapCollider(collider,filter, results);
            //Debug.Log($"OR {overlapResult} ResCount {results.Count(r=>r != null)}");

            if(overlapResult > 0)
            {
                var enemies = results.Where(c => c!= null && c.gameObject.tag == Constants.EnemyTag
                && !c.gameObject.GetComponent<EnemyScript>().IsDead);
                if(enemies.Any() && CanTarget())
                {
                    TargetEnemy(enemies);
                }
            }


            Array.Clear(results, 0, ColliderResultSize);
        }
    }

    void TargetEnemy(IEnumerable<Collider2D> enemies)
    {
        //TODO:Mechanizm wybierania najlepszego  celu
        Debug.Log($"Tower {GetInstanceID()} Targeted enemies");
        var enemy = enemies.FirstOrDefault().gameObject.GetComponent<EnemyScript>();
        TargetedEnemies.Clear();
        TargetedEnemies.Add(enemy);
        switch (AttackType)
        {
            default:
                //melee
                StartCoroutine(AttackMelee(enemy));
                break;
        }
    }

    IEnumerator AttackMelee(EnemyScript enemy)
    {
        Debug.Log("Attacking");
        do
        {
            enemy.Damage(Damage);
            var hit = Instantiate(Damage.HitPrefab, MeleeHitSource.position, Quaternion.identity,enemy.gameObject.transform);
            GameObject.Destroy(hit, 1);
            Debug.Log("Hit");
            yield return new WaitForSeconds(AttackSpeed);
        }
        while (IsEnemyInRange(enemy) && !enemy.IsDead);

        TargetedEnemies.Clear();

        Debug.Log("Stopped Attacking");
        yield return null;
    }

    bool IsEnemyInRange(EnemyScript enemy)
    {
        Collider2D[] results = new Collider2D[ColliderResultSize];
        foreach (Collider2D collider in Colliders)
        {
            ContactFilter2D filter = new ContactFilter2D();
            int overlapResult = Physics2D.OverlapCollider(collider, filter, results);
            //Debug.Log($"OR {overlapResult} ResCount {results.Count(r => r != null)}");

            if (overlapResult > 0)
            {
                if(results.Any(r => r != null && r.gameObject.GetInstanceID() == enemy.GetInstanceID()))
                    return true;
            }
            Array.Clear(results, 0, ColliderResultSize);
        }
        return false;
    }

    public void ShowRange()
    {
        if (Range != null)
            Range.enabled = true;
    }

    public void HideRange()
    {
        if (Range != null)
            Range.enabled = false;
    }
}
