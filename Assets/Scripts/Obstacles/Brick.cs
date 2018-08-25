using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public float Lifetime = 3;

	public AudioSource BallHitSound;

	// Use this for initialization
	void Start () {
		Debug.Log ("Brick constructed: " + Time.realtimeSinceStartup);
		Destroy (this.gameObject, Lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		Debug.Log ("Brick destroyed: " + Time.realtimeSinceStartup);
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.CompareTag("Ball")){
			PlaySound();
		}
	}

	void PlaySound () {
		BallHitSound.Play ();
	}
}
