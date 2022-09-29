using UnityEngine;
using System.Collections;

//questa torretta può colpire più nemici con un solo attacco all'istante
public class TorrettaElettrica : Turret
{

   public int massimoNemiciColpiti;  //quanti nemici può colpire insieme

   public override void Update()
   {
      Collider[] collisori = Physics.OverlapSphere(transform.position, raggio, enemies);
      if (collisori.Length > 0)
      {
         //ControllaLista();

         if (testa != null)
            testa.transform.LookAt(collisori[0].transform);

         if (tempoProssimoAttacco > 0)
         {
            tempoProssimoAttacco -= Time.deltaTime;
         }
         else if (tempoProssimoAttacco <= 0 && gestioneGioco.GetComponent<GestioneGioco>().energia >= consumoEnergia)
         {
            for (int i = 0; i < collisori.Length && i < massimoNemiciColpiti; i++)
            {
               collisori[i].GetComponent<Nemico>().DannoBase(damage);
            }
            gestioneGioco.GetComponent<GestioneGioco>().energia -= consumoEnergia;
            tempoProssimoAttacco = tempoIniz;
         }
      }

   }
}
