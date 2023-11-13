using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    ThirdPersonController controller;
    Animator animator;

    float damage = 20f;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        controller = GetComponentInParent<ThirdPersonController>();
    }

    private void Update()
    {
        if (!controller.isInteracting)
        {
            if (Input.GetButtonDown("Attack1"))
                animator.Play("Attack 1", 1);
            else if (Input.GetButtonDown("Attack2"))
                animator.Play("Attack 2", 1);
            else if (Input.GetButtonDown("Attack3"))
                animator.Play("Attack 3", 1);
            else if (Input.GetButtonDown("Attack4"))
                animator.Play("Attack 4", 1);
        }
    }
}
