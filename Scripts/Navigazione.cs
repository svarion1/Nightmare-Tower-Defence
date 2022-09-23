using UnityEngine;
using System.Collections;

public class Navigazione : MonoBehaviour {

	public GameObject target;
	private UnityEngine.AI.NavMeshAgent agente;

	// Use this for initialization
	void Awake () {
		agente = GetComponent <UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agente.SetDestination(target.transform.position);
	}
}
