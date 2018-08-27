using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting;

public class SpawnPosition : MonoBehaviour {

	public Vector3 center, size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 GetRandomPos() {
		return (center + new Vector3 (Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0));
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color (0.8f, 0.0f, 0.0f, 0.5f);
		Gizmos.DrawCube (center, size);
	}
}
