using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public RacquetManager[] players;
	public GameObject racquetPrefab; 		// reference to the Prefab the player will control
	public BallMovement ball;
	public float StartDelay = 3f;       // The delay between the start of RoundStarting and RoundPlaying phases.
	public float EndDelay = 3f;         // The delay when there is a winner; mostly useful to let the winning sound play to the end.
	public int numRoundsToWin = 3;
	public AudioSource gameWinSound;

	private WaitForSeconds StartWait;         // Used to have a delay whilst the round starts.
	private WaitForSeconds EndWait;         // Used to have a delay whilst the round ends.
	private RacquetManager gameWinner;
	private int roundWinner = -1;

	// Use this for initialization
	void Start () {
		StartWait = new WaitForSeconds (StartDelay);
		EndWait = new WaitForSeconds (EndDelay);

		SpawnRacquets ();

		StartCoroutine (GameLoop ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SpawnRacquets() {
		for (int i = 0; i < players.Length; i++) {
			players [i].instance = Instantiate (racquetPrefab, players [i].spawnPoint.position, players [i].spawnPoint.rotation) as GameObject;
			players [i].playerNumber = i + 1;
			players [i].Setup ();
		}
	}

	private void Restart() {
		Debug.Log ("Game manager restart");	

		//ball.Go ();
			
	}

	private IEnumerator GameLoop () {
		yield return StartCoroutine (RoundStarting ());

		yield return StartCoroutine (RoundPlaying ());

		yield return StartCoroutine (RoundEnding ());

		if (gameWinner != null) { // we have a winner
			//Application.LoadLevel (Application.loadedLevel);
			SceneManager.LoadScene("Main");
		} else {
			StartCoroutine (GameLoop ());
		}
	}

	private IEnumerator RoundStarting () {

		Debug.Log ("RoundStarting");
		ball.Reset ();
		roundWinner = -1;

		yield return StartWait;
	}

	private IEnumerator RoundPlaying () {
		Debug.Log ("RoundPlaying");

		ball.Go ();

		while (!CheckWallHit()) {
			yield return null;
		}			
	}

	private IEnumerator RoundEnding () {
		gameWinner = GetGameWinner ();

		if (gameWinner != null) {
			String wintext = "Player " + gameWinner.playerNumber.ToString () + " wins";

			for (int i = 0; i < players.Length; i++)
			{
				if (players [i].wall.hitSound.isPlaying) {
					players [i].wall.hitSound.Stop ();
				}
					
			}
			ball.Reset ();
			gameWinSound.Play ();
			Debug.Log (wintext);
			yield return EndWait;
		}	

		yield return null;
	}

	private bool CheckWallHit() {
		for (int i = 0; i < players.Length; i++) {
			if (players [i].wall.hit) {
				roundWinner = i;
				players [i].wall.hit = false;
				players [roundWinner].Goal ();
				Debug.Log (roundWinner);
			}
		}
		return roundWinner != -1;
	}

	private RacquetManager GetGameWinner()
	{
		// Go through all the racquets...
		for (int i = 0; i < players.Length; i++)
		{
			// ... and if one of them has enough rounds to win the game, return it.
			if (players[i].score >= numRoundsToWin)
				return players[i];
		}

		// If no player has enough rounds to win, return null.
		return null;
	}
}
