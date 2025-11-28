using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float attackCooldown = 2f;

    [SerializeField] private int attackDamage = 15;

    private float lastAttackTime;


    private NavMeshAgent agent;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (targetPlayer == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                targetPlayer = player.transform;
            }
        }

        lastAttackTime = -attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

            if (distanceToPlayer <= attackRange)
            {
                agent.isStopped = true;
                anim.SetBool("IsRunning", false);


                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(targetPlayer.position);

                bool isMoving = agent.velocity.magnitude > 0.01f;
                anim.SetBool("IsRunning", isMoving);
            }
        }
    }

    void AttackPlayer()
    {
        lastAttackTime = Time.time;
        anim.SetTrigger("Attack");
    }

    public void DieLogic()
    {
        if(anim != null)
        {
            anim.SetBool("IsDead", true);
        }

        agent.isStopped = true;
        agent.enabled = false;
        this.enabled = false;
    }

    public void ApplyDamage()
    {
        if (targetPlayer != null)
        {
            Health playerHealth = targetPlayer.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
