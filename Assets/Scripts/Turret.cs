using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Turret : MonoBehaviour
{
   // TODO Transalte every name from Italian to English
   public LayerMask enemies;
   public float damage, raggio, attacchiPerSecondo;
   public int costo;  //costo per l'acquisto di una torretta
   public int prezzoVendita;  //quante risorse vengono riborsate se la torretta viene rimossa
   public int consumoEnergia;  //quantità di energia consumata ad ogni attacco
   public string descrizione;
   public GameObject testa, canna, proiettile;  //parti della torretta
   public GameObject prossimoLivello;  //la torreta che viene sostituita al passaggio di livello
   public int expMax; //la torretta può salire di livello al rggiungimento del valore impostato
   public Button lvUp;
   public GameObject barraExp;
   public Image barra;

   protected GameObject gestioneGioco;
   protected float tempoIniz, tempoProssimoAttacco;
   protected int exp;
   protected Collider[] collisori;

   // Use this for initialization
   void Awake()
   {
      gestioneGioco = GameObject.Find("Main Camera");
      exp = 0;
      tempoIniz = 1 / attacchiPerSecondo;
      tempoProssimoAttacco = tempoIniz;
   }

   // Update is called once per frame
   public virtual void Update()
   {
      if (tempoProssimoAttacco > 0)
      {
         tempoProssimoAttacco -= Time.deltaTime;
      }
      else if (tempoProssimoAttacco <= 0 && gestioneGioco.GetComponent<GestioneGioco>().energia >= consumoEnergia)
      {
         collisori = Physics.OverlapSphere(transform.position, raggio, enemies);

         if (collisori.Length > 0)
         {
            Spara(collisori[0].transform);
            gestioneGioco.GetComponent<GestioneGioco>().energia -= consumoEnergia;
            tempoProssimoAttacco = tempoIniz;
         }
      }

      if (collisori != null && collisori.Length > 0)
      {
         if (testa != null && collisori[0] != null)
            testa.transform.LookAt(collisori[0].transform);

         /*if(barraExp)
				barraExp.transform.LookAt(gestioneGioco.transform);*/
      }
   }

   private void TrovaNemici()
   {


      if (tempoProssimoAttacco > 0)
      {
         tempoProssimoAttacco -= Time.deltaTime;
      }
      else if (tempoProssimoAttacco <= 0 && gestioneGioco.GetComponent<GestioneGioco>().energia >= consumoEnergia)
      {
         collisori = Physics.OverlapSphere(transform.position, raggio, enemies);

         if (collisori.Length > 0)
         {
            Spara(collisori[0].transform);
            gestioneGioco.GetComponent<GestioneGioco>().energia -= consumoEnergia;
            tempoProssimoAttacco = tempoIniz;
         }
      }
   }

   virtual protected void Spara(Transform target)
   {
      proiettile.GetComponent<Proiettile>().danno = damage;
      proiettile.GetComponent<Proiettile>().target = target;
      GameObject.Instantiate(proiettile, canna.transform.position, testa.transform.rotation);

      if (exp < expMax)
      {
         exp++;
         barra.transform.localScale = new Vector3(1 / expMax * exp, barra.transform.localScale.y, barra.transform.localScale.z);
         if (exp >= expMax)
         {
            lvUp.interactable = true;
         }
      }
   }
}
