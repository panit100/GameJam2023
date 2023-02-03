using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Walk State Setting")]

    [Range(0f, 10f)]
    public float WalkSpeed;

    [Range(1f, 1000f)]
    public float SpeedMultiplier;

    private float defaultSpeedMultiplier;

    [Header("Run State Setting")]

    [Range(0f, 10f)]
    public float RunSpeed;


    [Header("Camera Setting")]

    [Range(0f, 1f)]
    public float turnSmoothTime;

    private float turnSmoothVelocity;

    [SerializeField]
    private CinemachineFreeLook Vcam;

    [SerializeField]
    private Transform mainCam;

    public bool canMove;
    public bool canWalk;
    public bool canRun;
    public bool CanRotate;

    private Rigidbody rb;

    public enum movementState
    {
        idle, walk, run
    }

    public movementState currentMovementState;

    void Start()
    {
        defaultSpeedMultiplier = SpeedMultiplier;

        rb = this.GetComponent<Rigidbody>();

        currentMovementState = movementState.idle;
        mainCam = Camera.main.transform;

        Vcam.Follow = this.gameObject.transform;
        Vcam.LookAt = this.gameObject.transform;
    }

    void Update()
    {
        if (!canMove) return;

        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        currentMovementState = CheckMovementState(direction);

        MoveDependOnState(direction);
    }

    private movementState CheckMovementState(Vector3 direction)
    {
        if (direction.magnitude < 0.1f) { return movementState.idle; }

        if (Input.GetKey(KeyCode.LeftShift)) { return movementState.run; }

        return movementState.walk;
    }

    private void MoveDependOnState(Vector3 direction)
    {
        switch (currentMovementState)
        {
            case movementState.walk:
                walkToDirection(direction);
                break;

            case movementState.run:
                runToDirection(direction);
                break;

            case movementState.idle:

                //TODO : idle animation

                break;
        }
    }

    private void walkToDirection(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        if (CanRotate)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        if (canWalk)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(moveDir.normalized * WalkSpeed * SpeedMultiplier * Time.deltaTime);

            //TODO : Walk animation
        }
    }

    private void runToDirection(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        if (CanRotate)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        if (canRun)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.AddForce(moveDir.normalized * RunSpeed * SpeedMultiplier * Time.deltaTime);

            //TODO : Run animation
        }
        else
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(moveDir.normalized * WalkSpeed * SpeedMultiplier * Time.deltaTime);

            //TODO : Walk animation
        }
    }
}
