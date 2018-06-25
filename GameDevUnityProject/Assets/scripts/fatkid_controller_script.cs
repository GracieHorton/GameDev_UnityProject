using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fatkid_controller_script : MonoBehaviour {
	public float maxSpeed;
	bool moving;
	public bool facingRight = true;
	Rigidbody2D rb;
	Animator anim;

	// Use this for initialization
	void Start () {
		maxSpeed = 30f;
		moving = false;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");
		if (move == 0) {
			anim.SetBool ("moving", false);
		} else {
			anim.SetBool ("moving", true);
		}
		rb.velocity = new Vector2 (move * maxSpeed, rb.velocity.y);

		if (move > 0 && !facingRight) {
			flip ();
		}else if(move < 0 && facingRight) {
			flip();
		}
	}

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
