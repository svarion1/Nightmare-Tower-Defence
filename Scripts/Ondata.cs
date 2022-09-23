using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ondata : MonoBehaviour
{
	public float tempoAttesaOndata;
	public float tempoSpawnNemici;
	public GameObject[] nemici = new GameObject[0];
	public Transform[] puntiSpawn = new Transform[0];
	public Text testoOndata;
	
	private int nNemici = 0;
	private int ondata;  //a che ondata è arrivato
	private float tempoProssimoNemico;
	public string layoutOndate;
	private int currentSpawn = 0;
	private char tipoNemico;
	private float tempoProssimaOndata;


	private void SpawnNemico (char tipoNemico)
	{
		switch (tipoNemico) {
		case 'a':
			Instantiate (nemici [0], puntiSpawn [0].position, new Quaternion ());
			break;
		case 'b':
			Instantiate (nemici [1], puntiSpawn [0].position, new Quaternion ());
			break;
		default:
			break;
		}
	}


	// Use this for initialization
	void Start ()
	{
		tempoProssimoNemico = tempoSpawnNemici;
		tempoProssimaOndata = tempoAttesaOndata;
		ondata = 1;
	}

	// Update is called once per frame
	void Update ()
	{
		char c;
		//spawn dei nemici
		if(tempoProssimaOndata > 0)
		{
			tempoProssimaOndata -= Time.deltaTime;
			testoOndata.enabled = true;
		}
		else{
			testoOndata.enabled = false;
			tempoProssimoNemico -= Time.deltaTime;
			if (tempoProssimoNemico <= 0) {
				if (nNemici == 0) {
					for (int i = currentSpawn; i < layoutOndate.Length; i++) {
						c = layoutOndate [i];
						if (System.Char.IsLetter (c)) {
							tipoNemico = c;
							currentSpawn = i + 1;
							if (nNemici == 0)
								nNemici = 1;
							break;
						} else if (System.Char.IsNumber (c)) {
							nNemici += (int)Mathf.Pow(10, i) * (c - 48);
						} else if (c == '-') { 
							ondata++;
							tempoProssimaOndata = tempoAttesaOndata;
							testoOndata.text = "ONDATA " + ondata;
						}
					}
				}

				if (nNemici > 0) {
					SpawnNemico (tipoNemico);
					nNemici--;
					tempoProssimoNemico = tempoSpawnNemici;
				}
			}
		}//else
	}//Update
}//class
