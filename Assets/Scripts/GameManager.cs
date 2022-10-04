using UnityEngine;
using UnityEngine.UI;


//WARNING!!! Attach this component to Main Camera
public class GameManager : MonoBehaviour
{
   private Camera mainCamera;
   private Transform objectHit;
   public GameObject selectedTurret, gui;
   public int resources;
   public int energy;
   public Text resourcesText, energyText, nameText, descriptionText, damageText, attacksPerSecondText, rangeText, pauseText;  //testi che mostrano statistiche di gioco
   private bool isPaused;


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
      resourcesText.text = "Risorse: " + resources;
      energyText.text = "Energia: " + energy;



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
      damageText.text = "Danno: " + torretta.GetComponent<Turret>().damage;
      attacksPerSecondText.text = "Attacchi/sec: " + torretta.GetComponent<Turret>().attacksPerSecond;
      rangeText.text = "Raggio: " + torretta.GetComponent<Turret>().range;
      Debug.Log("Torretta Selezionata");

   }

   private void onMouseLeftClick()
   {
      RaycastHit hit;

      if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100) && !hit.transform)
      {
         return;
      }

      if (hit.collider.tag == "Casella" && selectedTurret != null && hit.collider.GetComponent<Tile>().Taken == false && resources >= selectedTurret.GetComponent<Turret>().cost)
      {
         Instantiate(selectedTurret, hit.collider.transform.position + Vector3.up, new Quaternion());
         resources -= selectedTurret.GetComponent<Turret>().cost;
         hit.collider.GetComponent<Tile>().Taken = true;
      }
      else if (hit.collider.tag == "Estrattore")
      {
         resources += hit.collider.GetComponent<Estrattore>().risorse;
         hit.collider.GetComponent<Estrattore>().risorse = 0;

      }

   }

   public void GameOver()
   {

   }
}
