using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


//WARNING!!! Attach this component to Main Camera
public class GameManager : MonoBehaviour
{
   private Camera mainCamera;
   private Transform objectHit;
   public GameObject selectedTurret, gui;
   public int resources = 100;
   private bool isPaused;

   [Header("Waves Design")]
   public Transform[] spawnPoints;
   public Wave[] waves;
   private Stack<Wave> wavesStack;

   private Wave currentWave;
   private int enemyIndex;
   private float nextWaveTime;
   private float nextEnemyTime;

   private Tile hoveredTile;

   [Header("UI")]
   public Text resourcesText;
   public Text nameText;
   public Text descriptionText;
   public Text damageText;
   public Text attacksPerSecondText;
   public Text rangeText;
   public Text pauseText;
   public GameObject turretsShop;


   void Start()
   {
      mainCamera = Camera.main;
      isPaused = false;

      wavesStack = new Stack<Wave>();

      for (int i = waves.Length - 1; i >= 0; i--)
      {
         wavesStack.Push(waves[i]);
      }

      currentWave = wavesStack.Pop();
      enemyIndex = 0;
      nextWaveTime = currentWave.startDelay;

      /*
      hoveredTile = GameObject.Find("Tile 1").GetComponent<Tile>();
      hoveredTile.OnHover();
      */
   }

   void Update()
   {
      //resourcesText.text = "Resources: " + resources;

      ManageSpawn();
      ManageInputs();
   }

   private void ManageSpawn()
   {
      if (nextWaveTime > 0)
      {
         nextWaveTime -= Time.deltaTime;
      }
      else
      {
         if (nextEnemyTime > 0)
         {
            nextEnemyTime -= Time.deltaTime;
         }
         else
         {
            SpawnEnemy();
         }
      }

   }

   private void SpawnEnemy()
   {
      Instantiate(currentWave.enemies[enemyIndex], spawnPoints[Random.Range(0, spawnPoints.Length)]);
      nextEnemyTime = currentWave.spawnDelay;
      enemyIndex++;

      if (enemyIndex >= currentWave.enemies.Length)
      {
         /*if (enemySetIndex < currentWave.enemies.Length)
         {

         }*/
         NextWave();
      }

   }

   private void NextWave()
   {
      currentWave = wavesStack.Pop();
      enemyIndex = 0;

      nextWaveTime = currentWave.startDelay;
      nextEnemyTime = currentWave.spawnDelay;
   }

   private void ManageInputs()
   {
      RaycastHit hit;

      /*
      if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100) && !hit.transform)
      {
         return;
      }
      */

      if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.collider.tag == "Tile")
      {
         if (hoveredTile && !Transform.Equals(hoveredTile.transform, hit.transform))
         {
            hoveredTile.OnExit();
         }


         hoveredTile = hit.transform.GetComponent<Tile>();
         hoveredTile.OnHover();
      }
      else
      {
         if (hoveredTile)
         {
            hoveredTile.OnExit();
            hoveredTile = null;
            HideTurretsShopUI();
         }
      }

      if (Input.GetMouseButtonDown(0) && !isPaused)
      {
         onMouseLeftClick(hit);
      }
      else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
      {
         Time.timeScale = 0;
         gui.SetActive(false);
         isPaused = true;
      }
      else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
      {
         Time.timeScale = 1;
         gui.SetActive(true);
         isPaused = false;
      }
   }

   public void SelectTurret(GameObject torretta)
   {
      this.selectedTurret = torretta;
      nameText.text = torretta.gameObject.name;
      descriptionText.text = torretta.GetComponent<Turret>().description;
      damageText.text = "Damage: " + torretta.GetComponent<Turret>().damage;
      attacksPerSecondText.text = "Attacks/sec: " + torretta.GetComponent<Turret>().attacksPerSecond;
      rangeText.text = "Range: " + torretta.GetComponent<Turret>().attackRange;
      Debug.Log(torretta.name + " turret selected");

   }

   private void onMouseLeftClick(RaycastHit hit)
   {
      if (hit.collider.tag == "Tile" && selectedTurret != null && hit.collider.GetComponent<Tile>().Taken == false && resources >= selectedTurret.GetComponent<Turret>().cost)
      {
         /*Instantiate(selectedTurret, hit.collider.transform.position + Vector3.up, new Quaternion());
         resources -= selectedTurret.GetComponent<Turret>().cost;
         hit.collider.GetComponent<Tile>().Taken = true;*/
         ShowTurretsShopUI();
      }
   }

   private void ShowTurretsShopUI()
   {
      turretsShop.GetComponent<Animation>().Play("Show Turrets Shop");
   }

   private void HideTurretsShopUI()
   {
      turretsShop.GetComponent<Animation>().Play("Hide Turrets Shop");
   }

   public void GameOver()
   {
      //TODO Game Over Logic
   }
}
