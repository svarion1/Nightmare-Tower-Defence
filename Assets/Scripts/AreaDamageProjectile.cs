using UnityEngine;

public class AreaDamageProjectile : Projectile
{
    public float damageRadius = 3f;
    public LayerMask enemyLayer;

    protected void OnTriggerEnter(Collider other)
   {
      //Debug.Log("Projectile Collision");

      if (other.CompareTag("Enemy"))
      {
         other.GetComponent<Enemy>().TakeDamage(damage);

         Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius, enemyLayer) ;

         foreach (Collider c in colliders)
         {
            if (CompareTag("Enemy"))
               c.GetComponent<Enemy>().TakeDamage(damage);
         }

         Destroy(gameObject);
      }
   }

/*
   void OnDrawGizmos()
   {
    Gizmos.Sphere(transform.position)
   }
   */
}
