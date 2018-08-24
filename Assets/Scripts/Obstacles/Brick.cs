using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public float Lifetime = 3;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, Lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
