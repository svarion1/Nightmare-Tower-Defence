using UnityEngine;
using System.Collections;

public class NemicoVolante : Nemico {
	public Transform target;

	public override void Awake() {
		vita = vitaMax;
		gestioneGioco = GameObject.Find("Main Camera");
		target = GameObject.FindGameObjectWithTag("Base").transform;
	}

	void Update () {
		gameObject.transform.LookAt(target);
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,gameObject.transform.rotation.eulerAngles.y,0));
		gameObject.transform.Translate(Vector3.forward * velocita * Time.deltaTime);
	}

}
