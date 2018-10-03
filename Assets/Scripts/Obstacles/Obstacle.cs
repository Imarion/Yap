using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Obstacle : MonoBehaviour {

	public float Lifetime = 3;

	public AudioSource BallHitSound;

	[HideInInspector]
	public int id { get; set; }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		Debug.Log ("Brick destroyed: " + Time.realtimeSinceStartup);
		ObstacleManager.instance.RemoveMe (this.gameObject);
	}

	public void PlaySound () {
		BallHitSound.Play ();
	}
}
