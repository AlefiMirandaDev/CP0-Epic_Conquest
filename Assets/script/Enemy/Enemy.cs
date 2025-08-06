using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;             // Velocidade de perseguição
    public float attackRange = 1.5f;         // Distância para atacar
    public float attackCooldown = 1.5f;      // Tempo entre ataques
    public int damage = 10;                  // Dano por ataque

    private Transform target;                // Referência ao herói atual
    private float nextAttackTime = 0f;       // Controle de tempo de ataque

    void Update()
    {
        FindPlayer();

        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            // Perseguir o jogador
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(target); // Olhar para o herói
        }
        else
        {
            // Atacar se estiver no alcance e o tempo permitir
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Attack()
    {
        Debug.Log($"{gameObject.name} atacou o jogador por {damage} de dano!");
        // Aqui você pode conectar com a saúde do herói, caso deseje
    }
}
