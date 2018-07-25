using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SideWall : MonoBehaviour {

	[HideInInspector] public bool hit = false;

	void OnTriggerEnter (Collider hitInfo) {
		if (hitInfo.name == "Ball")
		{
			//Debug.Log (this.name);
			hit = true;

			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
