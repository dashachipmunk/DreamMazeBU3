using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Lean.Pool;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject player;
    private HealthController playerHealthController;
    private CapsuleCollider colliderEnemy;

    [Header("Path finder")]
    [SerializeField]
    private GameObject[] patrolPoints;
    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;
    private System.Random randomPatrolPoints;
    private Transform startPosition;
    private float distance;

    [Header("Attack")]
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float moveRange;
    [SerializeField]
    private GameObject particleAttack;

    [Header("Health")]
    [SerializeField]
    private int health;
    [SerializeField]
    private float scaleReduce;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource audioSourceAttack;
    [SerializeField]
    private AudioSource audioSourceDeath;

    private EnemyStates enemyStates;

    enum EnemyStates { PATROL, CHASE, ATTACK }

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        colliderEnemy = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        playerHealthController = FindObjectOfType<HealthController>();
        randomPatrolPoints = new System.Random();
        startPosition = patrolPoints[0].transform;
        enemyStates = EnemyStates.PATROL;
        StartCoroutine(Patrolling());
    }

    private void Update()
    {
        UpdateState();
    }

    private void EnemyDies()
    {
        if (health <= 0)
        {
            audioSourceDeath.Play();
            StartCoroutine(WaitSound());
        }
    }

    void UpdateState()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        switch (enemyStates)
        {
            case EnemyStates.ATTACK:
                if (distance > attackRange)
                {
                    ChangeState(EnemyStates.CHASE);
                }

                break;

            case EnemyStates.CHASE:

                if (distance <= attackRange)
                {
                    ChangeState(EnemyStates.ATTACK);
                }
                break;

            case EnemyStates.PATROL:
                if (distance <= moveRange)
                {
                    ChangeState(EnemyStates.CHASE);
                }
                else if (distance > moveRange)
                {
                    ChangeState(EnemyStates.PATROL);
                }
                float distanceToPoint = Vector3.Distance(transform.position, patrolPoints[0].transform.position);
                if (distanceToPoint <= 0.01f)
                {
                    int point = randomPatrolPoints.Next(0, patrolPoints.Length);
                    startPosition = patrolPoints[point].transform;
                }
                break;
        }
    }

    private void ChangeState(EnemyStates newState)
    {
        enemyStates = newState;
        switch (enemyStates)
        {
            case EnemyStates.ATTACK:
                aiPath.enabled = false;
                LeanPool.Spawn(particleAttack, transform.position, Quaternion.identity);
                audioSourceAttack.Play();
                playerHealthController.HealthReduce();
                break;

            case EnemyStates.CHASE:
                aiPath.enabled = true;
                destinationSetter.target = player.transform;
                break;

            case EnemyStates.PATROL:
                aiPath.enabled = true;
                destinationSetter.target = startPosition;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerShot"))
        {
            health--;
            Vector3 healthReduce = new Vector3(scaleReduce, scaleReduce, scaleReduce);
            transform.localScale -= healthReduce;
            colliderEnemy.radius = 2;
            Destroy(other.gameObject);
            EnemyDies();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private IEnumerator Patrolling()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (destinationSetter.target != player.transform)
            {
                int point = randomPatrolPoints.Next(0, patrolPoints.Length);
                startPosition = patrolPoints[point].transform;
                ChangeState(EnemyStates.PATROL);
            }
        }
    }

    private IEnumerator WaitSound()
    {
        yield return new WaitWhile(() => audioSourceDeath.isPlaying);
        Destroy(gameObject);
    }
}
