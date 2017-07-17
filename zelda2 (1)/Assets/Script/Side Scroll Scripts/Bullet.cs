using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("stuff happened.");
		if (other.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Debug.Log ("hit enemy");
			DestroyObject (other.gameObject);
		}
	}
}
