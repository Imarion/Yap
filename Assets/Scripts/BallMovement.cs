using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Policy;
using System.IO;

public class BallMovement : MonoBehaviour {

	public float speed = 6f;
	public float acceleration = 1.1f;
	public AudioSource racquetHitSound;

	private Vector3 vel;
	private Rigidbody rb;
	private Vector2 spawndir = Vector2.zero;
	private TrailRenderer trailr;
	private float trailtime = 0.2f;
	private float curspeed;

	private void Awake()
	{
		trailr = GetComponent<TrailRenderer>();
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		curspeed = speed;
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
		trailtime = trailr.time;
		trailr.time = -1; // cancel trail effect when ball is set at <0, 0, 0>; trailr.Clear(); or enabled false do not work well.
		rb.position = Vector3.zero;
		rb.velocity = Vector3.zero;
		curspeed = speed;
		Invoke ("ReseTrail", 0.1f);  // wait few frames before re activating the trail
	}		

	public void ReseTrail() {
		trailr.time = trailtime;
	}

	public void Go()
	{
		float cos = 0;
		while (Mathf.Abs(cos) < 0.707) { // 0.707 = sqrt(2) / 2
			spawndir = Random.insideUnitCircle.normalized;
			cos = Vector2.Dot (spawndir, new Vector2 (1, 0));
		}

		rb.velocity = spawndir * speed;
	}

	private void Move()
	{
		vel = rb.velocity; // Record the velocity to have the last one before the collision; otherwise at the time of collision vel.x = 0
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

			if (curspeed < 9.6) // after 5 hits speed = 9.66306, after 6 hits speed = 10.629366
				curspeed *= acceleration;
			rb.velocity = d * curspeed;

			PlaySound();
		}
	}

	void PlaySound () {
		racquetHitSound.Play ();
	}

	//calculates the angle the ball hits the paddle at
	float launchAngle(Vector2 ball, Vector2 paddle, float paddleHeight) {
		//return (ball.y - paddle.y) / paddleHeight;
		return ball.y - paddle.y;
	}
}
