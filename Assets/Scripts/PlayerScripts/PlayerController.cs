using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	private float moveInput;

	private Rigidbody2D rb;
	private bool facingRight = true;

	[SerializeField]
	private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

	private int extraJump;
	public int extraJumpValue;
	private float jumpTimeCounter;
	public float jumpTime;
	[SerializeField]
	private bool isJumping;

	private AudioSource audioSrc;
	[SerializeField]public AudioClip[] fluteSounds;
	public static PlayerController Soundman;
	[SerializeField] private int randomSound;

	public bool hasFlute;

    void Start()
    {
		Soundman = this;
		extraJump = extraJumpValue;
		rb = GetComponent<Rigidbody2D>();
		audioSrc = GetComponent<AudioSource>();
    }
	public void PlayFluteSound()
    {
		randomSound = Random.Range(0, 2);
		audioSrc.PlayOneShot(fluteSounds[randomSound]);
    }

    private void FixedUpdate()
    {
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
		if (isGrounded == true)
			extraJump = extraJumpValue;
		if (hasFlute == true)
		{
			if (Input.GetButtonDown("Jump") && extraJump > 0 && isGrounded == true)
			{
				isJumping = true;
				jumpTimeCounter = jumpTime;
				rb.velocity = Vector2.up * jumpForce;
			}
			else if (Input.GetButtonDown("Jump") && extraJump > 0)
			{
				PlayFluteSound();
				isJumping = true;
				jumpTimeCounter = jumpTime;
				rb.velocity = Vector2.up * jumpForce;
				extraJump--;
			}
			if (Input.GetButton("Jump") && isJumping == true)
			{
				if (jumpTimeCounter > 0)
				{
					rb.velocity = Vector2.up * jumpForce;
					jumpTimeCounter -= Time.deltaTime;
				}
				else
					isJumping = false;
			}
			if (Input.GetButtonUp("Jump"))
			{
				isJumping = false;
			}
		}
		else if (hasFlute == false)
		{
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
    }

    void Flip()
    {
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
    }
}
