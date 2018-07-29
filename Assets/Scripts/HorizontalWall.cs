using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorizontalWall : MonoBehaviour {

	public AudioSource hitSound;

	void OnCollisionEnter (Collision col) {
		if (col.collider.name == "Ball") {
			PlaySound ();
		}
	}

	void PlaySound() {
		hitSound.Play ();
	}
}
