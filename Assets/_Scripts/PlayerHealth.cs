using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public float maxHealth = 100;
    private float _currentHealth;
    public bool isDeath {  get; private set; }

    [Header("Events")]
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnDeath;

    private void Start()
    {
        _currentHealth = maxHealth;
        isDeath = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDeath) return;

        
        
        _currentHealth -= damage;
        Debug.Log($"Player bi danh, mau con: {_currentHealth}");

        if(_currentHealth < 0 ) _currentHealth = 0;
    
        float healthPercent = _currentHealth/maxHealth;
        OnHealthChange?.Invoke(healthPercent);

        if(_currentHealth <= 0) Die();
    }

    private void Die()
    {
        if (isDeath) return;

        isDeath = true;
        Debug.Log("Game over");
        OnDeath?.Invoke();

        GetComponent<PlayerController>().enabled = false;

        if(GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
    }

}
