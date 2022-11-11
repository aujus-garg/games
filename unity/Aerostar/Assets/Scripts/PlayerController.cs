using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public GameObject respawnPoint;

    public float bottomBound;
    private Vector3 startPos;

    private float jumpTimeCounter;
    public float jumpTime;
    private float jumpsLeft;
    public float jumpAmount;

    public bool wallClimbEnabled;
    public GameObject wallClimbCamera;
    private bool notClinging = true;

    void Start()
    {
        startPos = transform.position;

        PlayerPrefs.SetInt("continueScene", SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        notClinging = true;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (move.sqrMagnitude > 1)
        {
            move /= move.magnitude;
        }

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            jumpsLeft = jumpAmount;
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && jumpsLeft > 0)
        {
            if (jumpTimeCounter > 0)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimeCounter = jumpTime;
            jumpsLeft--;
        }

        if (notClinging)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        if (transform.position.y < bottomBound)
        {
            if (respawnPoint != null)
            {
                transform.position = respawnPoint.transform.position + transform.up;
            }
            else
            {
                transform.position = startPos;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isGrounded && hit.normal.y < .1f && wallClimbEnabled && hit.gameObject.CompareTag("Wall"))
        {
            if (Input.GetKey(KeyCode.Space) && wallClimbCamera.transform.rotation.eulerAngles.x > 260 && wallClimbCamera.transform.rotation.eulerAngles.x < 280)
            {
                notClinging = true;
                velocity.y = Mathf.Sqrt((jumpHeight * -2f * gravity));
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                notClinging = false;
                velocity.y = 0;
            }
        }
    }
}
