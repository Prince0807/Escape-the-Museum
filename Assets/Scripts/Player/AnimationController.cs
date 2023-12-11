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
    [SerializeField] AudioSource axeSwingAudioSource;

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

        int damage = 10;

        string currentAnimationName = animator.GetCurrentAnimatorClipInfo(1)[0].clip.name;
        switch (currentAnimationName)
        {
            case "Attack 1":
                damage = 20;
                break;
            case "Attack 2":
                damage = 25;
                break;
            case "Attack 3":
                damage = 50;
                break;
            case "Attack 4":
                damage = 100;
                break;
            default:
                break;
        }

        foreach (Collider obj in objs)
        {
            if (obj.TryGetComponent(out IDamageable hit))
            {
                hit.Damage(damage);
                return;
            }
        }
        axeSwingAudioSource.Play();
    }
}
