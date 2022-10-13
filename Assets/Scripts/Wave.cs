using UnityEngine;

[System.Serializable]
public class Wave
{
   [Header("Timing")]
   public float startDelay = 6.0f;
   public float spawnDelay = 2.5f; // Delay in seconds between each enemy spawn

   [Header("Layout")]
   public EnemiesGroup[] enemiesGroups;

   private int index;

   public GameObject Next()
   {
      if (index < enemiesGroups.Length)
      {
         GameObject newEnemy = enemiesGroups[index].Next();

         if (newEnemy)
         {
            return newEnemy;
         }
         else
         {
            index++;
            return Next();
         }
      }
      else return null;
   }
}
