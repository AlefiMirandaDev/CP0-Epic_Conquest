using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("configuração de Movimento Inimigo")]
    public float speed = 3f;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;

    [Header("configuração de ataque Inimigo")]
    public float attackCooldown = 2f;
    public float attackDamage = 10f;

    [Header("Tags e Detecção")]
    public string heroTag = "Player";

    private CharacterStats stats;
    private Transform targetHero;
    private float lastAttackTime;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        FindClosestHero (); // Encontra o herói mais próximo 
    }

    private void Update()
    {
        if (stats == null || stats.isDead)
        {
            return;
        }

        if (targetHero == null)
        {
            FindClosestHero();
            return;
        }

        float distance = Vector3.Distance(transform.position, targetHero.position);

        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                MoveToHero();
            }
            else if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackHero();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            FindClosestHero();
        }
    }

    ///<summary>
    ///Move o inimigo na direção do herói alvo.
    ///</summary>

    void MoveToHero()
    {
        Vector3 direction = (targetHero.position - transform.position).normalized;

        //movmento do inimigo
        if(direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    ///<summary>
    /// Aplicar dano ao herói alvo.
    /// </summary>
    
    void AttackHero()
    {
        CharacterStats heroStats = targetHero.GetComponent<CharacterStats>();

        if (heroStats != null && !heroStats.IsDead)
        {
            heroStats.TakeDamage(attackDamage);
        }
    }

    ///<summary>
    /// Busca o herói mais próximo na cena.
    /// </summary>

    void FindClosestHero()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag(heroTag);

        // Verifica se há heróis na cena
        if (heroes.Length == 0)
        {
            targetHero = null;
            return;
        }

        GameObject closestHero = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject hero in heroes)
        {
            CharacterStats herostats = hero.GetComponent<CharacterStats>();

            if (herostats == null || heroStats.isDead)
            {
                continue; // Ignora heróis mortos ou sem stats
            }

            float distance = Vector3.Distance(transform.position, hero.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHero = hero;
            }
        }

        targetHero = closestHero != null ? closestHero.transform : null;

        if (closestHero != null)
        {
            targetHero = closestHero.transform;
        }
        else
        {
            targetHero = null; // Nenhum herói encontrado
        }
    }
}