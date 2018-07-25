using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Policy;

public class BallMovement : MonoBehaviour {

	public float speed = 12f;
	private Vector3 vel;

	private Rigidbody rb;
	private Vector2 spawndir = Vector2.zero;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		//Vector3 spawndir = new Vector3(1.0f, 0.0f, 0.0f);
		//Vector3 spawndir = Random.onUnitSphere;
		//spawndir.z = 0;
		//Invoke("Go", 3);
	}

	private void Update()
	{
		
	}

	private void FixedUpdate()
	{
		Move ();
	}

	public void Reset()
	{
		rb.position = Vector3.zero;
		rb.velocity = Vector3.zero;
	}		

	public void Go()
	{
		float cos = 0;
		while (Mathf.Abs(cos) < 0.707) { // 0.707 = sqrt(2) / 2
			spawndir = Random.insideUnitCircle.normalized;
			cos = Vector2.Dot (spawndir, new Vector2 (1, 0));
			//Debug.Log (cos);
		}

		//Debug.Log (rb.velocity);

		rb.velocity = spawndir * speed;
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

			//Debug.Log("After collision " + rb.velocity);
		}
	}

	//calculates the angle the ball hits the paddle at
	float launchAngle(Vector2 ball, Vector2 paddle, float paddleHeight) {
		//return (ball.y - paddle.y) / paddleHeight;
		return ball.y - paddle.y;
	}
}
