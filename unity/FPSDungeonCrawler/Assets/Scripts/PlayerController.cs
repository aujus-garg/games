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

    private bool fTripleTap = false;
    private float fTripleTapTime = 0;
    private bool fDoubleTap = false;
    private float fDoubleTapTime = 0;
    private bool bTripleTap = false;
    private float bTripleTapTime = 0;
    private bool bDoubleTap = false;
    private float bDoubleTapTime = 0;
    private bool rTripleTap = false;
    private float rTripleTapTime = 0;
    private bool rDoubleTap = false;
    private float rDoubleTapTime = 0;
    private bool lTripleTap = false;
    private float lTripleTapTime = 0;
    private bool lDoubleTap = false;
    private float lDoubleTapTime = 0;
    private float vDash = 0;
    private float hDash = 0;
    public float dashPower;
    public float tapResetDelay = 0.25f;
    private float vDashTime;
    private float hDashTime;
    public float dashLength;
    public float dashCooldown;

    public float trampPower = 1;

    void Start()
    {
        startPos = transform.position;

        PlayerPrefs.SetInt("continueScene", SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        dashSensor();

        if (Time.time - vDashTime > dashLength)
        {
            vDash = 0;
        }
        if (Time.time - hDashTime > dashLength)
        {
            hDash = 0;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 dash = transform.forward * vDash + transform.right * hDash;

        if (move.sqrMagnitude > 1)
        {
            move /= move.magnitude;
        }

        if (dash.sqrMagnitude > 1)
        {
            dash /= dash.magnitude;
        }

        controller.Move((move + dash * dashPower) * speed * Time.deltaTime);

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

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

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

    void dashSensor()
    {
        if (Time.time - vDashTime > dashCooldown)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {

                if (fTripleTap)
                {
                    if ((Time.time - fTripleTapTime) < tapResetDelay)
                    {
                        fTripleTapTime = 0f;
                    }
                    fTripleTap = false;
                }
                if (fDoubleTap && !fTripleTap)
                {
                    if ((Time.time - fDoubleTapTime) < tapResetDelay)
                    {
                        vDash = 1;
                        vDashTime = Time.time;
                        fDoubleTapTime = 0f;
                        fTripleTap = true;
                        fTripleTapTime = Time.time;
                    }
                    fDoubleTap = false;
                }
                if (!fDoubleTap && !fTripleTap)
                {
                    fDoubleTap = true;
                    fDoubleTapTime = Time.time;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (bTripleTap)
                {
                    if ((Time.time - bTripleTapTime) < tapResetDelay)
                    {
                        bTripleTapTime = 0f;
                    }
                    bTripleTap = false;
                }
                if (bDoubleTap && !bTripleTap)
                {
                    if ((Time.time - bDoubleTapTime) < tapResetDelay)
                    {
                        vDash = -1;
                        vDashTime = Time.time;
                        bDoubleTapTime = 0f;
                        bTripleTap = true;
                        bTripleTapTime = Time.time;
                    }
                    bDoubleTap = false;
                }
                if (!bDoubleTap && !bTripleTap)
                {
                    bDoubleTap = true;
                    bDoubleTapTime = Time.time;
                }
            }
        }
        if (Time.time - hDashTime > dashCooldown)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (lTripleTap)
                {
                    if ((Time.time - lTripleTapTime) < tapResetDelay)
                    {
                        lTripleTapTime = 0f;
                    }
                    lTripleTap = false;
                }
                if (lDoubleTap && !lTripleTap)
                {
                    if ((Time.time - lDoubleTapTime) < tapResetDelay)
                    {
                        hDash = -1;
                        hDashTime = Time.time;
                        lDoubleTapTime = 0f;
                        lTripleTap = true;
                        lTripleTapTime = Time.time;
                    }
                    lDoubleTap = false;
                }
                if (!lDoubleTap && !lTripleTap)
                {
                    lDoubleTap = true;
                    lDoubleTapTime = Time.time;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (rTripleTap)
                {
                    if ((Time.time - rTripleTapTime) < tapResetDelay)
                    {
                        rTripleTapTime = 0f;
                    }
                    rTripleTap = false;
                }
                if (rDoubleTap && !rTripleTap)
                {
                    if ((Time.time - rDoubleTapTime) < tapResetDelay)
                    {
                        hDash = 1;
                        hDashTime = Time.time;
                        rDoubleTapTime = 0f;
                        rTripleTap = true;
                        rTripleTapTime = Time.time;
                    }
                    rDoubleTap = false;
                }
                if (!rDoubleTap && !rTripleTap)
                {
                    rDoubleTap = true;
                    rDoubleTapTime = Time.time;
                }
            }
        }
    }

    public void trampJump()
    {
        velocity.y = Mathf.Sqrt(trampPower * -2f * gravity);
    }
}

