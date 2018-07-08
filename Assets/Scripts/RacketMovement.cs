using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour {

	public int m_PlayerNumber = 1;         
	public float m_Speed = 12f;            

	private string m_MovementAxisName;
	private float m_MovementInputValue;
	private Rigidbody m_Rigidbody;         

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		m_MovementAxisName = "Vertical" + m_PlayerNumber;
	}

	private void Update()
	{
		m_MovementInputValue =Input.GetAxis(m_MovementAxisName);
	}

	private void FixedUpdate()
	{
		Move ();
	}

	private void Move()
	{
		// Adjust the position of the tank based on the player's input.
		Vector3 movement = transform.up * m_MovementInputValue * m_Speed * Time.deltaTime;

		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
	}
}
