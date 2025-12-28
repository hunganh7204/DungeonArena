using UnityEngine;
using UnityEngine.AI;

public class EnemyDummy : MonoBehaviour,IDamageable
{
    [SerializeField] float maxHealth = 30;
    private float currentHealth;

    private Animator _animator;

    private void Start()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Hp:: {currentHealth}");
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has been slain");
        if (GetComponent<NavMeshAgent>() != null) GetComponent<NavMeshAgent>().enabled = false;
        if (GetComponent<EnemyAI>() != null) GetComponent<EnemyAI>().enabled = false;
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;
        
        if(_animator != null)
        {
            _animator.SetTrigger("Die");
        }
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnEnemyKilled();
        }
        Destroy(gameObject,2.0f);
    }

    private System.Collections.IEnumerator FlashRed()
    {
        Renderer rend = GetComponent<Renderer>();
        if(rend != null )
        {
            Color originalColor = rend.material.color;
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            rend.material.color = originalColor;
        }
    }
}
