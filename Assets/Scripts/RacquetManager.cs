using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RacquetManager {

	public Material playerMaterial;
	public Transform spawnPoint;
	public SideWall wall; // This reference is used in the Game Manager.
	public Text scoreText;

	[HideInInspector] public int playerNumber = 1;
	[HideInInspector] public GameObject instance;
	[HideInInspector] public uint score = 0;

	private RacquetMovement racquetMovement;


	public void Setup() {
		racquetMovement = instance.GetComponent <RacquetMovement>();

		racquetMovement.playerNumber = playerNumber;

		MeshRenderer meshr = instance.GetComponent<MeshRenderer> ();
		meshr.material = playerMaterial;
	}

	public void Goal() {
		score++;
		scoreText.text = score.ToString();
	}
}
