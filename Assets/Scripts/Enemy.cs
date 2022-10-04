using UnityEngine;
using UnityEngine.UI;

//questo script controlla i comportamenti dei nemici e lavora assieme allo script Navigazione
public class Enemy : MonoBehaviour
{
   public float speed = 1.0f;
   public float damageReduction = 0f;  // Value between 0 and 1, it defines damage reduction, when it's 0 is full damage, when it's 1 damege taken is completely cancelled
   public int maxHp = 100;
   protected int hp;
   public int damage = 5;
   public Text damageText;
   public int pathNumber;
   private Path path;
   private int pathPoint; //punto del percorso a cui sta puntando
   public Image hpBar;
   public int droppedResources = 20;  //risorse che vengono guadagnate all'uccisione
   public float attackRange = 1.0f;
   public float attackDelay = 3.0f; //il tempo che impiega per attaccare
   private float nextAttackDelay;  //contatore del tempo per il prossimo attacco

   private GameManager gameManager;
   private Animator animator;

   public virtual void Start()
   {
      hp = maxHp; //inizializza la vita
      path = GameObject.Find("Path " + pathNumber).GetComponent<Path>();

      gameManager = Camera.main.GetComponent<GameManager>();
      animator = transform.GetChild(0).GetComponent<Animator>();

      nextAttackDelay = attackDelay;
      transform.LookAt(path.GetComponent<Path>().Waypoints[0]);
   }

   void Update()
   {
      //hpBar.transform.LookAt(gameManager.transform);  //la barra della vita punta verso la camera

      if (pathPoint < path.Waypoints.Length)
      {
         transform.LookAt(path.Waypoints[pathPoint]); // Si rivolge verso il punto del percorso verso il quale sta andando
         transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Si sposta in avanti
         //controlla se ha raggiunto il prossimo punto
         if (Distance(path.Waypoints[pathPoint]) <= 0.1)
         {
            //punta al punto del percorso successivo
            pathPoint++;
            /*nextAttackDelay -= Time.deltaTime;
            if (nextAttackDelay <= 0)
               Attack();*/
         }

      }
      else
      {
         nextAttackDelay -= Time.deltaTime;
         if (nextAttackDelay <= 0)
            Attack();

         animator.SetTrigger("Base Reached");
      }

   }

   void OnTriggerEnter(Collider other)
   {

      /*
      if (other.tag == "Projectile")
      {
         Debug.Log("Danno");
         TakeDamage(other.GetComponent<Projectile>().damage);
      }

      if (other.tag == "Poisonous Projectile")
      {
         TakeDamage(other.GetComponent<ProjectilePoisonous>().damage);
         gameObject.AddComponent<Avvelenamento>();
         gameObject.GetComponent<Avvelenamento>().durata = other.GetComponent<ProjectilePoisonous>().poisonDuration;
         gameObject.GetComponent<Avvelenamento>().tempoAttivazione = other.GetComponent<ProjectilePoisonous>().poisonEffectFrequency;
         gameObject.GetComponent<Avvelenamento>().damage = other.GetComponent<ProjectilePoisonous>().poisonDamage;
         Debug.Log("Poisoned Enemy");
      }
      */
      if (other.tag == "Base")
      {
         //Destroy(gameObject);
      }
   }

   //sottrae dalla vita il valore passato, fa apparire un testo che indica il damage subito, aggiorna la barra della vita e controlla la morte
   public void TakeDamage(float damage)
   {
      hp -= Mathf.RoundToInt(damage * (1 - damageReduction));
      damageText.text = "" + damage;
      GameObject.Instantiate(damageText.gameObject, gameObject.transform.position, new Quaternion());
      hpBar.transform.localScale = new Vector3(1 / maxHp * hp, hpBar.transform.localScale.y, hpBar.transform.localScale.z);

      Debug.Log("Enemy hp: " + hp);

      DeathCheck();
   }

   void DeathCheck()
   {
      if (hp <= 0)
      {
         gameManager.resources += droppedResources;
         animator.SetTrigger("Dead");
         Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length + 1);
      }
   }

   private float Distance(Transform target)
   {
      return (float)System.Math.Sqrt(System.Math.Pow(target.position.x - gameObject.transform.position.x, 2)
                                   + System.Math.Pow(target.position.z - gameObject.transform.position.z, 2));
   }

   private void Attack()
   {
      // TODO Attack Logic
   }
}
