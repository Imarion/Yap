using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	public GameObject[] Obstacles;
	public int ObstacleDelay = 10; // delay in seconds between 2 obstacle generation.

	private WaitForSeconds NewObstacleWait;
	private int ObstacleIndex = -1;
	private GameObject curObstacle;

	private Coroutine ObstacleLoopCoroutine = null;

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
		ObstacleLoopCoroutine = StartCoroutine (ObstacleLoop ());
	}

	public void StopObstacleLoop() {

		StopCoroutine (ObstacleLoopCoroutine);

		if (curObstacle) {
			Destroy (curObstacle);
		}
			
		ObstacleIndex = -1;
		ObstacleLoopCoroutine = null;
	}

	private IEnumerator ObstacleLoop () {
		yield return NewObstacleWait;

		SelectObstacle ();

		CreateObstacle ();

		ObstacleLoopCoroutine = StartCoroutine (ObstacleLoop ());
	}

	private void SelectObstacle () {
		ObstacleIndex = Random.Range(0, Obstacles.Length);
	}

	private void CreateObstacle () {
		curObstacle = Instantiate (Obstacles[ObstacleIndex]);
	}
}
