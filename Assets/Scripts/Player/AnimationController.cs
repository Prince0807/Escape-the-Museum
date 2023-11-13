using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    [SerializeField] GameObject equipedAxe;
    [SerializeField] GameObject holsterAxe;

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

    public void StartAttack()
    {

    }

    public void StopAttack()
    {

    }
}
