using UnityEngine;

[System.Serializable]
public class EnemiesGroup
{
   public GameObject enemy;
   public int quantity;

   private int index;

   public GameObject Next()
   {
      index++;
      if (index < quantity)
      {

         return enemy;
      }
      else return null;
   }
}
