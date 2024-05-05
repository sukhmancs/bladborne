using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage; 
        animator.SetTrigger("Hurt");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    void Die()
    {            
        animator.SetTrigger("IsDead");

        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }
}
