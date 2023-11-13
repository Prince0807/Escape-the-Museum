using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    [SerializeField] GameObject equipedAxe;
    [SerializeField] GameObject holsterAxe;

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask attackMask;

    [SerializeField] GameObject attackCollider;

    private bool isAxeEquipped;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(isAxeEquipped? 1:0);
        if (animState.normalizedTime % 1 >= 0.9f)
            animator.SetBool("IsJumping", false);
    }

    public void EquipAxe()
    {
        isAxeEquipped = true;
        holsterAxe.SetActive(false);
        equipedAxe.SetActive(true);
    }

    public void UnequipAxe()
    {
        isAxeEquipped = false;
        equipedAxe.SetActive(false);
        holsterAxe.SetActive(true);
    }

    public void Attack()
    {
        Collider[] objs = Physics.OverlapSphere(attackPoint.position, attackRange, attackMask);

        foreach (Collider obj in objs)
        {
            if (obj.TryGetComponent(out IDamageable hit))
                hit.Damage(1000);
        }
    }
}
