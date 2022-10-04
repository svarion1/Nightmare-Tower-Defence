using UnityEngine;
using UnityEngine.UI;


public class Turret : MonoBehaviour
{
   // TODO Transalte comments
   public LayerMask enemies;
   public float damage, attackRange, attacksPerSecond;
   public int cost;  //costo per l'acquisto di una torretta
   public int sellPrice;  //quante risorse vengono riborsate se la torretta viene rimossa
   public int energyConsumption;  //quantità di energy consumata ad ogni attacco
   public string description;
   public GameObject head, barrel, projectile;  //parti della torretta
   public GameObject leveledUpVersion;  //la torreta che viene sostituita al passaggio di livello
   public int maxExperience; //la torretta può salire di livello al rggiungimento del valore impostato
   public Button levelUpButton;
   public GameObject experienceBar;
   public Image bar;

   protected GameObject gameManager;
   protected float initialTime, nextAttackTime;
   protected int experience;
   protected Collider[] colliders;

   // Use this for initialization
   void Awake()
   {
      gameManager = GameObject.Find("Main Camera");
      experience = 0;
      initialTime = 1 / attacksPerSecond;
      nextAttackTime = initialTime;
   }

   // Update is called once per frame
   public virtual void Update()
   {
      if (nextAttackTime > 0)
      {
         nextAttackTime -= Time.deltaTime;
      }
      else if (nextAttackTime <= 0 && gameManager.GetComponent<GameManager>().energy >= energyConsumption)
      {
         colliders = Physics.OverlapSphere(transform.position, attackRange, enemies);

         if (colliders.Length > 0)
         {
            Shoot(colliders[0].transform);
            gameManager.GetComponent<GameManager>().energy -= energyConsumption;
            nextAttackTime = initialTime;
         }
      }

      if (colliders != null && colliders.Length > 0)
      {
         if (head != null && colliders[0] != null)
            head.transform.LookAt(colliders[0].transform);

         /*if(barraExp)
				barraExp.transform.LookAt(gestioneGioco.transform);*/
      }
   }


   void OnDrawGizmos()
   {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, attackRange);
   }

   private void FindEnemies()
   {


      if (nextAttackTime > 0)
      {
         nextAttackTime -= Time.deltaTime;
      }
      else if (nextAttackTime <= 0 && gameManager.GetComponent<GameManager>().energy >= energyConsumption)
      {
         colliders = Physics.OverlapSphere(transform.position, attackRange, enemies);

         if (colliders.Length > 0)
         {
            Shoot(colliders[0].transform);
            gameManager.GetComponent<GameManager>().energy -= energyConsumption;
            nextAttackTime = initialTime;
         }
      }
   }

   virtual protected void Shoot(Transform target)
   {
      projectile.GetComponent<Projectile>().damage = damage;
      projectile.GetComponent<Projectile>().target = target;
      GameObject.Instantiate(projectile, barrel.transform.position, head.transform.rotation);

      if (experience < maxExperience)
      {
         experience++;
         bar.transform.localScale = new Vector3(1 / maxExperience * experience, bar.transform.localScale.y, bar.transform.localScale.z);
         if (experience >= maxExperience)
         {
            levelUpButton.interactable = true;
         }
      }
   }
}
