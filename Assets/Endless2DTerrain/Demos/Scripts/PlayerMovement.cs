using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	
    public Camera playerCamera;
	
    public float speed = 12.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        controller = GetComponent<CharacterController>();

        playerCamera.transparencySortMode = TransparencySortMode.Orthographic;
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(1, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            gravity = 50.0f;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            //if (Input.touchCount > 0)
            //{
            //    moveDirection.y = jumpSpeed;
            //}

        }
        if (Input.GetMouseButton(0) /*&& !Input.GetMouseButtonUp(0)*/)
        {
            //gravity -= 1;

            if (speed < 100.0f)
            {
                speed += 1;
                //moveDirection = new Vector3(1, +1, Input.GetAxis("Vertical"));
                //moveDirection = transform.TransformDirection(moveDirection);
                //moveDirection *= speed;
            }
            moveDirection = new Vector3(1, -0.5f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //Debug.Log("GetMouseButtonDown ");
        }
        else if (speed > 12)
        {
            speed -= 1;
        }
        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Update()
    {    

        //if (controller.isGrounded)
        //{
        //    moveDirection = new Vector3(1, 0, Input.GetAxis("Vertical"));
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed;
        //    gravity = 50.0f;
        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = jumpSpeed; 
        //    }
        //    //if (Input.touchCount > 0)
        //    //{
        //    //    moveDirection.y = jumpSpeed;
        //    //}
           
        //}
        //if (Input.GetMouseButton(0) /*&& !Input.GetMouseButtonUp(0)*/)
        //{
        //    //gravity -= 1;

        //    if (speed < 100.0f)
        //    {
        //        speed += 1;
        //        //moveDirection = new Vector3(1, +1, Input.GetAxis("Vertical"));
        //        //moveDirection = transform.TransformDirection(moveDirection);
        //        //moveDirection *= speed;
        //    }
        //    moveDirection = new Vector3(1,-0.5f, Input.GetAxis("Vertical"));
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed;

        //    //Debug.Log("GetMouseButtonDown ");
        //}
        //else if(speed > 12)
        //{
        //    speed -= 1;
        //}
        //moveDirection.y -= gravity * Time.smoothDeltaTime;
        //controller.Move(moveDirection * Time.smoothDeltaTime);


        //After we move, adjust the camera to follow the player
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.tag == "")
    //    {
            
    //    }
    //}


    // OnCollisionStay(other:Collision)
    //{
    //    if (other.gameObject.tag == "Cube")
    //    {
    //        controller.isGrounded = true;
    //    }
    //}
}
