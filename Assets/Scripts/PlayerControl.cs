using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{


	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 100f;			// Amount of force added when the player jumps.
	public char cEmote = 'z';				//Nuetral Starting Emote

	private bool grounded = false;			// Whether or not the player is grounded
    bool dead = false;
	int iHighJump = 1;
	float fSpdMult = 1f;

	float fSpeed = 2f;

    public AudioClip jumpClip;
    public AudioClip[] stepClip;

    private Animator anim;

    public float selfRightingFactor;

    public bool IsDead()
    {
        return dead;
    }

    void Start()
    {
        anim = GameObject.Find("Animation").GetComponent<Animator>();
    }


	void Update()
	{
        if (dead)
        {
            var emiter = GameObject.Find("PlayerDeathEmiter");
            emiter.transform.position = transform.position;
            emiter.GetComponent<ParticleSystem>().Emit(20);
            gameObject.SetActive (false);

			Invoke ("Restart", 3);


        }
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");

			if (anim == null)
			{
				if (GameObject.Find("Animation") == null)
				{
					
					anim = GameObject.Find("Animation").GetComponent<Animator>();
					if (anim == null)
						return;
				}
			}


			anim.SetFloat ("SpeedX", Mathf.Abs (Input.GetAxis ("Horizontal")));
				
			transform.position += transform.right * Input.GetAxis("Horizontal")* fSpeed * fSpdMult * Time.deltaTime;
		
			// If the input is moving the player right and the player is facing left...
			if(h > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(h < 0 && facingRight)
				// ... flip the player.
				Flip();
			
	}

    public void Jump()
    {
        if (grounded)
        {
			Vector2 speed = rigidbody2D.velocity;

            if (!((speed.y > 0.01) || speed.y < -0.01))
            {
                // Jump
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);  
                rigidbody2D.AddForce(new Vector2(0f, jumpForce * iHighJump));

                anim.SetBool("Grounded", false);
                anim.SetTrigger("Jump");
                grounded = false;
            }
        }
    }
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Check for deadly collision
        if (coll.collider.tag == "Deadly")
        {
            dead = true;
            print("You are dead");
        }
		

		if(coll.collider.tag == "Tramp")
		{
			iHighJump = 2;
		}

		if(coll.collider.tag == "Decel")
		{
			fSpdMult = 0.5f;
		}

		if(coll.collider.tag == "Accel")
		{
			fSpdMult = 2f;
		}

		if(coll.collider.tag == "ForceH")
		{
			cEmote = 'H';
			GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.9f, 0.9f, 0.1f));
		}

		if(coll.collider.tag == "ForceS")
		{
			cEmote = 'S';
			GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.1f, 0.1f, 0.9f));
		}

		if(coll.collider.tag == "ForceF")
		{
			cEmote = 'F';
			GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.5f, 0.1f, 0.9f));
		}

		if(coll.collider.tag == "ForceA")
		{
			cEmote = 'A';
			GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.9f, 0.1f, 0.1f));
		}

		if(coll.collider.tag == "ForceD")
		{
			cEmote = 'D';
			GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.1f, 0.9f, 0.1f));
		}

		if(coll.collider.tag == "ForceU")
		{
			cEmote = 'U';
			GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(1f, 0.6f, 0.1f));
		}

		if(coll.collider.tag == "Finish")
		{
			Application.LoadLevel ("Victory");
		}

        if ((coll.transform.position.y < this.transform.position.y))
        {
            if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))

            {
                anim.SetBool("Grounded", true);
                grounded = true;
                print("Layer Ground");
            }
            else
                print("Layer Not ground");
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.tag == "Boundary")
        {
            print("Left the level");
        }

		if(coll.collider.tag == "Tramp")
		{
			iHighJump = 1;
		}

		if(coll.collider.tag == "Decel")
		{
			fSpdMult = 1;
		}

		if(coll.collider.tag == "Accel")
		{
			fSpdMult = 1;
		}
    }

    /*public void ActivateAbility(int i)
    {
		switch(i)
		{
		case 1:
			cEmote = 'A';
            GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.9f, 0.1f, 0.1f));
			break;
		case 2:
			cEmote = 'S';
            GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.1f, 0.1f, 0.9f));
			break;
		case 3:
			cEmote = 'H';
            GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.9f, 0.9f, 0.1f));
			break;
		case 4:
			cEmote = 'D';
            GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.1f, 0.9f, 0.1f));
			break;
		case 5:
			cEmote = 'U';
            GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(1f, 0.6f, 0.1f));
			break;
		case 6:
			cEmote = 'F';
            GameObject.Find("Main Camera").GetComponent<CameraControler>().SetClearColor(new Color(0.5f, 0.1f, 0.9f));
			break;
		}
        print("Ability Activated " + i);
    }*/

    public void OnStep(int step)
    {
        AudioSource.PlayClipAtPoint(stepClip[step], transform.position);
    }

	void Restart()
	{
		Application.LoadLevel ("testscene");
	}
}