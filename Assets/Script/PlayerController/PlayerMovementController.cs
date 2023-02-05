using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Utilities;
using FMOD.Studio;

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
    public bool canJump;
    public bool CanRotate;

    private Rigidbody rb;

    [Header("JumpSetting")]
    [Range(1f, 1000f)]
    public float jumpForce;
    [SerializeField] Vector3 offSet;
    [SerializeField] float radius;

    [SerializeField]
    private PlayerAnimationController animController;

    Vector3 direction;
    bool isRun;
    public bool isJumping;

    InputSystemManager inputSystemManager;
    SoundManager soundManager;

    EventInstance playerWalkSFX;
    EventInstance playerRunSFX;

    public enum movementState
    {
        idle, walk, run
    }

    public movementState currentMovementState;

    void Start()
    {
        defaultSpeedMultiplier = SpeedMultiplier;

        animController.Init(this);

        rb = this.GetComponent<Rigidbody>();

        currentMovementState = movementState.idle;

        Vcam.Follow = this.gameObject.transform;
        Vcam.LookAt = this.gameObject.transform;

        inputSystemManager = SharedObject.Instance.Get<InputSystemManager>();
        soundManager = SharedObject.Instance.Get<SoundManager>();
        
        AddInputListiner();

        playerWalkSFX = soundManager.CreateInstance(soundManager.fModEvent.PlayerWalkSFX);
        playerRunSFX = soundManager.CreateInstance(soundManager.fModEvent.PlayerRunSFX);
    }

    void AddInputListiner()
    {
        inputSystemManager.onMove += OnMove;
        inputSystemManager.onPressMove += OnPressMove;
        inputSystemManager.onPressRun += OnPressRun;
        inputSystemManager.onPressJump  += OnPressJump;
    }

    void RemoveInputListiner()
    {
        inputSystemManager.onMove -= OnMove;
        inputSystemManager.onPressMove -= OnPressMove;
        inputSystemManager.onPressRun -= OnPressRun;
        inputSystemManager.onPressJump -= OnPressJump;
    }

    void Update() 
    {
        if (!canMove)
        {
            return;
        }

        Jump();

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

                WalkSFX();

                playerRunSFX.stop(STOP_MODE.ALLOWFADEOUT);
                break;

            case movementState.run:
                runToDirection(direction);

                RanSFX();

                playerWalkSFX.stop(STOP_MODE.ALLOWFADEOUT);
                break;

            case movementState.idle:
                playerWalkSFX.stop(STOP_MODE.ALLOWFADEOUT);
                playerRunSFX.stop(STOP_MODE.ALLOWFADEOUT);

                animController.AnimationState("idle");

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
            rb.velocity += moveDir.normalized * WalkSpeed * SpeedMultiplier * Time.deltaTime;

            animController.AnimationState("walk");
        }
    }

    private void runToDirection(Vector3 direction)
    {
        if (isJumping) { return; }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        if (CanRotate)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        if (canRun)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.velocity += moveDir.normalized * RunSpeed * SpeedMultiplier * Time.deltaTime;

            animController.AnimationState("run");
        }
        else
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.velocity += moveDir.normalized * WalkSpeed * SpeedMultiplier * Time.deltaTime;

            animController.AnimationState("walk");
        }
    }

    void Jump()
    {
        if (isJumping)
        {
            playerWalkSFX.stop(STOP_MODE.ALLOWFADEOUT);
            playerRunSFX.stop(STOP_MODE.ALLOWFADEOUT);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            if (isJumping) { return; }

            rb.velocity += Vector3.up * jumpForce * Time.fixedDeltaTime;

            JumpSFX();

            animController.PlayJumpAnimation();

            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) { return; }

        if (isJumping)
        {
            animController.Landing();
        }

        HitGroundSFX();
        canJump = true;
        isJumping = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) { return; }

        canJump = false;
        isJumping = true;
    }

    public void OnMove(Vector2 value)
    {
        //direction = new Vector3(value.x,0,value.y).normalized;
    }

    public void OnPressMove(bool value)
    {
        //canWalk = value;
    }

    public void OnPressRun(bool value)
    {
        //canRun = value;
    }

    public void OnPressJump(bool value)
    {
        // rb.velocity = Vector3.zero;
        //Jump();
    }

    void WalkSFX()
    {
        soundManager.AttachInstanceToGameObject(playerWalkSFX,transform,rb);

        playerWalkSFX.getPlaybackState(out var playBackState);
        if(playBackState.Equals(PLAYBACK_STATE.STOPPED))
            playerWalkSFX.start();
    }

    void RanSFX()
    {
        soundManager.AttachInstanceToGameObject(playerRunSFX,transform,rb);

        playerRunSFX.getPlaybackState(out var playBackState);
        if(playBackState.Equals(PLAYBACK_STATE.STOPPED))
            playerRunSFX.start();
    }

    void JumpSFX()
    {
        soundManager.PlayOneShot(soundManager.fModEvent.PlayerJumpSFX,this.gameObject);
    }

    void HitGroundSFX()
    {
        soundManager.PlayOneShot(soundManager.fModEvent.PlayerHitGroundSFX,this.gameObject);
    }

    void OnDestroy() 
    {
        RemoveInputListiner();
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offSet,radius);    
    }
}
