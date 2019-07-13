using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject surferHightLimitCollider;

    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

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

        surferHightLimitCollider.transform.position = new Vector3(playerXposition, colliderYPosition, colliderZPosition);

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if(transform.position.y > 35)
            {
                Vector3 pos = new Vector3(1, -1, 0);
                pos *= 2;
                rb.AddForce(pos * 200, ForceMode.Force);
                //rb.AddForceAtPosition(pos * 200, pos);
            }
            else
            {
                Vector3 pos = new Vector3(1, -1, 0);
                pos *= 2;
                rb.AddForce(pos * 100, ForceMode.Force);
                //rb.AddForceAtPosition(pos * 200, pos);
            }

            //rb.velocity = new Vector3();

            Debug.Log("velocity: " + rb.velocity);
        }


    }
}
