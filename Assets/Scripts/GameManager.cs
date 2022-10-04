using UnityEngine;
using UnityEngine.UI;


//WARNING!!! Attach this component to Main Camera
public class GameManager : MonoBehaviour
{
   private Camera mainCamera;
   private Transform objectHit;
   public GameObject selectedTurret, gui;
   public int resources = 100;
   public Text resourcesText, nameText, descriptionText, damageText, attacksPerSecondText, rangeText, pauseText;  //testi che mostrano statistiche di gioco
   private bool isPaused;

   [Header("Waves")]
   public Wave[] waves;

   // Use this for initialization
   void Start()
   {
      mainCamera = Camera.main;
      isPaused = false;
   }

   // Update is called once per frame
   void Update()
   {
      //testo delle risorse
      resourcesText.text = "Resources: " + resources;



      if (Input.GetMouseButtonDown(0) && !isPaused)
      {
         onMouseLeftClick();
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

   private void onMouseLeftClick()
   {
      RaycastHit hit;

      if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100) && !hit.transform)
      {
         return;
      }

      if (hit.collider.tag == "Tile" && selectedTurret != null && hit.collider.GetComponent<Tile>().Taken == false && resources >= selectedTurret.GetComponent<Turret>().cost)
      {
         Instantiate(selectedTurret, hit.collider.transform.position + Vector3.up, new Quaternion());
         resources -= selectedTurret.GetComponent<Turret>().cost;
         hit.collider.GetComponent<Tile>().Taken = true;
      }
   }

   public void GameOver()
   {
      //TODO Game Over Logic
   }
}
