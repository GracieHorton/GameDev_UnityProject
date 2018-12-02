using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fatkid_controller_script : MonoBehaviour {
	public float maxSpeed;
	bool moving;
    bool grounded = false;
	bool jumping = true;
	public bool facingRight = true;
    public float jumpForce = 200.0f;
    public float footRayLength = 5.0f;
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

        

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, footRayLength, LayerMask.GetMask("ground"));

        Debug.DrawRay(transform.position, -Vector2.up * footRayLength, Color.red);
        if (hit.collider)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

		if (Input.GetButton ("Jump")) {
            print(grounded);
			if (!jumping && grounded) {
				rb.AddForce (new Vector2 (0, jumpForce));
                jumping = true;
            }
        } else {
			jumping = false;
		}
	}

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
