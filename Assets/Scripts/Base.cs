using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{

   public int vita;

   void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Enemy")
      {
         vita -= other.GetComponent<Enemy>().damage;
         ControllaGameOver();
      }
   }

   private bool ControllaGameOver()
   {
      if (vita <= 0)
         return true;
      else
         return false;
   }
}
