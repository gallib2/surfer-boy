using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static event Action OnGameOver;

    public Camera playerCamera;
    public GameObject surferHightLimitCollider;
    public GameObject cameraLimitCollider;

    public SpriteRenderer surferSpriteRenderer;

    public float speed = 10.0f;
    public float maxSpeed = 600.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 40.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;

    public Rigidbody rb;

    bool toZoomOut = false;
    public float cameraZoomOutSizeTarget = 90.0f;
    public float cameraZoomInSizeTarget = 56.0f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        playerCamera.transparencySortMode = TransparencySortMode.Orthographic;
    }

    private void Update()
    {
        SetFollowingObjectPosition();
        SetCameraSize();
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        bool isFlip = surferSpriteRenderer.flipX || surferSpriteRenderer.flipY;


        if(grounded && isFlip)
        {
            OnGameOver?.Invoke();
            // game over
            // freeze for some seconds and move to game over screen
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "cameraCollider")
        {
            toZoomOut = true;
        }
    }

    void PlayerMovement()
    {
        Vector3 targetVelocity = new Vector3(1, -1, 2);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = 2;
        velocityChange.y = -1;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        if (Input.GetMouseButton(0))
        {
            if (speed < maxSpeed)
            {
                speed += 1;
            }
        }
        else if (!Input.GetMouseButton(0) && grounded)
        {
            speed = 20;
        }

        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    void SetCameraSize()
    {
        if (toZoomOut)
        {
            if (playerCamera.orthographicSize < cameraZoomOutSizeTarget)
            {
                playerCamera.orthographicSize += 1;
            }
        }

        if ((Mathf.Approximately(playerCamera.orthographicSize, cameraZoomOutSizeTarget)) && grounded)
        {
            toZoomOut = false;
        }

        if (!toZoomOut)
        {
            if (playerCamera.orthographicSize > cameraZoomInSizeTarget)
            {
                playerCamera.orthographicSize -= 1;
            }
        }
    }

    void SetFollowingObjectPosition()
    {
        float playerXposition = transform.position.x;
        float colliderYPosition = surferHightLimitCollider.transform.position.y;
        float colliderZPosition = surferHightLimitCollider.transform.position.z;

        cameraLimitCollider.transform.position = surferHightLimitCollider.transform.position = new Vector3(playerXposition, colliderYPosition, colliderZPosition);

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }
}
