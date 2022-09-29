using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//questo script controlla i comportamenti dei nemici e lavora assieme allo script Navigazione
public class Nemico : MonoBehaviour {
	public float velocita; 
	public int armatura;  //percentuale di danni assorbiti
	public float vitaMax;
    protected float vita;
    public GameObject barraVita, testoDanno;
	public GameObject gestioneGioco;
	public int percorsoDaSeguire;
	private GameObject percorso;
	private int punto; //punto del percorso a cui sta puntando
	public Image barra;
    public int guadagno;  //risorse che vengono guadagnate all'uccisione
	public int danno;
	public float distanzaAttacco;
	public float tempoAttacco;	//il tempo che impiega per attaccare
	private float tempoProssimoAttacco;  //contatore del tempo per il prossimo attacco

	//inizzializzazione del nemico
	public virtual void Awake () {
        vita = vitaMax;		//inizializza la vita
       /* GetComponent<NavMeshAgent>().speed = velocita;
		GetComponent<NavMeshAgent>().stoppingDistance = distanzaAttacco;
        GetComponent<Navigazione>().target = GameObject.FindGameObjectWithTag("Base");*/
        gestioneGioco = GameObject.Find("Main Camera");
		percorso = GameObject.Find("Percorso " + percorsoDaSeguire);
		tempoProssimoAttacco = tempoAttacco;
		transform.LookAt(percorso.GetComponent<Percorso>().punti[0]);
	}

	//operazioni eseguite ad ogni frame
	void Update() {
		barraVita.transform.LookAt(gestioneGioco.transform);  //la barra della vita punta verso la camera

		if(punto < percorso.GetComponent<Percorso>().punti.Length)
		{
		transform.Translate(Vector3.forward * velocita * Time.deltaTime);  //si sposta in avanti
		//controlla se ha raggiunto il prossimo punto
		if(Distanza(percorso.GetComponent<Percorso>().punti[punto]) <= 0.1)
		{
			//punta al punto del percorso successivo
			punto++;
				tempoProssimoAttacco -= Time.deltaTime;
				if(tempoProssimoAttacco <= 0)
					Attacco();
			}
			transform.LookAt(percorso.GetComponent<Percorso>().punti[punto]);
		}else {
			tempoProssimoAttacco -= Time.deltaTime;
			if(tempoProssimoAttacco <= 0)
			Attacco();
		}

	}

    void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Proiettile")
        {
            Debug.Log("Danno");
            DannoBase(other.GetComponent<Proiettile>().danno);
        }

        if (other.tag == "ProiettileVelenoso")
        {
			DannoBase(other.GetComponent<ProiettileVelenoso>().danno);
            gameObject.AddComponent<Avvelenamento>();
            gameObject.GetComponent<Avvelenamento>().durata = other.GetComponent<ProiettileVelenoso>().tempoDurataVeleno;
            gameObject.GetComponent<Avvelenamento>().tempoAttivazione = other.GetComponent<ProiettileVelenoso>().tempoAttivazione;
            gameObject.GetComponent<Avvelenamento>().danno = other.GetComponent<ProiettileVelenoso>().dannoVeleno;
            Debug.Log("Nemico Avvelenato");
        }

		if(other.tag == "Base")
		{
			Destroy(gameObject);
		}
    }

	//calcola il danno sottraendo una percentuale data dall'armatura e lo sottrae dalla vita
	public void DannoBase(float danno) {
		SubisciDanno((int)System.Math.Round((danno-(danno*armatura/100))));

	}

	//sottrae dalla vita il valore passato, fa apparire un testo che indica il danno subito, aggiorna la barra della vita e controlla la morte
    public void SubisciDanno(float danno)
    {
        vita -= danno;
        testoDanno.GetComponent<Text>().text = ""+danno;
        GameObject.Instantiate(testoDanno, gameObject.transform.position, new Quaternion());
        barra.transform.localScale = new Vector3(1 / vitaMax * vita, barra.transform.localScale.y, barra.transform.localScale.z);
		ControllaMorte();
    }

    void ControllaMorte() {
        if(vita <= 0)
        {
            gestioneGioco.GetComponent<GestioneGioco>().risorse += guadagno;
            Destroy(gameObject);
        }
    }

	private float Distanza(Transform target) {
		return (float)System.Math.Sqrt(System.Math.Pow(target.position.x - gameObject.transform.position.x, 2)
		                             + System.Math.Pow(target.position.z - gameObject.transform.position.z, 2));
	}

	private void Attacco() {
		tempoProssimoAttacco = tempoAttacco;
		GetComponent<Animation>().Play();
	}
}
