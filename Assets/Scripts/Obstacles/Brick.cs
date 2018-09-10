using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Brick : MonoBehaviour {

	public float Lifetime = 3;

	public AudioSource BallHitSound;

	[HideInInspector]
	public int id { get; set; }

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
		ObstacleManager.instance.RemoveMe (this.gameObject);
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
