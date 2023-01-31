using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	public float speed;			//player speed and jump values
	public float jumpForce;
	public float hRadius;		//effect radius of horn
	public float hPower;		//pushing force of horn
	private float moveInput;

	private Rigidbody2D rb;
	public Rigidbody2D hornBlast;
	public Transform shotPoint;
	private bool facingRight = true;
	public bool shootside = false;
	public bool shootup = false;

	//checks whether player is touching ground
	[SerializeField] private bool isGrounded;
	[SerializeField] private bool isPushable;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;
	public LayerMask whatIsPushable;

	//jump checks and extra jump numbers
	private int extraJump;
	public int extraJumpValue;
	private float jumpTimeCounter;
	public float jumpTime;
	[SerializeField] private bool isJumping;

	//Sounds
	private AudioSource audioSrc;
	public AudioClip[] fluteSounds;	//array of different sound effects when using flute
	//public AudioClip[] hornSounds;	//array of different sounds for horn
	public static PlayerController Soundman;
	private int randomSound;

	public bool hasFlute;
	public bool hasHorn;

    void Start()
    {
		Soundman = this;
		extraJump = extraJumpValue;
		rb = GetComponent<Rigidbody2D>();
		audioSrc = GetComponent<AudioSource>();

		//create circle around player that effects physical objects
		Vector2 explosionPos = transform.position;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, hRadius);
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
		isPushable = Physics2D.OverlapCircle(rb.position, hRadius, whatIsPushable);

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
					if(Input.GetKeyDown(KeyCode.E) && Input.GetButton("Vertical"))
					{
						Debug.Log("Shoot Upwards");
						shootup = true;
						shootside = false;
						Instantiate(hornBlast, shotPoint.position, shotPoint.rotation);
					}
					if (Input.GetKeyDown(KeyCode.E) && !Input.GetButton("Vertical"))
          {
						  Debug.Log("Used Horn");
							shootup = false;
							shootside = true;
							Instantiate(hornBlast, shotPoint.position, shotPoint.rotation);
          }
					if(Input.GetKeyUp(KeyCode.E))
					{
						shootup = false;
						shootside = false;
					}
        }
    }

	//flip player sprite according to input direction
    void Flip()
    {
		facingRight = !facingRight;
		/*
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
		*/
		transform.Rotate(0f,180f,0f);
    }
}
