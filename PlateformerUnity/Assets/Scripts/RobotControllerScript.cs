﻿using UnityEngine;
using System.Collections;

public class RobotControllerScript : MonoBehaviour {


	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	bool doubleJump = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if (grounded) {
			doubleJump  = false;
		}

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


		float move = Input.GetAxis("Horizontal");

		anim.SetFloat ("Speed",Mathf.Abs(move));

		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight) {
			Flip();
		}else if(move <0 && facingRight){
			Flip();
		}
	}

	void Update(){
		if ((grounded  || !doubleJump) && ((Input.GetKeyDown (KeyCode.UpArrow))||Input.GetKeyDown(KeyCode.Joystick1Button0))) {
			anim.SetBool("Ground",false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
			if(!doubleJump && !grounded){
				doubleJump = true;
			}
		}
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
