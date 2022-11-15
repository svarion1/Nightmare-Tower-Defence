using UnityEngine;
using UnityEngine.UI;


public class Turret : MonoBehaviour
{
   // TODO Transalte comments
   public LayerMask enemiesMask;
   public float damage = 25f, attackRange = 3f, attacksPerSecond = 1.0f;
   public int cost = 100;  //costo per l'acquisto di una torretta
   public int sellPrice = 50;  //quante risorse vengono riborsate se la torretta viene rimossa
   public int maxExperience = 300; //la torretta può salire di livello al rggiungimento del valore impostato
   public string description = "This is a turret";
   public GameObject head, barrel, projectile;  //parti della torretta

   protected GameObject gameManager;
   protected float initialTime, nextAttackTime;
   protected int experience;
   protected Collider[] colliders;

   // Use this for initialization
   protected void Awake()
   {
      gameManager = GameObject.Find("Main Camera");
      experience = 0;
      initialTime = 1 / attacksPerSecond;
      nextAttackTime = initialTime;
   }

   // Update is called once per frame
   public virtual void Update()
   {
      /*
      if (nextAttackTime > 0)
      {
         nextAttackTime -= Time.deltaTime;
      }
      else if (nextAttackTime <= 0)
      {
         colliders = Physics.OverlapSphere(transform.position, attackRange, enemiesMask);
         Debug.Log(colliders.Length);

         if (colliders.Length > 0)
         {
            Shoot(colliders[0].transform);
            nextAttackTime = initialTime;
         }
      }
      */
      
      FindEnemies();
      
      if (colliders != null && colliders.Length > 0)
      {
         if (head != null && colliders[0] != null)
         {
            head.transform.LookAt(colliders[0].transform);
         }
      }
   }
   
   protected void OnDrawGizmos()
   {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, attackRange);
   }

   protected void FindEnemies()
   {
      if (nextAttackTime > 0)
      {
         nextAttackTime -= Time.deltaTime;
      }
      else if (nextAttackTime <= 0)
      {
         colliders = Physics.OverlapSphere(transform.position, attackRange, enemiesMask);

         if (colliders.Length > 0)
         {
            Shoot(colliders[0].transform);
            nextAttackTime = initialTime;
         }
      }
   }

   protected virtual void Shoot(Transform target)
   {
      GameObject newProjectile = Instantiate(projectile, barrel.transform.position, head.transform.rotation);
      newProjectile.GetComponent<Projectile>().damage = damage;
      newProjectile.GetComponent<Projectile>().target = target;
      
   }
}
