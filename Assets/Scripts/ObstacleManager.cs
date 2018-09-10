using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System.Security.Principal;
using System;

public class ObstacleManager : MonoBehaviour {

	public GameObject[] Obstacles;
	public float ObstacleDelay = 10.0f; // delay in seconds between 2 obstacle generation.

	public static ObstacleManager instance;

	private WaitForSeconds NewObstacleWait;
	private int ObstacleIndex = -1;
	private GameObject curObstacle;
	private List<GameObject> curObstacles;

	private SpawnPosition SpawnPositionScript; 

	private Coroutine ObstacleLoopCoroutine = null;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			if(gameObject == null)
			{
				Destroy(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
		SpawnPositionScript = GetComponent ("SpawnPosition") as SpawnPosition;
		curObstacles = new List<GameObject>();
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

		/*
		if (curObstacle) {
			Destroy (curObstacle);
		}
		*/

		DestroyObstacles (); // Destroy the remaining obstacles
			
		ObstacleIndex = -1;
		ObstacleLoopCoroutine = null;
	}

	private IEnumerator ObstacleLoop () {
		yield return NewObstacleWait;

		SelectObstacle ();

		CreateObstacle ();

		ObstacleLoopCoroutine = StartCoroutine (ObstacleLoop ());
	}

	public void RemoveMe(GameObject ObstacleToRemove) {		
		curObstacles.Remove (ObstacleToRemove);
	}

	private void SelectObstacle () {
		ObstacleIndex = Random.Range(0, Obstacles.Length);
	}

	private void CreateObstacle () {
		//curObstacle = Instantiate (Obstacles[ObstacleIndex], SpawnPositionScript.GetRandomPos(), Quaternion.identity);
		curObstacles.Add(Instantiate (Obstacles[ObstacleIndex], SpawnPositionScript.GetRandomPos(), Quaternion.identity));
	}

	private void DestroyObstacles() {
		int i = 0;
		foreach(GameObject go in curObstacles)
		{
			Destroy(go);
			i++;
		}
		curObstacles.Clear ();
		Debug.Log ("DestroyObstacles: number of obstacles destroyed: " + i);
	}
}
