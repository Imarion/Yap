using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class RacquetManager {

	public Material playerMaterial;
	public Transform spawnPoint;

	[HideInInspector] public int playerNumber = 1;
	[HideInInspector] public GameObject instance;

	private RacquetMovement racquetMovement;

	public void Setup() {
		racquetMovement = instance.GetComponent <RacquetMovement>();

		racquetMovement.playerNumber = playerNumber;

		MeshRenderer meshr = instance.GetComponent<MeshRenderer> ();
		meshr.material = playerMaterial;
	}
}
