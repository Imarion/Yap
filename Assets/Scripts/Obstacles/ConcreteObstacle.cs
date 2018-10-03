using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteObstacle : Obstacle {

	// Use this for initialization
	void Start () {
		Debug.Log ("Brick constructed: " + Time.realtimeSinceStartup);
		Destroy (this.gameObject, Lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.CompareTag("Ball")){
			PlaySound();
		}
	}
}
