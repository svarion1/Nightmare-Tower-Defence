using UnityEngine;
using System.Collections;

public class Proiettile : MonoBehaviour {

    public float velocita, danno, tempoVita;
    public Transform target;

	void Start() {
		Destroy(gameObject, tempoVita);
	}

	// Update is called once per frame
	void Update () {
        transform.LookAt(target);
        transform.Translate(Vector3.forward * velocita * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Nemico")
        {
            Destroy(gameObject);
        }
    }
}
