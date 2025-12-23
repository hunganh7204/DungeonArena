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
    private PlayerHealth _playerHealth;

    [Header("Combat")]
    public float attackRange = 1f;
    public float attackCooldown = 2.0f;
    public float damage = 10.0f;
    private float _lastAttackTime;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
       
        if (playerObj != null)
        {
            _playerTarget = playerObj.transform; 
            _playerHealth = playerObj.GetComponent<PlayerHealth>();
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

        if(_playerHealth != null && _playerHealth.isDeath)
        {
            _agent.ResetPath();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTarget.position);
        if (distanceToPlayer <= ChaseRange)
        {
            ChasePlayer();
            if(distanceToPlayer <= attackRange)
            {
                if(Time.time >= _lastAttackTime+attackCooldown)
                {
                    AttackPlayer();
                    _lastAttackTime = Time.time ;
                }
            }
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
        if(direction != Vector3.zero)
        {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*RotateSpeed);
        }
    }

    private void OnDrawGizmosSelected() //ve tam nhin quai
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }

    private void AttackPlayer()
    {
        Debug.Log("Enemy Attack");
        if(_playerTarget.TryGetComponent<IDamageable>(out IDamageable targetHealth))
        {
            targetHealth.TakeDamage(damage);
        }
    }
}
