using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//questo script controlla i comportamenti dei nemici e lavora assieme allo script Navigazione
public class Enemy : MonoBehaviour
{
   public float speed = 1.0f;
   [Range(0f, 1f)]
   public float damageReduction = 0f;  // Value between 0 and 1, it defines damage reduction, when it's 0 is full damage, when it's 1 damege taken is completely cancelled
   public int maxHp = 100;
   protected int hp;
   public int damage = 5;
   private int pathPoint; //punto del percorso a cui sta puntando
   public float attackRange = 1.0f;
   public float attackDelay = 3.0f; //il tempo che impiega per attaccare
   private float nextAttackDelay;  //contatore del tempo per il prossimo attacco
   public int droppedResources = 20;  //risorse che vengono guadagnate all'uccisione
   public int pathNumber;
   //private Path path;
   private bool isDead;
   // UI elements
   //public Text damageText;
   //public Image hpBar;

   private GameManager gameManager;
   private Animator animator;
   private NavMeshAgent nav;
   private Base targetBase;

   public Base TargetBase
   {
      set
      {
         targetBase = value;
      }
   }

   void Start()
   {
      hp = maxHp;
      //path = GameObject.Find("Path " + pathNumber).GetComponent<Path>();
      isDead = false;
      nextAttackDelay = attackDelay;

      gameManager = Camera.main.GetComponent<GameManager>();
      animator = transform.GetChild(0).GetComponent<Animator>();
      nav = GetComponent<NavMeshAgent>();
      nav.speed = speed;

      //transform.LookAt(path.GetComponent<Path>().Waypoints[0]);

      // Game Manager should assign it when enemies gets spawned but in case it hasen't done yet enemy will find the base itself
      if (!targetBase)
      {
         GameObject.Find("Base");
      }

      nav.SetDestination(targetBase.transform.position);
   }

   void Update()
   {
      //hpBar.transform.LookAt(gameManager.transform);  //la barra della vita punta verso la camera
      if (!isDead)
      {
         if (Vector3.Distance(transform.position, targetBase.transform.position) < attackRange)
         {
            //Debug.Log("Enemy in Range!");

            nav.enabled = false;
            animator.SetTrigger("Base Reached");

            if (nextAttackDelay > 0)
            {
               nextAttackDelay -= Time.deltaTime;
            }
            else
            {
               Attack();
               nextAttackDelay = attackDelay;
            }
         }
         else
         {
            nav.SetDestination(targetBase.transform.position);
         }


         /*
         if (pathPoint < path.Waypoints.Length)
         {
            Move();
         }
         else
         {
            nextAttackDelay -= Time.deltaTime;
            if (nextAttackDelay <= 0)
               Attack();

            animator.SetTrigger("Base Reached");
         }
         */
      }
   }

   private void Move()
   {
      /*
      transform.LookAt(path.Waypoints[pathPoint]); // Si rivolge verso il punto del percorso verso il quale sta andando
      transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Si sposta in avanti
                                                                      //controlla se ha raggiunto il prossimo punto
      if (Distance(path.Waypoints[pathPoint]) <= 0.1)
      {
         pathPoint++;
      }
      */
   }

   //sottrae dalla vita il valore passato, fa apparire un testo che indica il damage subito, aggiorna la barra della vita e controlla la morte
   public void TakeDamage(float damage)
   {
      hp -= Mathf.RoundToInt(damage * (1 - damageReduction));

      if (hp < 0)
      {
         hp = 0;
         OnDeath();
      }

      //damageText.text = "" + damage;
      //GameObject.Instantiate(damageText.gameObject, gameObject.transform.position, new Quaternion());
      //hpBar.transform.localScale = new Vector3(1 / maxHp * hp, hpBar.transform.localScale.y, hpBar.transform.localScale.z);

      //Debug.Log("Enemy hp: " + hp);
   }

   void OnDeath()
   {
      isDead = true;
      GetComponent<Collider>().enabled = false;
      nav.enabled = false;
      gameManager.OnEnemyKill(this);
      animator.SetTrigger("Dead");
      Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length + 1);
   }
   private void Attack()
   {
      Debug.Log("Enemy Attack");
      targetBase.TakeDamage(damage);
   }
}
