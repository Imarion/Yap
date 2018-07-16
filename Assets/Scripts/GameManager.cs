using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public RacquetManager[] players;
	public GameObject racquetPrefab; 		// reference to the Prefab the player will control

	// Use this for initialization
	void Start () {
		SpawnRacquets ();
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
}
