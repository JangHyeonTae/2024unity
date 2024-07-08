using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] float gravityValue = -10f;
    [SerializeField] float groundDistance = 0.2f;
    public Vector3 MoveVector { set; get; }

    public VirtualJoystick joystick;
    public CharacterController charcon;
    
    public Transform target;
    public Transform camTrans;

    private bool isGrounded;
    private LayerMask groundMask;
    private Transform transGroundCheckPoint;


    Animator animator;
    float animator_Speed = 0f;
    private bool hasAnimator;


    void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        charcon = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();    
        transGroundCheckPoint = transform;
        groundMask = (1 << 6 ) | (1 << 7);

    }


    void Update()
    {
        MovePad();

        GroundedCheck();

        playerVelocity.y = gravityValue * Time.deltaTime;

        charcon.Move(playerVelocity * Time.deltaTime);
        
    }

    void GroundedCheck()
    {
        isGrounded = Physics.Raycast(transGroundCheckPoint.position, Vector3.down, groundDistance, groundMask);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;

        }
    }

    public void MovePad()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();


        Vector3 dirMove = transform.forward * dir.z + transform.right * dir.x;
        
        MoveVector = dirMove;
        animator_Speed = MoveVector.x;
        
        
        Vector3 movement = MoveVector * moveSpeed * Time.deltaTime;
        charcon.Move(movement);
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + dir.x, transform.rotation.eulerAngles.z);
        //camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(0, dir.x, 0f));

        //아직 사용안함 blendtree사용해서 조절 *mathf.lerp사용으로 speed,idle,motionspeed 조절
        if(hasAnimator)
        {
            animator.SetFloat("Speed", animator_Speed);
            //animator.SetFloat("MotionSpeed", 1f);
        }
    }
    public void Jump()
    {
        //playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);

        //charcon.Move(playerVelocity * Time.deltaTime);

        if (hasAnimator)
        {
            //animator.SetBool("Jump", true);
            animator.SetTrigger("Jump");
        }

    }
}
