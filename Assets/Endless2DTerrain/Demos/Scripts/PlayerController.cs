using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject surferHightLimitCollider;
    public GameObject cameraLimitCollider;

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 40.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
    public Rigidbody rigidbody;

    bool toZoomOut = false;
    int cameraSizeTarget = 56;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
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
        float playerXposition = transform.position.x;
        float colliderYPosition = surferHightLimitCollider.transform.position.y;
        float colliderZPosition = surferHightLimitCollider.transform.position.z;

        cameraLimitCollider.transform.position = surferHightLimitCollider.transform.position = new Vector3(playerXposition, colliderYPosition, colliderZPosition);

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);

        Debug.Log("to zoom out " + toZoomOut);
        Debug.Log("grounded " + grounded);

        if (toZoomOut)
        {
            if(playerCamera.orthographicSize < 90)
            {
                playerCamera.orthographicSize += 1;
            }
        }
        //if ((Mathf.Approximately(playerCamera.orthographicSize, 90)) && (grounded || !toZoomOut))
        if ((Mathf.Approximately(playerCamera.orthographicSize, 90)) && grounded)
        {
            toZoomOut = false;
            //if(playerCamera.orthographicSize > 56)
            //{
            //    playerCamera.orthographicSize -= 1;
            //}
        }

        if (!toZoomOut)
        {
            if(playerCamera.orthographicSize > 56)
            {
                playerCamera.orthographicSize -= 1;
            }
        }
    }

    //void FixedUpdate()
    //{
    //    Vector3 targetVelocity = new Vector3(1, -1, 2);
    //    targetVelocity = transform.TransformDirection(targetVelocity);
    //    targetVelocity *= speed;

    //    if(Input.GetMouseButton(0))
    //    {
    //            // Apply a force that attempts to reach our target velocity
    //            Vector3 velocity = rigidbody.velocity;
    //            Vector3 velocityChange = (targetVelocity - velocity);
    //            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
    //            velocityChange.z = 2;
    //            velocityChange.y = -1;
    //            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

    //            //// Jump
    //            //if (canJump && Input.GetButton("Jump"))
    //            //{
    //            //    rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), 2);
    //            //}
    //        }

    //    // We apply gravity manually for more tuning control
    //    rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

    //    grounded = false;
    //}

    void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(1, -1, 2);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = 2;
        velocityChange.y = -1;
        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        if (Input.GetMouseButton(0))
        {
            speed += 1;
            //// Jump
            //if (canJump && Input.GetButton("Jump"))
            //{
            //    rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), 2);
            //}
        } else if(!Input.GetMouseButton(0) && grounded)
        {
            speed = 20;
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

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
            cameraSizeTarget = 90;
            //playerCamera.orthographicSize = 90;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "cameraCollider")
    //    {
    //        toZoomOut = false;
    //        cameraSizeTarget = 56;
    //        //playerCamera.orthographicSize = 56;
    //    }
    //}

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
