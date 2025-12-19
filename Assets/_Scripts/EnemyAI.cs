using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public float ChaseRange = 10f; // tam nhin enemy
    public float RotateSpeed = 5f;

    private NavMeshAgent _agent;
    private Transform _playerTarget;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTarget = playerObj.transform;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTarget == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTarget.position);
        if (distanceToPlayer <= ChaseRange)
        {
            ChasePlayer();
        }
        else
        {
            _agent.ResetPath();//dung lai
        }
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_playerTarget.position);
        if(_agent.remainingDistance <= _agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (_playerTarget.position - transform.position).normalized;
        direction.y = 0; //chi xoay huong ngang
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*RotateSpeed);
    }

    private void OnDrawGizmosSelected() //ve tam nhin quai
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }
}
