using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float speed = 12f;
    public float gravity = -9.81f;

    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioClip footstepSound;
    public AudioClip jumpingSound;

    private AudioSource audioSource;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("IsJumping", false);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(x) > 0.01f || Mathf.Abs(z) > 0.01f;
        if (isMoving)
        {
            animator.SetBool("IsWalking", true);
            PlayFootstepSound();
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", true);
            audioSource.Stop();
        }

        if (Input.anyKeyDown && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.E)))
        {
            animator.SetBool("IsWalking", true);
            PlayFootstepSound();
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsJumping", true);
            audioSource.PlayOneShot(jumpingSound);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayFootstepSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}