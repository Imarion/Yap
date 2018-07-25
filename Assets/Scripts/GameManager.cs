using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GameManager : MonoBehaviour {

	public RacquetManager[] players;
	public GameObject racquetPrefab; 		// reference to the Prefab the player will control
	public BallMovement ball;
	public float StartDelay = 3f;         // The delay between the start of RoundStarting and RoundPlaying phases.

	private int winnerPlayer = -1;
	private WaitForSeconds StartWait;         // Used to have a delay whilst the round starts.

	// Use this for initialization
	void Start () {
		StartWait = new WaitForSeconds (StartDelay);

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

		//yield return StartCoroutine (RoundEnding ());

		if (winnerPlayer != -1) { // still no winner
			StartCoroutine (GameLoop ());
		}
	}

	private IEnumerator RoundStarting () {

		Debug.Log ("RoundStarting");
		ball.Reset ();
		winnerPlayer = -1;

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
		yield return null;
	}

	private bool CheckWallHit() {
		for (int i = 0; i < players.Length; i++) {
			if (players [i].wall.hit) {
				winnerPlayer = i;
				players [i].wall.hit = false;
				players [winnerPlayer].Goal ();
				Debug.Log (winnerPlayer);
			}
		}
		return winnerPlayer != -1;
	}
}
