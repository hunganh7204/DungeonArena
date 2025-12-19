using UnityEngine;

public class EnemyDummy : MonoBehaviour,IDamageable
{
    [SerializeField] float maxHealth = 30;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
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
        gameObject.SetActive(false);
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
