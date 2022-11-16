using Unity.VisualScripting;
using UnityEngine;

public class BurningProjectile : Projectile
{
    public float duration = 3f;
    public float tickDuration = 1f;
    public float tickDamage = 10f;

    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Projectile Collision");

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            ContinuousDamageEffect cd = enemy.GetComponent<ContinuousDamageEffect>();
            
            if (cd == null)
            {
              cd = enemy.AddComponent<ContinuousDamageEffect>();
            }

            cd.duration = duration;
            cd.tickDuration = tickDuration;
            cd.tickDamage = tickDamage;
            enemy.TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
}
