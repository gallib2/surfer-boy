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
    public Transform targetCoins;

    public SpriteRenderer surferSpriteRenderer;

    //public float speed = 10.0f;
    //public float maxSpeed = 600.0f;
    //public float gravity = 10.0f;
    //public float maxVelocityChange = 40.0f;
    //public bool canJump = true;
    //public float jumpHeight = 2.0f;
    private bool grounded = false;
    Vector3 lastLocalVelocity;

    public Rigidbody rb;

    bool toZoomOut = false;
    public float cameraZoomOutSizeTarget = 90.0f;
    public float cameraZoomInSizeTarget = 56.0f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        //rb.useGravity = false;
    }

    private void Start()
    {
        lastLocalVelocity = transform.InverseTransformDirection(rb.velocity);
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
        bool isFlip = surferSpriteRenderer.flipX || !surferSpriteRenderer.flipY;


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
        //var vel = rb.velocity;
        //vel.x *= 1.0f - 0.3f; // reduce x component...
        //rb.velocity = vel;

    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag != "cameraCollider" && collision.gameObject.tag != "LimitCollider")
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "cameraCollider")
        {
            toZoomOut = true;
        }
    }


    void PlayerMovement()/////////////////////////////////////////////////////// Where the magic happens
    {
        Vector3 currentLocalVelocity = transform.InverseTransformDirection(rb.velocity); //Save the current location of the player
        float maxVelocity = 1800.0f;
        float baseVelocity = 240.0f;
        float addVelocity = 80.0f;
        float addVelocityDown = 120.0f;
        float decreaseVelocity = 80.0f;
        float currentVelocity = baseVelocity;
        Vector3 ascendingGroundVelocity = new Vector3(baseVelocity, 5.0f); //ground speed (assuming player is going uphill)
        Vector3 descendingGroundVelocity = new Vector3(baseVelocity, -10.0f); //ground speed (assuming player is going downhill)
        Vector3 aerialVelocity = new Vector3(50.0f, -100.0f); //aerial speed
        Vector3 aerialVelocityGrounded = new Vector3(2.0f, -50.0f);

        if (Input.GetMouseButton(0) && grounded == true) //If the player is holding while on ground
        {
            if ((currentLocalVelocity.y - lastLocalVelocity.y) < 0) //and if the player is going up (comparing his Y now and before)
            {
//                Debug.Log("Going UP!");
                rb.AddForce(ascendingGroundVelocity); //apply force forward and up
                while (currentVelocity < maxVelocity)
                {
                    currentVelocity += addVelocity;
                    ascendingGroundVelocity[0] = currentVelocity;
                }
            }
            else //and if the player is going down
            {
//                Debug.Log("Going down!");
                rb.AddForce(descendingGroundVelocity); //apply force forward and down
                while (currentVelocity < maxVelocity)
                {
                    currentVelocity += addVelocityDown;
                    descendingGroundVelocity[0] = currentVelocity;
                }
            }
        }
        else if (Input.GetMouseButton(0) && grounded == false)
        {           
            rb.AddForce(aerialVelocity); //apply force down and forward
            while (currentVelocity < maxVelocity)
            {
                currentVelocity += addVelocity;
                descendingGroundVelocity[0] = currentVelocity;
            }
        }

        if (grounded == false)
        {
            rb.AddForce(aerialVelocityGrounded);
        }
        
        if (!Input.GetMouseButton(0))
        {
            currentVelocity -= decreaseVelocity;
            Debug.Log(currentVelocity);
        }

        lastLocalVelocity = currentLocalVelocity; //save last location 

        //Vector3 targetVelocity = new Vector3(1, -1, 2);
        //targetVelocity = transform.TransformDirection(targetVelocity);
        //targetVelocity *= speed;

        //Apply a force that attempts to reach our target velocity
        //Vector3 velocity = rb.velocity;
        //Vector3 velocityChange = (targetVelocity - velocity);
        //velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        //velocityChange.z = 2;
        //velocityChange.y = -1;
        //rb.AddForce(velocityChange, ForceMode.VelocityChange);

        //if (Input.GetMouseButton(0))
        //{
        //    if (speed < maxSpeed)
        //    {
        //        speed += 1;
        //    }
        //}
        //else if (!Input.GetMouseButton(0) && grounded) //NOTE: the slowdown feels too sudden. see if its possible make gradual slowdown, and make it faster on upward slopes (hard)
        //{
        //    speed = 20;
        //}//NOTE 2: possible bug here, sometimes when you let go of the mouse midair you slowdown to 20 immidiatly. maybe "grounded" is not updated correctly

        //// We apply gravity manually for more tuning control

        //rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));


    }

    //float CalculateJumpVerticalSpeed()
    //{
    //    // From the jump height and gravity we deduce the upwards speed 
    //    // for the character to reach at the apex.
    //    return Mathf.Sqrt(2 * jumpHeight * gravity);
    //}

    void SetCameraSize()
    {
        if (toZoomOut)
        {
            if (playerCamera.orthographicSize < cameraZoomOutSizeTarget)
            {
                playerCamera.orthographicSize += 3;
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
                playerCamera.orthographicSize -= 3;
            }
        }
    }

    void SetFollowingObjectPosition()
    {
        float playerXposition = transform.position.x;
        float colliderYPosition = surferHightLimitCollider.transform.position.y;
        float colliderZPosition = surferHightLimitCollider.transform.position.z;

        targetCoins.position = new Vector3(playerXposition -20 , 86.0f, 3);
        cameraLimitCollider.transform.position = 
            surferHightLimitCollider.transform.position = new Vector3(playerXposition, colliderYPosition, colliderZPosition);

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }
}
