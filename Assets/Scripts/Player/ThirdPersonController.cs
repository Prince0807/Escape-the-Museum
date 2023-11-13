using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    // Components
    CharacterController controller;
    Animator animator;
    Transform playerCamera;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Jumping")]
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    [Header("Axe")]
    private bool isAxeEquipped = false;

    [HideInInspector] public bool isInteracting = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        CheckIsGrounded();
        Movement();
        JumpAndFall();
        Axe();
    }

    private void CheckIsGrounded()
    {
        isGrounded = controller.isGrounded;
            //Physics.CheckSphere(transform.position, 0.1f, 1);
        animator.SetBool("IsGrounded", isGrounded);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1;
    }

    private void Movement()
    {
        if (isInteracting)
            return;

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(moveInput.magnitude < 0.1f)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * (Input.GetButton("Sprint") ? runSpeed : walkSpeed);
        controller.Move(moveDirection * Time.deltaTime);
        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    private void JumpAndFall()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isInteracting)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
            animator.SetBool("IsJumping", true);
        }
        if (velocity.y > -20)
            velocity.y += (gravity * 10) * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Axe()
    {
        if(Input.GetKeyDown(KeyCode.X) && !isInteracting)
        {
            isAxeEquipped = !isAxeEquipped;
            if (isAxeEquipped)
            {
                animator.SetLayerWeight(1, 1f);
                animator.Play("Equip Axe", 1);
            }
            else
            {
                animator.SetLayerWeight(1, 0);
                animator.Play("Unequip Axe", 0);
            }
        }
    }
}
