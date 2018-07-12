using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Policy;

public class BallMovement : MonoBehaviour {

	public float speed = 12f;
	private Vector3 vel;

	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		Vector3 spawndir = Random.onUnitSphere;
		//Vector3 spawndir = new Vector3(1.0f, 0.0f, 0.0f);
		spawndir.z = 0;
		rb.velocity = spawndir * speed;
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		Move ();
	}

	private void Move()
	{
		vel = rb.velocity; // Record the velocity to have the la&st one before the collision; otherwise at the time of collision vel.x = 0
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.CompareTag("Player")){
			//calculate angle
			float y = launchAngle(transform.position,
				collision.transform.position,
				collision.collider.bounds.size.y);

			Vector2 d = Vector2.zero;

			//set angle and speed
			if (vel.x > 0) {
				d = new Vector2 (-1, y).normalized;
			} else {
				d = new Vector2 (1, y).normalized;
			}
			rb.velocity = d * speed;

			Debug.Log("After collision " + rb.velocity);
		}
	}

	//calculates the angle the ball hits the paddle at
	float launchAngle(Vector2 ball, Vector2 paddle, float paddleHeight) {
		//return (ball.y - paddle.y) / paddleHeight;
		return (ball.y - paddle.y) / 1;
	}
}
