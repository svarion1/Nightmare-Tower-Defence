using UnityEngine;
using System.Collections;

//questa torretta può colpire più nemici con un solo attacco all'istante
public class TorrettaElettrica : Turret
{

   public int massimoNemiciColpiti;  //quanti nemici può colpire insieme

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
         else if (nextAttackTime <= 0 && gameManager.GetComponent<GestioneGioco>().energia >= energyConsumption)
         {
            for (int i = 0; i < colliders.Length && i < massimoNemiciColpiti; i++)
            {
               colliders[i].GetComponent<Nemico>().DannoBase(damage);
            }
            gameManager.GetComponent<GestioneGioco>().energia -= energyConsumption;
            nextAttackTime = initialTime;
         }
      }

   }
}
