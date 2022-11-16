using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    public float explosionRadius = 3f;
    public LayerMask enemyLayer;

   protected override void OnTriggerEnter(Collider other)
   {
      //Debug.Log("Projectile Collision");

      // Damage every enemy inside explosion radius
      if (other.CompareTag("Enemy"))
      {
         Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer) ;

         foreach (Collider c in colliders)
         {
            c.GetComponent<Enemy>().TakeDamage(damage);
         }

         Destroy(gameObject);
      }
   }
   
   void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(transform.position, explosionRadius);
   }
}
