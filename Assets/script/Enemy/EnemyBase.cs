using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Status Básicos")]
    public float maxHealth = 100f;      // vida máxima do inimigo
    protected float currentHealth;      // vida atual do inimigo
    public float moveSpeed = 3f;        // velocidade de movimento do inimigo
    public float attackDamage = 10f;    // dano de ataque do inimigo
    public float attackRange = 2f;      // alcance de ataque do inimigo
    public float attackCooldown = 1f;   // tempo de recarga do ataque

    // Referência ao HUD
    protected Transform target; // alvo atual (heroi)
    protected float lastAttackTime = -Mathf.Infinity; //tempo do último ataque

    protected virtual void Start()
    {
        currentHealth = maxHealth; // Inicializa a vida atual com o máximo
    }

    /// <summary>
    /// Aplica dano ao inimigo.
    /// </summary>
    
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Aplica dano ao inimigo e verifica se ele morreu.
    /// </summary>  
    
    protected virtual void Die()
    {
        // Lógica de morte do inimigo
        Destroy(gameObject); // Destroi o objeto inimigo
    }

    /// <summary>
    ///  Ataca o alvo se estiver dentro do alcance de ataque.
    /// </summary>
    protected virtual void TryAttack()
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            // aplicar animação e/ou efeito de ataque aqui
        }
    }

}