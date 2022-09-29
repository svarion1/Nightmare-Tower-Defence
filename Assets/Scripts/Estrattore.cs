using UnityEngine;
using System.Collections;

//l'estrattore va posizionato su caselle che contengono risorse per poterle estrarre ed utilizzarle
//per raccogliere le risorse bisogna clickare sull'estrattore
public class Estrattore : MonoBehaviour {

	public float risorseAlSecondo;  //quante risorse vengono estratte al secondo
	private float tempoRisorsa;  //tempo per l'estrazione di una singola risorsa
	private float tempoProssimaRisorsa;  //contatore del tempo
	public int risorse;  //contatore risorse estratte
	public int risorseMax;  //risorse massime accumulabili
	
	void Awake () {
		tempoRisorsa = 1/risorseAlSecondo;
		tempoProssimaRisorsa = tempoRisorsa;
	}
	
	void Update () {

		if(tempoProssimaRisorsa > 0)
		{
			tempoProssimaRisorsa -= Time.deltaTime;
		}
		else if(risorse < risorseMax)
		{
			risorse++;
			tempoProssimaRisorsa = tempoRisorsa;
		}
	}
}
