using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacquetMovement : MonoBehaviour {

	public int playerNumber = 1;         
	public float speed = 12f;
	public float boundY = 5f;

	private string movementAxisName;
	private float movementInputValue;
	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		movementAxisName = "Vertical" + playerNumber;
	}

	private void Update()
	{
		movementInputValue =Input.GetAxis(movementAxisName);
	}

	private void FixedUpdate()
	{
		Move ();
	}

	private void Move()
	{
		// Adjust the position of the tank based on the player's input.
		Vector3 movement = transform.up * movementInputValue * speed * Time.deltaTime;

		if (Mathf.Abs (GetComponent<Rigidbody>().position.y + movement.y) < boundY) {
			rb.MovePosition (rb.position + movement);
		}
	}
}
