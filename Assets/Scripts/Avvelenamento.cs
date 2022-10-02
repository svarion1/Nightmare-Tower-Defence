using UnityEngine;
using System.Collections;

public class Avvelenamento : MonoBehaviour
{

   public float durata, tempoAttivazione, tempo, damage;


   // Use this for initialization
   void Start()
   {
      tempo = tempoAttivazione;
   }

   // Update is called once per frame
   void Update()
   {
      durata -= Time.deltaTime;
      if (durata > 0)
      {
         tempo -= Time.deltaTime;
         if (tempo <= 0)
         {
            Debug.Log("Danno");
            gameObject.GetComponent<Enemy>().TakeDamage(damage);
            tempo = tempoAttivazione;
         }
      }
      else
      {
         Destroy(this);
      }
   }
}
