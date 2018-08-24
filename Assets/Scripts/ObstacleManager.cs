using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	public GameObject[] Obstacles;
	public int ObstacleDelay = 10; // delay in seconds between 2 obstacle generation.

	private WaitForSeconds NewObstacleWait;
	private int ObstacleIndex = -1;

	// Use this for initialization
	void Start () {
		NewObstacleWait = new WaitForSeconds (ObstacleDelay);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ImThere() {
		Debug.Log ("Coucou");
	}

	public void StartObstacleLoop() {
		StartCoroutine (ObstacleLoop ());
	}

	public void StopObstacleLoop() {

		if (Obstacles [ObstacleIndex] != null) {
			Destroy (Obstacles [ObstacleIndex]);
		}
			
		ObstacleIndex = -1;

		StopAllCoroutines ();
	}

	private IEnumerator ObstacleLoop () {
		yield return NewObstacleWait;

		SelectObstacle ();

		CreateObstacle ();

		StartCoroutine (ObstacleLoop ());
	}

	private void SelectObstacle () {
		ObstacleIndex = Random.Range(0, Obstacles.Length);
	}

	private void CreateObstacle () {
		Instantiate (Obstacles[ObstacleIndex]);
	}
}
