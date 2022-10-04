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
   public GameObject damageText;
   public int pathNumber;
   private GameObject path;
   private int pathPoint; //punto del percorso a cui sta puntando
   public Image hpBar;
   public int droppedResources;  //risorse che vengono guadagnate all'uccisione

   public float attackRange;
   public float attackDelay; //il tempo che impiega per attaccare
   private float nextAttackDelay;  //contatore del tempo per il prossimo attacco

   private GameManager gameManager;


   public virtual void Awake()
   {
      hp = maxHp; //inizializza la vita
      gameManager = Camera.main.GetComponent<GameManager>();
      path = GameObject.Find("Path " + pathNumber);
      nextAttackDelay = attackDelay;
      transform.LookAt(path.GetComponent<Path>().waypoints[0]);
   }

   void Update()
   {
      hpBar.transform.LookAt(gameManager.transform);  //la barra della vita punta verso la camera

      if (pathPoint < path.GetComponent<Path>().waypoints.Length)
      {
         transform.Translate(Vector3.forward * speed * Time.deltaTime);  //si sposta in avanti
         //controlla se ha raggiunto il prossimo punto
         if (Distance(path.GetComponent<Path>().waypoints[pathPoint]) <= 0.1)
         {
            //punta al punto del percorso successivo
            pathPoint++;
            nextAttackDelay -= Time.deltaTime;
            if (nextAttackDelay <= 0)
               Attack();
         }
         transform.LookAt(path.GetComponent<Path>().waypoints[pathPoint]);
      }
      else
      {
         nextAttackDelay -= Time.deltaTime;
         if (nextAttackDelay <= 0)
            Attack();
      }

   }

   void OnTriggerEnter(Collider other)
   {

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

      if (other.tag == "Base")
      {
         Destroy(gameObject);
      }
   }

   //sottrae dalla vita il valore passato, fa apparire un testo che indica il damage subito, aggiorna la barra della vita e controlla la morte
   public void TakeDamage(float damage)
   {
      hp -= Mathf.RoundToInt(damage * (1 - damageReduction));
      damageText.GetComponent<Text>().text = "" + damage;
      GameObject.Instantiate(damageText, gameObject.transform.position, new Quaternion());
      hpBar.transform.localScale = new Vector3(1 / maxHp * hp, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
      DeathCheck();
   }

   void DeathCheck()
   {
      if (hp <= 0)
      {
         gameManager.GetComponent<GameManager>().resources += droppedResources;
         Destroy(gameObject);
      }
   }

   private float Distance(Transform target)
   {
      return (float)System.Math.Sqrt(System.Math.Pow(target.position.x - gameObject.transform.position.x, 2)
                                   + System.Math.Pow(target.position.z - gameObject.transform.position.z, 2));
   }

   private void Attack()
   {
      nextAttackDelay = attackDelay;
      GetComponent<Animation>().Play();
   }
}
