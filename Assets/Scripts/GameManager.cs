using UnityEngine;
using UnityEngine.UI;
using TMPro;


//WARNING!!! Attach this component to Main Camera
public class GameManager : MonoBehaviour
{

   #region Fields


   // public GameObject selectedTurret;

   public Base playerBase;

   [Header("Waves Design")]
   public Transform[] spawnPoints;
   public Wave[] waves;

   [Header("UI")]
   public GameObject playInterface;
   public GameObject pauseInterface;
   public GameObject turretsShop;
   public Text resourcesText;
   public Text nameText;
   public Text descriptionText;
   public Text damageText;
   public Text attacksPerSecondText;
   public Text rangeText;
   public Text pauseText;
   public GameObject gameoverText;

   private bool isPaused;
   private bool isWon;
   private bool isTurretsShopOpened;
   private int resources = 100;
   private Camera mainCamera;
   //private Stack<Wave> wavesStack;
   private int waveIndex;
   //private int enemyIndex;
   private float nextWaveTime;
   private float nextEnemyTime;
   private Tile hoveredTile;
   private Tile selectedTile;

   #endregion


   #region Init

   void Start()
   {
      mainCamera = Camera.main;
      isPaused = false;
      isWon = false;

      waveIndex = 0;

      //wavesStack = new Stack<Wave>();

      /*
      for (int i = waves.Length - 1; i >= 0; i--)
      {
         wavesStack.Push(waves[i]);
      }
      */

      //currentWave = wavesStack.Pop();
      //enemyIndex = 0;

      /*
      hoveredTile = GameObject.Find("Tile 1").GetComponent<Tile>();
      hoveredTile.OnHover();
      */
   }

   #endregion


   #region Game Loop Events

   void Update()
   {
      //resourcesText.text = "Resources: " + resources;

      Debug.Log("Wave Index: " + waveIndex);

      ManageSpawn();
      ManageInputs();
   }

   private void ManageSpawn()
   {
      if (!isWon)
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
               nextEnemyTime = waves[waveIndex].spawnDelay;
            }
         }
      }
   }

   private void SpawnEnemy()
   {
      GameObject newEnemy = waves[waveIndex].Next();

      if (!newEnemy)
      {
         NextWave();
         return;
      }

      Instantiate(newEnemy, spawnPoints[Random.Range(0, spawnPoints.Length)]).GetComponent<Enemy>().TargetBase = playerBase;
   }

   private void NextWave()
   {
      if (waveIndex < waves.Length)
         waveIndex++;
      else
         isWon = true;

   }

   public void OnEnemyKill(Enemy killedEnemy)
   {
      resources += killedEnemy.droppedResources;
   }

   public void OnGameOver()
   {
      gameoverText.SetActive(true);
   }

   #endregion


   #region Input Management

   private void ManageInputs()
   {
      if (!isPaused)
      {
         RaycastHit hit;

         /*
         if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100) && !hit.transform)
         {
            return;
         }
         */

         if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.collider.CompareTag("Tile") && hit.collider.GetComponent<Tile>().Taken == false)
         {
            if (!selectedTile)
            {
               if (!hoveredTile)
               {
                  hoveredTile = hit.transform.GetComponent<Tile>();
                  hoveredTile.OnHover();
               }
               else if (!Transform.Equals(hoveredTile.transform, hit.transform))
               {
                  hoveredTile.OnExit();
                  hoveredTile = hit.transform.GetComponent<Tile>();
                  hoveredTile.OnHover();
               }
            }

            if (Input.GetMouseButtonDown(0))
            {
               OnMouseLeftClick(hit);
            }
         }

         if (Input.GetKeyDown(KeyCode.Escape))
         {
            OnPause();
         }
         else if (Input.GetKeyDown(KeyCode.Escape))
         {
            OnPlay();
         }
      }
   }

   /*
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
   */

   private void OnMouseLeftClick(RaycastHit hit)
   {
      if (hit.collider.CompareTag("Tile") /*&& selectedTurret != null /*&& resources >= selectedTurret.GetComponent<Turret>().cost*/)
      {
         /*Instantiate(selectedTurret, hit.collider.transform.position + Vector3.up, new Quaternion());
         resources -= selectedTurret.GetComponent<Turret>().cost;
         hit.collider.GetComponent<Tile>().Taken = true;*/
         if (selectedTile) selectedTile.OnExit();
         selectedTile = hit.transform.GetComponent<Tile>();
         selectedTile.OnSelect();
         if (!isTurretsShopOpened)
         {
            ShowTurretsShopUI();
         }
      }
   }

   public void OnTurretBuy(GameObject turretGameObject)
   {
      if (selectedTile)
      {
         Turret turret = turretGameObject.GetComponent<Turret>();

         if (turret.cost <= resources)
         {
            resources -= turret.cost;
            PlaceTurret(turretGameObject);
         }
      }
   }

   public void PlaceTurret(GameObject turretGameObject)
   {
      Instantiate(turretGameObject, selectedTile.transform);
      selectedTile.OnTake();

      if (isTurretsShopOpened)
      {
         HideTurretsShopUI();
      }
   }

   private void ShowTurretsShopUI()
   {
      isTurretsShopOpened = true;
      turretsShop.GetComponent<Animation>().Play("Show Turrets Shop");
   }

   public void HideTurretsShopUI()
   {
      isTurretsShopOpened = false;
      selectedTile.OnExit();
      hoveredTile = null;
      selectedTile = null;
      turretsShop.GetComponent<Animation>().Play("Hide Turrets Shop");
   }

   private void OnPlay()
   {
      Time.timeScale = 1;
      playInterface.SetActive(true);
      pauseInterface.SetActive(false);
      isPaused = false;
   }

   private void OnPause()
   {
      Time.timeScale = 0;
      playInterface.SetActive(false);
      pauseInterface.SetActive(true);
      isPaused = true;
   }

   #endregion
}
