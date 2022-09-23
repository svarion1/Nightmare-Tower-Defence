using UnityEngine;
using System.Collections;

public class Generatore : MonoBehaviour {
	public int energiaGenerata;  //quantità di energia generata ogni x secondi
	public float tempoGenerazione;  //tempo per generare x energia
	private float tempoProssimaGenerazione;  //contatore del tempo
	private GameObject gestioneGioco;  //puntatore alla gestione del gioco
	
	void Awake () {
		gestioneGioco = GameObject.Find("Main Camera");
		tempoProssimaGenerazione = tempoGenerazione;
	}

	void Update () {
		if(tempoProssimaGenerazione > 0)
		{
			tempoProssimaGenerazione -= Time.deltaTime;
		}
		else
		{
			gestioneGioco.GetComponent<GestioneGioco>().energia += energiaGenerata;
			tempoProssimaGenerazione = tempoGenerazione;
		}
	}
}
