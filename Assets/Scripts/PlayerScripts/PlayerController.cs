using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	public float speed;			//player speed and jump values
	public float jumpForce;
	private float moveInput;	//value that determines player direction

	private Rigidbody2D rb;			//this rigidbody called on start
	public Rigidbody2D hornBlast;	//the prefab of horn projectile
	public Transform shotPoint;		//position of where projectiles will instantiate
	private bool facingRight = true;

	//checks whether player is touching ground
	[SerializeField] private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

	//jump checks and extra jump numbers
	private int extraJump;
	public int extraJumpValue;
	private float jumpTimeCounter;
	public float jumpTime;
	[SerializeField] private bool isJumping;

	//Sounds
	private AudioSource audioSrc;
	public AudioClip[] fluteSounds;		//array of different sound effects when using flute
	//public AudioClip[] hornSounds;	//array of different sounds for horn. NOT YET IMPLEMENTED
	public static PlayerController Soundman;
	private int randomSound;

	public bool hasFlute;		//These must be adjusted later so that only 1 instrument is active at a time
	public bool hasHorn;

    void Start()
    {
		Soundman = this;
		extraJump = extraJumpValue;
		rb = GetComponent<Rigidbody2D>();
		audioSrc = GetComponent<AudioSource>();
    }
	//plays a random sound within the flutesound array
	public void PlayFluteSound()
    {
		randomSound = Random.Range(0, 2);
		audioSrc.PlayOneShot(fluteSounds[randomSound]);
    }

    private void FixedUpdate()
    {
		//Check using small circle on player feet to check for ground layermask
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

		moveInput = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

		if (facingRight == false && moveInput > 0)
        {
			Flip();
        }
		else if (facingRight == true && moveInput < 0)
        {
			Flip();
        }
    }

    private void Update()
    {
		if (isGrounded == true) //touching the ground will reset jump amount
			extraJump = extraJumpValue;
		if (hasFlute == true)
		{
			//initialize jump when on ground
			if (Input.GetButtonDown("Jump") && extraJump > 0 && isGrounded == true)
			{
				isJumping = true;
				jumpTimeCounter = jumpTime;
				rb.velocity = Vector2.up * jumpForce;
			}
			//while in the air with flute, perform double jump and sound
			else if (Input.GetButtonDown("Jump") && extraJump > 0)
			{
				PlayFluteSound();
				isJumping = true;
				jumpTimeCounter = jumpTime;
				rb.velocity = Vector2.up * jumpForce;
				extraJump--;
			}
			//player can do a short or higher jump depending on if jump button is held down.
			if (Input.GetButton("Jump") && isJumping == true)
			{
				//jump height is decided on a timer. if timer is above 0, then player can still rise higher
				if (jumpTimeCounter > 0)
				{
					rb.velocity = Vector2.up * jumpForce;
					jumpTimeCounter -= Time.deltaTime;
				}
				else
					isJumping = false;
			}
			//when player lets go of jump button(even if jump timer != 0), then jump is stopped
			if (Input.GetButtonUp("Jump"))
			{
				isJumping = false;
			}
		}
		//jump is different when flute is not equipped.
		else if (hasFlute == false)
		{
			//cannot double jump and jump is shorter.
			if (Input.GetButtonDown("Jump") && isGrounded == true)
			{
				isJumping = true;
				rb.velocity = Vector2.up * jumpForce * 1.2f;
			}
			if (Input.GetButtonUp("Jump"))
			{
				isJumping = false;
			}
		}
		if (hasHorn)
        {
			//This is called when player presses 'E' while holding the up/w key.
			if(Input.GetKeyDown(KeyCode.E) && Input.GetButton("Vertical"))
			{
				//This should cause projectile to shoot upwards. Useful for falling objects or puzzles that are above the player
				Debug.Log("Shoot Upwards");
				Instantiate(hornBlast, shotPoint.position, Quaternion.Euler(new Vector3(0,0,90)));
			}
			//This is called when just pressing 'E' and not holding the up/w key.
			if (Input.GetKeyDown(KeyCode.E) && !Input.GetButton("Vertical"))
			{
				//This shoots a horizontal projectile in the direction the player is facing.
				Debug.Log("Used Horn");
				Instantiate(hornBlast, shotPoint.position, shotPoint.rotation);
			}
        }
    }

	//flip player sprite according to input direction
    void Flip()
    {
		facingRight = !facingRight;

		//This was the old script to flip player sprite
		//Now changed to flip the character as a whole which includes the 
		//position of the spot where projectiles will be fired from
		/*
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
		*/
		transform.Rotate(0f,180f,0f);
    }
}
