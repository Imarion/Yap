using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SideWall : MonoBehaviour {

	public AudioSource hitSound;

	[HideInInspector] public bool hit = false;

	void OnTriggerEnter (Collider hitInfo) {
		if (hitInfo.name == "Ball")
		{
			//Debug.Log (this.name);
			hit = true;
			PlaySound ();

			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void PlaySound() {
		hitSound.Play ();
	}
}
