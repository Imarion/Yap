﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

	public BallMovement ball;

	// Use this for initialization
	void Start () {
		ball.Go ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		SceneManager.LoadScene("Main");
	}
}