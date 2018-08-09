using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour {

	public BallMovement ball;
	public Toggle specialBricksToggle;

	private int useSpecialBricks; // 0 or 1
	private bool useBrickerTrigger = true; // true if the "SpecialBricks" must act

	// Use this for initialization
	void Start () {

		// Resolution of the Canvas
		Screen.SetResolution (932 , 452, false, 60 );

		useSpecialBricks = PlayerPrefs.GetInt ("useSpecialBricks", 0);
		Debug.Log ("Start useSpecialBricks " + useSpecialBricks);

		useBrickerTrigger = false;
		specialBricksToggle.isOn = (useSpecialBricks == 1);
		useBrickerTrigger = true;

		ball.Go ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		SceneManager.LoadScene("Main");
	}

	public void SpecialBricks() {
		if (useBrickerTrigger) {
			useSpecialBricks = 1 - useSpecialBricks;
			Debug.Log ("Toggle useSpecialBricks " + useSpecialBricks);
			PlayerPrefs.SetInt ("useSpecialBricks", useSpecialBricks);
			PlayerPrefs.Save ();
		}
	}
}
