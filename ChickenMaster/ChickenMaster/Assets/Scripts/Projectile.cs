using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform Target;
    Vector2 moveDirection;
    float moveSpeed;
    DamageInfo damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitProjectile(Transform target, float moveSpeed, DamageInfo damage)
    {
        this.Target = target;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
    }

    public void StartMovement()
    {
        var rb = GetComponent<Rigidbody2D>();
        moveDirection = (Target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        //rotate projectile to direction
        Vector3 dir = Target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.EnemyTag)
        {
            var enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.Damage(damage);
            //TODO:play sound on hit
            Debug.Log("Projectile hit enemy");
        }
    }
}
