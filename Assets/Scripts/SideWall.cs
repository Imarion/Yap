using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SideWall : MonoBehaviour {

	void OnTriggerEnter (Collider hitInfo) {
		Debug.Log ("Trigger enter");
		if (hitInfo.name == "Ball")
		{
			Debug.Log ("Ball");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
