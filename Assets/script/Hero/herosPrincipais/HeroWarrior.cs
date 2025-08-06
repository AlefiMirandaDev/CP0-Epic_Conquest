using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroWarrior : HeroBase
{
    // Variáveis específicas do Guerreiro
    [Header("Ataques do Guerreiro")]
    // Raio de ataque do Guerreiro
    public float attackRange = 3f;
    // Define as camadas que serão atingidas pelos ataques
    public LayerMask enemyLayers;

    // Dano de cada ataque
    public int attack1Damage = 10;
    public int attack2Damage = 15;
    public int attack3Damage = 20;
    public int specialDamage = 40;

    // Custo de mana para cada ataque
    public int attack1ManaCost = 5;
    public int attack2ManaCost = 10;
    public int attack3ManaCost = 15;
    public int specialManaCost = 30;

    // Ponto de ataque onde os ataques serão realizados
    public Transform attackPoint;

    // Referência para o HUD
    public override void HandleInput()
    {
        // Verifica as teclas pressionadas para realizar os ataques
        if (Input.GetKeyDown(KeyCode.Alpha1)) Attack1();
        else if (Input.GetKeyDown(KeyCode.Alpha2)) Attack2();
        else if (Input.GetKeyDown(KeyCode.Alpha3)) Attack3();
        else if (Input.GetKeyDown(KeyCode.E)) Special();
    }

    // Método chamado quando o Guerreiro é instanciado
    public void Attack1()
    {
        // Verifica se há mana suficiente para realizar o ataque
        if (!UseMana(attack1ManaCost))
        {
            // Se não houver mana suficiente, exibe uma mensagem no console
            Debug.Log($"{heroName}: Mana insuficiente para ataque 1");
            return;
        }

        // Inicia o cooldown do ataque no HUD
        hud?.StartCooldown(1);
        Debug.Log($"{heroName} usou Ataque 1");

        var target = GetNearestEnemy();
        if(target != null)
        {
            FaceTarget(target.transform.position);
            target.TakeDamage(attack1Damage);
        }
    }

    public void Attack2()
    {
        if (!UseMana(attack2ManaCost))
        {
            Debug.Log($"{heroName}: Mana insuficiente para ataque 2");
            return;
        }
        hud?.StartCooldown(2);
        Debug.Log($"{heroName} usou Ataque 2");
        
        var target = GetNearestEnemy();
        if(target != null)
        {
            FaceTarget(target.transform.position);
            target.TakeDamage(attack2Damage);
        }
    }

    public void Attack3()
    {
        if (!UseMana(attack3ManaCost))
        {
            Debug.Log($"{heroName}: Mana insuficiente para ataque 3");
            return;
        }
        hud?.StartCooldown(3);
        Debug.Log($"{heroName} usou Ataque 3");
        
        var target = GetNearestEnemy();
        if(target != null)
        {
            FaceTarget(target.transform.position);
            target.TakeDamage(attack3Damage);
        }
    }

    public void Special()
    {
        if (!UseMana(specialManaCost))
        {
            Debug.Log($"{heroName}: Mana insuficiente para especial");
            return;
        }
        hud?.StartCooldown(4);
        Debug.Log($"{heroName} usou Especial");
        
        var target = GetNearestEnemy();
        if(target != null)
        {
            FaceTarget(target.transform.position);
            target.TakeDamage(specialDamage);
        }
    }

    private CharacterStats GetNearestEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        CharacterStats nearestEnemy = null;

        float minDistance = Mathf.Infinity;

        foreach (Collider enemyCollider in enemiesInRange)
        {
            CharacterStats enemyStats = enemyCollider.GetComponent<CharacterStats>();
            if (enemyStats != null)
            {
                float distance = Vector3.Distance(attackPoint.position, enemyCollider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemyStats;
                }
            }
        }
        return nearestEnemy;
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // evitar que o herói olhe para cima ou para baixo

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}