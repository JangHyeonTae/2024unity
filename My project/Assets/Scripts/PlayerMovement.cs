using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float gravityValue = -30f;
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] GameObject player;
    public Vector3 MoveVector { set; get; }

    public VirtualJoystick joystick;
    public CharacterController charcon;
    
    public Transform target;
    public Transform camTrans;

    private bool isGrounded;
    private LayerMask groundMask;
    private Transform transGroundCheckPoint;

    public bool jumpActive;

    private Animator animator;

    void Start()
    {
        charcon =gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        transGroundCheckPoint = transform;
        groundMask = (1 << 6 ) | (1 << 7);

        //animator = GetComponent<Animator>();
    }


    void Update()
    {
        
        MovePad();  

        isGrounded = Physics.Raycast(transGroundCheckPoint.position, Vector3.down, groundDistance, groundMask);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpActive = true;
        }

        playerVelocity.y = gravityValue * Time.deltaTime;

        charcon.Move(playerVelocity * Time.deltaTime);
    }

    public void MovePad()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();


        Vector3 dirMove = transform.forward * dir.z + transform.right * dir.x;

        MoveVector = dirMove;


        Vector3 movement = MoveVector * moveSpeed * Time.deltaTime;
        charcon.Move(movement);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + dir.x, transform.rotation.eulerAngles.z);

        //camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(0, dir.x, 0f));
    }
    public void Jump()
    {
        
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        
        charcon.Move(playerVelocity * Time.deltaTime);
        //animator.SetBool("Jump", true);
        
    }
}
