using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifetime;
    private float lifeTimer;
    private float damage;
    [SerializeField] private Rigidbody2D rigidbody;

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Enemy>() != null)
        {
            collision.transform.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetProjectileDate(float newLifetime, float newDamage, float newSpeed, Vector2 direction)
    {
        lifetime = newLifetime;
        damage = newDamage;
        rigidbody.velocity = direction.normalized * newSpeed;
    }

}
