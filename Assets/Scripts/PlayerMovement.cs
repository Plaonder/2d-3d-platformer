using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    float playerHeight = 2f;
    public Transform camTransform;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    public float zPos2d;
    float savedZPos;

    [Header("Sprinting")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 6f;
    public float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 13f;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    bool isGrounded;

    bool wallDetected;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;
    RaycastHit ceilingHit;
    #endregion

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    private bool Behind3DWall()
    {
        if(Physics.CapsuleCast(transform.position + Vector3.up * 0.5f, transform.position - Vector3.up * 0.5f, 0.5f, Vector3.forward * -1, 100, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        PlayerInput();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(GameManager.switchKey))
        {
            Behind3DWall();
            if (GameManager.is3D)
            {
                SwitchTo3D();
            }
            else
            {
                SwitchTo2D();
            }
        }

        if (GameManager.is3D)
        {
            When3D();
        }
        else
        {
            When2D();
        }
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void FixedUpdate()
    {
        Move();
    }

    void SwitchTo3D()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, savedZPos);
    }

    void SwitchTo2D()
    {
        savedZPos = transform.position.z;
        if(!Behind3DWall())
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos2d);
            wallDetected = false;
            print("No wall, all good");
        } else
        {
            print("AHHH THERE'S A WALL HERE");
            wallDetected = true;
            return;
        }
    }

    void When2D()
    {
        if(wallDetected && !Behind3DWall())
        {
            print("There is no longer a wall detected");
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos2d);
            wallDetected = false;
        }
        else if (!wallDetected)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos2d);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, savedZPos);
        }
    }

    void When3D()
    {
    }

    void Move()
    {
        // more gravity
        rb.AddForce(Vector3.down * 10, ForceMode.Force);

        // move me!
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        // jumping!
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void PlayerInput()
    {
        // gets the axis and then uses that to get the move direction
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = camTransform.forward * verticalMovement + camTransform.right * horizontalMovement;
    }

    void ControlSpeed()
    {
        // lets you sprint!
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        // changes the drag of the player depending if they're on the ground or not
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    public void SetPlayerLocation(Vector3 location)
    {
        transform.position = location;
        rb.velocity = Vector3.zero;
        Debug.Log("Teleporting player to " + location);
    }
}
