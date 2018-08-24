using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public RacquetManager[] players;
	public GameObject racquetPrefab; 	// reference to the Prefab the player will control
	public BallMovement ball;
	public float StartDelay = 3f;       // The delay between the start of RoundStarting and RoundPlaying phases.
	public float EndDelay = 3f;         // The delay when there is a winner; mostly useful to let the winning sound play to the end.
	public int numRoundsToWin = 3;
	public AudioSource gameWinSound;
	public Text messageText;

	public ObstacleManager om, omClone;

	private WaitForSeconds StartWait;       // Used to have a delay whilst the round starts.
	private WaitForSeconds EndWait;         // Used to have a delay whilst the round ends.
	private RacquetManager gameWinner;
	private RacquetManager roundWinner;
	private int roundNumber = 0;

	// Use this for initialization
	void Start () {
		StartWait = new WaitForSeconds (StartDelay);
		EndWait = new WaitForSeconds (EndDelay);

		if (PlayerPrefs.GetInt ("useSpecialBricks", 0) == 1) {
			omClone = Instantiate (om);
		}

		SpawnRacquets ();

		// Resolution of the Canvas
		Screen.SetResolution (932 , 452, false, 60 );

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

		omClone.StartObstacleLoop ();

		yield return StartCoroutine (RoundPlaying ());

		yield return StartCoroutine (RoundEnding ());

		if (gameWinner != null) { // we have a winner
			//Application.LoadLevel (Application.loadedLevel);
			SceneManager.LoadScene("StartMenu");
		} else {
			StartCoroutine (GameLoop ());
		}
	}

	private IEnumerator RoundStarting () {

		Debug.Log ("RoundStarting");
		ball.Reset ();
		roundWinner = null;

		roundNumber++;
		messageText.text = "ROUND " + roundNumber;

		yield return StartWait;
	}

	private IEnumerator RoundPlaying () {
		Debug.Log ("RoundPlaying");
		messageText.text = string.Empty;

		ball.Go ();

		while (!CheckWallHit()) {
			yield return null;
		}			
	}

	private IEnumerator RoundEnding () {
		gameWinner = GetGameWinner ();

		if (gameWinner != null) {
			for (int i = 0; i < players.Length; i++)
			{
				if (players [i].wall.hitSound.isPlaying) {
					players [i].wall.hitSound.Stop ();
				}
					
			}
			gameWinSound.Play ();
			//yield return EndWait;
		}

		Debug.Log (EndMessage());
		ball.Reset ();
		messageText.text = EndMessage ();

		om.StopObstacleLoop ();

		yield return EndWait;

	}

	private bool CheckWallHit() {
		for (int i = 0; i < players.Length; i++) {
			if (players [i].wall.hit) {
				roundWinner = players[i];
				players [i].wall.hit = false;
				roundWinner.Goal ();
				Debug.Log (roundWinner.playerNumber);
			}
		}
		return roundWinner != null;
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

	// Returns a string message to display.
	private string EndMessage()
	{
		// By default when a round ends there are no winners so the default end message is a draw.
		string message = "DRAW!";

		// If there is a winner then change the message to reflect that.
		if (roundWinner != null)
			message = roundWinner.coloredPlayerText + " WINS THE ROUND!";

		// Add some line breaks after the initial message.
		message += "\n\n";

		// Go through all the tanks and add each of their scores to the message.
		for (int i = 0; i < players.Length; i++)
		{
			//message += "Player " + players[i].playerNumber.ToString() + ": " + players[i].score + " POINTS\n";
			message += players[i].coloredPlayerText + ": " + players[i].score + " POINT(S)\n";
		}

		// If there is a game winner, change the entire message to reflect that.
		if (gameWinner != null)
			message = gameWinner.coloredPlayerText + " WINS THE GAME!";

		return message;
	}

}
