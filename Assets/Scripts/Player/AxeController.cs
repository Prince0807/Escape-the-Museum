using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    Animator animator;

    float damage = 20f;
    bool isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

        }

        if (!isAttacking)
        {
            if (Input.GetButtonDown("Attack1"))
                animator.Play("Attack1");
            else if (Input.GetButtonDown("Attack2"))
                animator.Play("Attack2");
            else if (Input.GetButtonDown("Attack3"))
                animator.Play("Attack3");
            else if (Input.GetButtonDown("Attack4"))
                animator.Play("Attack4");
        }
    }
}
