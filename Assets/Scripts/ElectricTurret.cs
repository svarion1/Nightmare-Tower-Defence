using UnityEngine;

//questa torretta può colpire più nemici con un solo attacco all'istante
public class ElectricTurret : Turret
{

   public int maxEnemyPerAttack;  //quanti nemici può colpire insieme

   public override void Update()
   {
      Collider[] colliders = Physics.OverlapSphere(transform.position, range, enemies);
      if (colliders.Length > 0)
      {
         //ControllaLista();

         if (head != null)
            head.transform.LookAt(colliders[0].transform);

         if (nextAttackTime > 0)
         {
            nextAttackTime -= Time.deltaTime;
         }
         else if (nextAttackTime <= 0 && gameManager.GetComponent<GameManager>().energy >= energyConsumption)
         {
            for (int i = 0; i < colliders.Length && i < maxEnemyPerAttack; i++)
            {
               colliders[i].GetComponent<Enemy>().TakeDamage(damage);
            }
            gameManager.GetComponent<GameManager>().energy -= energyConsumption;
            nextAttackTime = initialTime;
         }
      }

   }
}
