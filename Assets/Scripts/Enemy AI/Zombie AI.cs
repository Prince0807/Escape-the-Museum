using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour, IDamageable
{
    [SerializeField] float attackRange = 1.5f;

    [SerializeField] private ParticleSystem bloodSplashEffect;
    [SerializeField] private ParticleSystem chunksParticleEffect;
    [SerializeField] public int Health { get; set; }

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;


    public void Damage(int damage)
    {
        Health -= damage;
        bloodSplashEffect.Play();
        bloodSplashEffect.GetComponent<AudioSource>().Play();
        animator.Play("React");

        if(Health <= 0)
        {
            animator.Play("Death");
            chunksParticleEffect.Play();
            chunksParticleEffect.GetComponent<AudioSource>().Play();
            agent.isStopped = true;
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;

            GameManager.INSTANCE.AddScore(100);

            Destroy(gameObject, 2f);
            this.enabled = false;
        }
    }

    private void Awake()
    {
        Health = 100;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<ThirdPersonController>().transform;
    }

    private void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < attackRange)
        {
            agent.isStopped = true;
            animator.SetBool("IsInAttackRange", true);
        }
        else
        {
            agent.isStopped= false;
            agent.SetDestination(player.position);
            animator.SetBool("IsInAttackRange", false);
        }
    }

    public void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackRange && player.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(Random.Range(10,20));
        }
    }
}
