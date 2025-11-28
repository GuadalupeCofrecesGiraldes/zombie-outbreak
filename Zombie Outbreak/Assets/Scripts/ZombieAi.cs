using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;

    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (targetPlayer == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                targetPlayer = player.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPlayer != null)
        {
            agent.SetDestination(targetPlayer.position);
        }
    }
}
