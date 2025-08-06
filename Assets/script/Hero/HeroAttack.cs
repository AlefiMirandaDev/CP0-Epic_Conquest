using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    public float attackDamage = 10f;
    public float specialDamage = 30f;

    public float attackCost = 0f;
    public float specialCost = 20f;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // botão de ataque
        {
            NormalAttack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) // botão de especial
        {
            SpecialAttack();
        }
    }

    void NormalAttack()
    {
        if (!stats.UseEnergy(attackCost)) return;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<CharacterStats>()?.TakeDamage(attackDamage);
        }
    }

    void SpecialAttack()
    {
        if (!stats.UseEnergy(specialCost)) return;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<CharacterStats>()?.TakeDamage(specialDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
