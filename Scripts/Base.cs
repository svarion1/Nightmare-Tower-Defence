using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

	public int vita;

	void OnTriggerEnter(Collider other){
		if(other.tag == "Nemico")
		{
			vita -= other.GetComponent<Nemico>().danno;
			ControllaGameOver();
		}
	}

	private bool ControllaGameOver(){
		if(vita<=0)
			return true;
		else
			return false;
	}
}
