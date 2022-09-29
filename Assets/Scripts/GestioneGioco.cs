using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


//ATTENZIONE!!! Attaccare questo componente a "Main Camera"
public class GestioneGioco : MonoBehaviour
{
   public Camera camera;
   private Transform objectHit;
   public GameObject torretta, gui;
   public int risorse;
   public int energia;
   public Text testoRisorse, testoEnergia, testoNome, testoDescrizione, testoDanno, testoAttacchiPerSecondo, testoRaggio, testoPausa;  //testi che mostrano statistiche di gioco
   private bool pausa;


   // Use this for initialization
   void Start()
   {
      pausa = false;
      camera = GetComponent<Camera>();

   }

   // Update is called once per frame
   void Update()
   {
      //testo delle risorse
      testoRisorse.text = "Risorse: " + risorse;
      testoEnergia.text = "Energia: " + energia;



      if (Input.GetMouseButtonDown(0) && !pausa)
      {
         PressioneMouse();
      }
      else if (Input.GetKeyDown(KeyCode.Escape) && !pausa)
      {
         Time.timeScale = 0;
         gui.SetActive(false);
         // camera.GetComponent<BlurOptimized>().enabled = true;
         pausa = true;
      }
      else if (Input.GetKeyDown(KeyCode.Escape) && pausa)
      {
         Time.timeScale = 1;
         gui.SetActive(true);
         //     camera.GetComponent<BlurOptimized>().enabled = false;
         pausa = false;
      }
   }

   public void SettaOggetto(GameObject torretta)
   {
      this.torretta = torretta;
      testoNome.text = torretta.gameObject.name;
      testoDescrizione.text = torretta.GetComponent<Turret>().descrizione;
      testoDanno.text = "Danno: " + torretta.GetComponent<Turret>().damage;
      testoAttacchiPerSecondo.text = "Attacchi/sec: " + torretta.GetComponent<Turret>().attacchiPerSecondo;
      testoRaggio.text = "Raggio: " + torretta.GetComponent<Turret>().raggio;
      Debug.Log("Torretta Selezionata");

   }

   private void PressioneMouse()
   {
      RaycastHit hit;

      if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100) && !hit.transform)
      {
         return;
      }

      if (hit.collider.tag == "Casella" && torretta != null && hit.collider.GetComponent<Casella>().occupata == false && risorse >= torretta.GetComponent<Turret>().costo)
      {
         Instantiate(torretta, hit.collider.transform.position + Vector3.up, new Quaternion());
         risorse -= torretta.GetComponent<Turret>().costo;
         hit.collider.GetComponent<Casella>().occupata = true;
      }
      else if (hit.collider.tag == "Estrattore")
      {
         risorse += hit.collider.GetComponent<Estrattore>().risorse;
         hit.collider.GetComponent<Estrattore>().risorse = 0;

      }

   }

   public void GameOver()
   {

   }
}
