using UnityEngine;
using System.Collections;

public class TestoDanni : MonoBehaviour {

    public float tempo, velocita;
	
	// Update is called once per frame
	void Update () {
        tempo -= Time.deltaTime;
        gameObject.transform.Translate(0, velocita * Time.deltaTime, 0);
        if (tempo <= 0)
            Destroy(gameObject);
	}
}
