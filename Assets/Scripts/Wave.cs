using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave Scriptable Object", order = 1)]
public class Wave : ScriptableObject
{
   [Header("Timing")]
   public float startDelay = 6.0f;
   public float spawnDelay = 2.5f; // Delay in seconds between each enemy spawn

   [Header("Layout")]
   //public string waveLayout;
   public GameObject[] enemies;

   /*
      private int enemiesCount;
      private int waveCount;  //a che ondata è arrivato
      private float nextWaveDelay, nextSpawnDelay;
      private int currentSpawn;
      private char enemyChar;
   */

   /*
   private void SpawnEnemy(char tipoNemico)
   {
      switch (tipoNemico)
      {
         case 'a':
            Instantiate(enemies[0], spawnPoints[0].position, new Quaternion());
            break;
         case 'b':
            Instantiate(enemies[1], spawnPoints[0].position, new Quaternion());
            break;
         default:
            break;
      }
   }
   */

   /*
   void Start()
   {
      nextSpawnTime = spawnDelay;
      nextWaveDelay = timeDelay;
      waveCount = 1;
   }

   void Update()
   {
      char c;
      //spawn dei nemici
      if (nextWaveDelay > 0)
      {
         nextWaveDelay -= Time.deltaTime;
         waveText.enabled = true;
      }
      else
      {
         waveText.enabled = false;
         nextSpawnTime -= Time.deltaTime;
         if (nextSpawnTime <= 0)
         {
            if (enemiesCount == 0)
            {
               for (int i = currentSpawn; i < waveLayout.Length; i++)
               {
                  c = waveLayout[i];
                  if (System.Char.IsLetter(c))
                  {
                     enemyChar = c;
                     currentSpawn = i + 1;
                     if (enemiesCount == 0)
                        enemiesCount = 1;
                     break;
                  }
                  else if (System.Char.IsNumber(c))
                  {
                     enemiesCount += (int)Mathf.Pow(10, i) * (c - 48);
                  }
                  else if (c == '-')
                  {
                     waveCount++;
                     nextWaveDelay = timeDelay;
                     waveText.text = "ONDATA " + waveCount;
                  }
               }
            }

            if (enemiesCount > 0)
            {
               SpawnEnemy(enemyChar);
               enemiesCount--;
               nextSpawnTime = spawnDelay;
            }
         }
      }
   }*/
}
